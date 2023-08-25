namespace bcw_2023summer_keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth0Provider;
        private readonly VaultKeepsService _vaultKeepsService;
        public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService, KeepsService keepsService)
        {
            _vaultsService = vaultsService;
            _auth0Provider = auth0Provider;
            _vaultKeepsService = vaultKeepsService;
            _keepsService = keepsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault createdVault = _vaultsService.CreateVault(vaultData);
                return Ok(createdVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}")]
        public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Vault foundVault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
                return Ok(foundVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{vaultId}")]
        public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                vaultData.Id = vaultId;
                Vault editedVault = _vaultsService.EditVault(vaultData, userInfo.Id);
                return Ok(editedVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{vaultId}")]
        public async Task<ActionResult<string>> DeleteVault(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                _vaultsService.DeleteVault(vaultId, userInfo.Id);
                return Ok("Vault deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}/keeps")]
        public async Task<ActionResult<List<VaultKeep>>> GetKeepsByVaultId(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<KeepRelationship> keeps = _keepsService.GetKeepsByVaultId(vaultId, userInfo?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}