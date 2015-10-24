using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Players;
using AIPokerPlayer.UI;

namespace AIPokerPlayer.Poker
{
    class Game
    {
        // all players who are still in the game with a chip count > 0
        List<Player> activePlayers;

        // how many rounds we have played
        // used to determine when to increase blinds
        int roundCount = 0;

        // a reference to the UI of the game
        GameForm gameForm;

        // keep looping through rounds until a winner is determined
        // returns the winner
        public Player play()
        {
            while(activePlayers.Count > 1)
            {
                new Round().playRound(activePlayers);
            }

            // return the only player left in our list of active players, our winner
            return activePlayers[0];
        }


    }
}
