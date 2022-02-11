using System;
using System.Linq;
using Redux;

namespace RSP.Game.Actions
{
    public class UpdateGame : IAction
    {
        public UpdateGame(Game Game)
        {
            this.Game = Game;
        }   
        public Game Game { get; private set; }
    }


    public class UpdateGameReducer : ActionReducer<UpdateGame, GameState>
    {
        
        
        public override GameState Reducer(GameState state, UpdateGame action)
        {
            var prevOc = state.OpenGames.FirstOrDefault(oc => oc.Game.GameId == action.Game.GameId) 
                         ?? throw new Exception($"Game {action.Game.GameId} is not opened");
            
            var newOc = new OpenedGame()
            {
                Game = new Game()
                {
                    GameId = action.Game.GameId,
                    //Address = action.Game.Address,
                    //City = action.Game.City,
                    //CompanyName = action.Game.CompanyName,
                    //ContactName = action.Game.ContactName
                },
                Users = prevOc.Users,
                IsChanged = true
            };
            state.OpenGames.Remove(prevOc);
            state.OpenGames.Add(newOc);
            return state;
        }
    }
}