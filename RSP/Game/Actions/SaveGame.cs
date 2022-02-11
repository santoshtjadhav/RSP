using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redux;

namespace RSP.Game.Actions
{
    public class SaveGame: Redux.IAction
    {
        public SaveGame(Game Game)
        {
            this.Game = Game;
        }
        public Game Game { get; private set; }
    }

    
    
    public class SaveGameEffect : ActionEffect<SaveGame, GameState>
    {
        
        private readonly ILogger<SaveGameEffect> _logger;

        public SaveGameEffect(ILogger<SaveGameEffect> logger)
        {
            _logger = logger;
        }

        public override async Task<IAction> Effect(GameState prevState, SaveGame action)
        {
            _logger.LogInformation("Save Game Effect");
            var GameEntity = ApplicationData.Games.FirstOrDefault(c => c.GameId == action.Game.GameId)
                                 ?? new Game();

            //GameEntity.CompanyName = action.Game.CompanyName;
            //GameEntity.Address = action.Game.Address;
            //GameEntity.City = action.Game.City;
            //GameEntity.ContactName = action.Game.ContactName;
            
            if (GameEntity.GameId == null)
            {
                GameEntity.GameId= Guid.NewGuid().ToString();
                ApplicationData.Games.Add(GameEntity);    
            }


            
            // reload Game
            var Game = ApplicationData.Games
                .Select(c => new Game()
                {
                    //Address = c.Address,
                    //City = c.City,
                    //CompanyName = c.CompanyName,
                    //ContactName = c.ContactName,
                    GameId = c.GameId
                })
                .FirstOrDefault(c => c.GameId == GameEntity.GameId);

            SaveGameSuccessful saveGameSuccessful = new SaveGameSuccessful(Game);
            return await Task.FromResult(saveGameSuccessful);
        }
    }
    
    
}