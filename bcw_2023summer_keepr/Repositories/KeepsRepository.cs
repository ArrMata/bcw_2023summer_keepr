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
            views = @Views,
            img = @Img
            WHERE id = @Id
            ;";
            _db.Execute(sql, originalKeep);
        }

        internal Keep GetKeepById(int keepId)
        {
            string sql = @"
            SELECT 
            k.*,
            COUNT(vk.id) AS kept,
            acc.*
            FROM keeps k
            LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
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

        internal List<Keep> GetKeepsByProfileId(string profileId)
        {
            string sql = "SELECT * FROM keeps WHERE creatorId = @ProfileId;";
            List<Keep> keeps = _db.Query<Keep>(sql, new { profileId }).ToList();
            return keeps;
        }

        internal List<KeepRelationship> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
            SELECT
            vk.id,
            k.*,
            acc.*
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts acc ON acc.id = vk.creatorId
            WHERE vk.vaultId = @VaultId
            ;";

            List<KeepRelationship> keeps = (
                _db.Query<VaultKeep, KeepRelationship, Profile, KeepRelationship>(sql, 
                (vaultkeep, keeprelationship, profile) => {
                    keeprelationship.VaultKeepId = vaultkeep.Id;
                    keeprelationship.Creator = profile;
                    return keeprelationship;
                }, new { vaultId }).ToList()
            );
            return keeps;
        }
    }
}