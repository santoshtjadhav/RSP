using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redux;

namespace RSP.Game.Actions
{
    public class SaveGameSuccessful : IAction
    {
        public SaveGameSuccessful(Game Game)
        {
            this.Game = Game;
        }
        public Game Game { get; private set; }
    }

     
    public class SaveGameSuccessfulReloadListEffect : ActionEffect<SaveGameSuccessful, GameState>
    {
        private readonly ILogger<SaveGameSuccessfulReloadListEffect> _logger;

        public SaveGameSuccessfulReloadListEffect(ILogger<SaveGameSuccessfulReloadListEffect> logger)
        {
            _logger = logger;
        }

        public override Task<IAction> Effect(GameState prevState, SaveGameSuccessful action)
        {
            _logger.LogInformation("SaveGameSuccessfulReloadListEffect");
            return Task.FromResult<IAction>(new LoadGameListAction());
        }
    }
    
    
    public class SaveGameSuccessfulReducer : ActionReducer<SaveGameSuccessful, GameState>
    {
        public override GameState Reducer(GameState state, SaveGameSuccessful action)
        {
            var prevOc =
                state.OpenGames.FirstOrDefault(oc => oc.Game.GameId == action.Game.GameId)
                ?? throw new Exception($"Game {action.Game.GameId} is not opened");;

            var newOc = new OpenedGame()
            {
                Game = action.Game,
                Users = prevOc.Users,
                IsChanged = false
            };
            state.OpenGames.Remove(prevOc);
            state.OpenGames.Add(newOc);
            return state;
        }
    }
}