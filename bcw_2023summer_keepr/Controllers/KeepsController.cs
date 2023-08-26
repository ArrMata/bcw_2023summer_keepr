namespace bcw_2023summer_keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth0Provider;

        public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
        {
            _keepsService = keepsService;
            _auth0Provider = auth0Provider;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData) 
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                keepData.CreatorId = userInfo.Id;
                Keep keep = _keepsService.CreateKeep(keepData);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Keep>> GetKeeps()
        {
            try
            {
                List<Keep> keeps = _keepsService.GetKeeps();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetKeepById(int keepId)
        {
            try
            {
                Keep foundKeep = _keepsService.ViewKeepById(keepId);
                return Ok(foundKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{keepId}")]
        public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                keepData.Id = keepId;
                Keep editedKeep = _keepsService.EditKeep(keepData, userInfo.Id);
                return Ok(editedKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{keepId}")]
        public async Task<ActionResult<string>> DeleteKeep(int keepId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                _keepsService.DeleteKeep(keepId, userInfo.Id);
                return Ok("Keep Deleted.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}