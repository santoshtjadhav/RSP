using System.Collections.Generic;

namespace RSP.Game
{
    public class GameState 
    {
        /// <summary>
        /// Games that have been opened for editing
        /// </summary>
        public IList<OpenedGame> OpenGames { get; set; } = new List<OpenedGame>();

        /// <summary>
        /// list of all saved Games
        /// </summary>
        public IList<Game> GameList { get; set; } = new List<Game>();
    }


    public class OpenedGame
    {
        public Game Game { get; set; }

        public IList<string> Users { get; set; } = new List<string>();

        public bool IsChanged { get; set; } = false;
    }
    
}