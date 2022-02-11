using System.Linq;
using Redux;

namespace RSP.Game.Actions
{
    public class OpenGameSuccess : IAction
    {
        public OpenGameSuccess(Game Game, string userId)
        {
            this.Game = Game;
            UserId = userId;
        }
        
        public Game Game { get; private set; }
        public string UserId { get; private set; }
    }
    
    
    public class OpenGameSuccessReducer : ActionReducer<OpenGameSuccess, GameState>
    {
        public override GameState Reducer(GameState state, OpenGameSuccess action)
        {
            var item = state.OpenGames.FirstOrDefault(oc => oc.Game.GameId == action.Game.GameId)
                       ?? new OpenedGame() { Game = action.Game } ;

            if (!item.Users.Contains(action.UserId))
            {
                item.Users.Add(action.UserId);
            }

            if (!state.OpenGames.Contains(item))
            {
                state.OpenGames.Add(item);
            }
            return state;
        }
    }
    
    
}