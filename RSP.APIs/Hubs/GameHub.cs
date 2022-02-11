using System.Data;
using Microsoft.AspNetCore.SignalR;
using RSP.Game.Actions;
using System.Reactive.Linq;

using RSP.Game;
using Microsoft.AspNetCore.Authorization;

namespace RSP.APIs.Hubs
{
    
    public class GameHub : Hub
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly ILogger<GameHub> _logger;
        private readonly HubSubscriptionManager _hubSubscriptionManager;


        public GameHub(IHubContext<GameHub> hubContext, 
            ILogger<GameHub> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
            _hubSubscriptionManager = new HubSubscriptionManager();

            _logger.LogInformation("New hub instance");
        }


        public async Task OpenGame(string gameId)
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation("hub: OpenGame {gameId} for {connectionId}", gameId, Context.ConnectionId);
            
            // subscribe to store
            var sub = StoreContainer.GameStore
                .Select(state => state.OpenGames.FirstOrDefault(c => c.Game.GameId == gameId))
                .DistinctUntilChanged()
                .Subscribe(async oc => {
                    if (oc == null) return;
                    _logger.LogInformation("hub: game {gameId} changed. calling push", oc.Game.GameId);
                    await _hubContext.Clients.Client(connectionId).SendAsync("PushGame", oc);
                });
            _hubSubscriptionManager.Add(connectionId, "Game", sub);
                     
            // dispatch OpenGame action
            StoreContainer.GameStore.Dispatch(new OpenGame(gameId, $"{Context.User.Identity.Name} - {connectionId}"));       
        }
        

        public async Task CloseGame()
        {
            _hubSubscriptionManager.DisposeAndRemove(Context.ConnectionId, "Game");
        }

        
        public void UpdateGame(RSP.Game.Game game)
        {
            _logger.LogInformation("update game {gameId}", game.GameId);
            StoreContainer.GameStore.Dispatch(new UpdateGame(game));
        }

        public void SaveGame(RSP.Game.Game game)
        {
            _logger.LogInformation("save game {gameId}", game.GameId);
            StoreContainer.GameStore.Dispatch(new SaveGame(game));
        }


        public async Task GameListSubscribe()
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation("GameListSubscribe {connectionId}", connectionId);
            
            // subscribe to store
            var sub = StoreContainer.GameStore
                .Select(state => state.GameList)
                .DistinctUntilChanged()
                .Subscribe(async gameList =>
                {
                    _logger.LogInformation("Pushing Game List");
                    await _hubContext.Clients.Client(connectionId).SendAsync("PushGameList", gameList);
                });
            _hubSubscriptionManager.Add(connectionId, "GameList", sub);
            
            // dispatch load
            // StoreContainer.GameStore.Dispatch(new LoadGameListAction());
        }
        
        
        public async Task GameListUnSubscribe()
        {
            _logger.LogInformation("GameListUnSubscribe {connectionId}", Context.ConnectionId);
            _hubSubscriptionManager.DisposeAndRemove(Context.ConnectionId, "GameList");
        }
        
        
        
        
        
    }
}