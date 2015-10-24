using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Poker.Cards;

namespace AIPokerPlayer.Players
{
    abstract class Player
    {
        int chipCount; // the player's current chip count
        List<Card> playerHand; // the player's two cards held in their hand

        // get the player's decision for their next move
        public abstract void requestAction();

        // returns the player's chip count
        public int getChipCount()
        {
            return chipCount;
        }

        // increases or decreases the player's chip count depending on if the amount is positive or negative
        public void modifyChipCount(int amount)
        {
            chipCount += amount;
        }
    }
}
