using System;
using System.Collections.Generic;
using System.Text;

namespace RSP.Game
{
    internal static class ApplicationData
    {

        public static List<Game> Games; // Static List instance

        static ApplicationData()
        {
            //
            // Allocate the list.
            //
            Games = new List<Game>();
            Games.Add(new Game { GameId="sfsdfsdf"});
        }
    }

}


