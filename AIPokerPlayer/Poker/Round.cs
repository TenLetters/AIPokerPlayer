using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Players;

namespace AIPokerPlayer.Poker
{
    // one round of poker
    class Round
    {
        // the index of the player who is currently the big blind
        // used for betting as well as player turn order
        int bigBlindLocation;

        // plays this round of poker with the given players
        // removes players from the list if they have been knocked out (0 chips)
        public void playRound(List<Player> players)
        {

        }
    }
}
