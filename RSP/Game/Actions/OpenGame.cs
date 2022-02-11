using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redux;

namespace RSP.Game.Actions
{
    public class OpenGame : IAction
    {
        public OpenGame(string GameId, string userId)
        {
            this.GameId = GameId;
            UserId = userId;
        }
        public string GameId { get; private set; }
        public string UserId { get; private set; }
    }



    public class OpenGameEffect : ActionEffect<OpenGame, GameState>
    {
        private readonly ILogger<OpenGameEffect> _logger;

        public OpenGameEffect(ILogger<OpenGameEffect> logger)
        {

            _logger = logger;
        }


        public override async Task<IAction> Effect(GameState prevState, OpenGame action)
        {
            _logger.LogInformation("OpenGameEffect {GameId} {userId}", action.GameId, action.UserId);
            var Game = ApplicationData.Games
                .Select(c => new Game()
                {
                    //Address = c.Address,
                    //City = c.City,
                    //CompanyName = c.CompanyName,
                    //ContactName = c.ContactName,
                    GameId = c.GameId
                })
                .FirstOrDefault(c => c.GameId == action.GameId);
            var obj = (IAction)new OpenGameSuccess(Game, action.UserId);
            return await Task.FromResult( obj);
        }
    }




}