namespace bcw_2023summer_keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepository;

        public VaultsService(VaultsRepository vaultsRepository)
        {
            _vaultsRepository = vaultsRepository;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            int vaultId = _vaultsRepository.CreateVault(vaultData);
            Vault createdVault = GetVaultById(vaultId, vaultData.CreatorId);
            return createdVault;
        }

        internal Vault EditVault(Vault vaultData, string userId)
        {
            Vault originalVault = GetVaultById(vaultData.Id, userId);
            VaultOwnerCheck(originalVault, userId);
            originalVault.Name = vaultData.Name ?? originalVault.Name;
            originalVault.Description = vaultData.Description ?? originalVault.Description;
            originalVault.isPrivate = vaultData.isPrivate ?? originalVault.isPrivate;
            _vaultsRepository.EditVault(originalVault);
            Vault editedVault = GetVaultById(vaultData.Id, userId);
            return editedVault;
        }

        internal Vault GetVaultById(int vaultId, string userId)
        {
            Vault foundVault = _vaultsRepository.GetVaultById(vaultId);
            _VaultValidationChecks(foundVault, userId);
            return foundVault;
        }

        internal void DeleteVault(int vaultId, string userId)
        {
            Vault vaultToDelete = GetVaultById(vaultId, userId);
            VaultOwnerCheck(vaultToDelete, userId);
            _vaultsRepository.DeleteVault(vaultId);
        }

        private void _VaultValidationChecks(Vault vaultToCheck, string userId)
        {
            if (vaultToCheck == null)
            {
                throw new Exception("No vault found with that id!");
            }
            if (vaultToCheck.isPrivate == true && vaultToCheck.CreatorId != userId)
            {
                throw new Exception("No vault found!");
            }
        }
        public void VaultOwnerCheck(Vault vaultToCheck, string userId)
        {
            if (vaultToCheck.CreatorId != userId)
            {
                throw new Exception("No vault found!");
            }
        }
    }
}