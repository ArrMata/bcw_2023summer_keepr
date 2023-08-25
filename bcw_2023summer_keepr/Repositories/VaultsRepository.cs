namespace bcw_2023summer_keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal int CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO
            vaults(creatorId, name, description, img, isPrivate)
            VALUES(@CreatorId, @Name, @Description, @Img, @IsPrivate);
            SELECT LAST_INSERT_ID()
            ;";
            return _db.ExecuteScalar<int>(sql, vaultData);
        }

        internal void DeleteVault(int vaultId)
        {
            string sql = "DELETE FROM vaults WHERE id = @VaultId";
            _db.Execute(sql, new { vaultId });
        }

        internal void EditVault(Vault editedVault)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            isPrivate = @isPrivate
            WHERE id = @Id
            ;";
            _db.Execute(sql, editedVault);
        }

        internal Vault GetVaultById(int vaultId)
        {
            string sql = @"
            SELECT
            v.*,
            acc.*
            FROM vaults v
            JOIN accounts acc ON v.creatorId = acc.id
            WHERE v.id = @VaultId
            LIMIT 1
            ;";

            Vault foundVault = _db.Query<Vault, Profile, Vault>(sql, 
            (vault, profile) => {
                vault.Creator = profile;
                return vault;
            }, new { vaultId }).FirstOrDefault();
            return foundVault;
        }

        internal List<Vault> GetVaultsByProfileId(string profileId)
        {
            string sql = @"
            SELECT * FROM vaults
            WHERE creatorId = @ProfileId
            ;";

            List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
            return vaults;
        }
    }
}