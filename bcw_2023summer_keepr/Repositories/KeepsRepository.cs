namespace bcw_2023summer_keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal int CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO
            keeps(creatorId, name, description, img)
            VALUES(@CreatorId, @Name, @Description, @Img);
            SELECT LAST_INSERT_ID()
            ;";

            return _db.ExecuteScalar<int>(sql, keepData);
        }

        internal void DeleteKeep(int keepId)
        {
            string sql = "DELETE FROM keeps WHERE id = @KeepId;";
            _db.Execute(sql, new { keepId });
        }

        internal void EditKeep(Keep originalKeep)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            Img = @Img
            WHERE id = @Id
            ;";
            _db.Execute(sql, originalKeep);
        }

        internal Keep GetKeepById(int keepId)
        {
            string sql = @"
            SELECT 
            k.*,
            COUNT(kv.id) AS kept,
            acc.*
            FROM keeps k
            LEFT JOIN vaultkeeps kv ON kv.keepId = k.id
            JOIN accounts acc ON acc.id = k.creatorId
            WHERE k.id = @KeepId
            ;";

            Keep foundKeep = _db.Query<Keep, Profile, Keep>(sql, 
            (keep, profile) => {
                keep.Creator = profile;
                return keep;
            }, new { keepId }).FirstOrDefault();
            return foundKeep;
        }

        internal List<Keep> GetKeeps()
        {
            string sql = @"
            SELECT
            k.*,
            acc.*
            FROM keeps k
            JOIN accounts acc ON k.creatorId = acc.id
            ;";

            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql,
            (keep, profile) => {
                keep.Creator = profile;
                return keep;
            }).ToList();
            return keeps;
        }
    }
}