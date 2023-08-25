namespace bcw_2023summer_keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepository;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
        {
            _vaultKeepsRepository = vaultKeepsRepository;
            _vaultsService = vaultsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            Vault foundVault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
            _vaultsService.VaultOwnerCheck(foundVault, vaultKeepData.CreatorId);
            VaultKeep createdVaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
            return createdVaultKeep;
        }

        internal VaultKeep GetVaultKeepById(int vaultKeepId)
        {
            VaultKeep foundVaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
            return foundVaultKeep;
        }

        internal void DeleteVaultKeep(int vaultKeepId, string userId)
        {
            VaultKeep vaultKeepToDelete = GetVaultKeepById(vaultKeepId);
            if (vaultKeepToDelete.CreatorId != userId)
            {
                throw new Exception("No VaultKeep found!");
            }
            _vaultKeepsRepository.DeleteVaultKeep(vaultKeepId);
        }
    }
}