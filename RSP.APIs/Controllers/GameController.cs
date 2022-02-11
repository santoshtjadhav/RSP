using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSP.Game;
namespace RSP.APIs.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GameController : Controller
    {

        [HttpGet("list")]
        public ActionResult<IEnumerable<RSP.Game.Game>> List()
        {
            return Ok(StoreContainer.GameStore.GetState().GameList
                .OrderBy(c => c.GameId)
                .Take(20)
            );
        }

        [HttpGet("authtest")]        
        public ActionResult<string> AuthTest()
        {
            return Ok("Authenticated successfully");
        }
    }
}
