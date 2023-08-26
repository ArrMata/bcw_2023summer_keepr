namespace bcw_2023summer_keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {

        private readonly Auth0Provider _auth0Provider;
        private readonly AccountService _accountService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(Auth0Provider auth0Provider, AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
        {
            _auth0Provider = auth0Provider;
            _accountService = accountService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }


        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfileById(string profileId)
        {
            try
            {
                Profile profile = _accountService.GetProfileById(profileId);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
        {
            try
            {
                List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userInfo?.Id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}