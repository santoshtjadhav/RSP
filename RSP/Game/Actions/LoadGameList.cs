using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redux;

namespace RSP.Game.Actions
{

    public class LoadGameListAction : IAction 
    {
    }

    public class LoadGameListCompleteAction : IAction
    {
        public LoadGameListCompleteAction(IList<Game> Games)
        {
            this.Games = Games;
        }
        public IList<Game> Games { get; private set; }
    }
    
    
    public class LoadGameListEffect : ActionEffect<LoadGameListAction, GameState>
    {
       private readonly ILogger<LoadGameListEffect> _logger;

        public LoadGameListEffect(ILogger<LoadGameListEffect> logger)
        {
            
            _logger = logger;
        }

        public override async Task<IAction> Effect(GameState prevState, LoadGameListAction action)
        {
            _logger.LogInformation("Load Game List Effect");
            
            var Games =  ApplicationData.Games
                .OrderBy(c => c.GameId)
                .Select(c => new Game()
                {
                    //Address = c.Address,
                    //City = c.City,
                    //CompanyName = c.CompanyName,
                    //ContactName = c.ContactName,
                    GameId = c.GameId
                }).ToList();
            var returnObj = (IAction)new LoadGameListCompleteAction((IList<Game>)Games);
            return await Task.FromResult(returnObj);
        }
    }
    
    public class LoadGameListCompleteReducer : ActionReducer<LoadGameListCompleteAction, GameState>
    {
        
        public override GameState Reducer(GameState prevState, LoadGameListCompleteAction action)
        {
            return new GameState()
            {
                OpenGames = prevState.OpenGames,
                GameList = action.Games
            };
        }
    }
}