namespace bcw_2023summer_keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO
            vaultkeeps(creatorId, vaultId, keepId)
            VALUES(@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID()
            ;";
            int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = vaultKeepId;
            return vaultKeepData;
        }

        internal void DeleteVaultKeep(int vaultKeepId)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @VaultKeepId;";
            _db.Execute(sql, new { vaultKeepId });
        }

        internal VaultKeep GetVaultKeepById(int vaultKeepId)
        {
            string sql = @"
            SELECT *
            FROM vaultkeeps
            WHERE id = @VaultKeepId
            ;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultKeepId });
        }
    }
}