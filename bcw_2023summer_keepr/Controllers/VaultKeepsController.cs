namespace bcw_2023summer_keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth0Provider;
        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth0Provider = auth0Provider;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                vaultKeepData.CreatorId = userInfo.Id;
                VaultKeep createdVaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
                return Ok(createdVaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{vaultKeepId}")]
        public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo.Id);
                return Ok("Deleted Vault Keep");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}