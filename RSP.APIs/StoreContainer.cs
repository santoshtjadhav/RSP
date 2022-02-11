using Redux;
using RSP.Game;

namespace RSP.APIs
{
    public static class StoreContainer
    {
        public static Store<GameState> GameStore { get; set; }

    }
}
