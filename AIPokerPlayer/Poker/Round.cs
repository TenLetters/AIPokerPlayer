using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Players;
using AIPokerPlayer.UI;
using AIPokerPlayer.Poker.Cards;

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
        // updates the game UI as needed
        public void playRound(List<Player> players, GameForm gameForm)
        {
            // create a shuffled deck of cards for this round of poker
            Deck deck = new Deck();
        }
    }
}
