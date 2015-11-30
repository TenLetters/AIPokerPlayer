using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Poker.Cards;
using AIPokerPlayer.Poker.Moves;

namespace AIPokerPlayer.Players
{
    // Written by Alex Ciaramella
    public abstract class Player
    {
        int chipCount; // the player's current chip count
        int chipsInCurrentPot;
        int positionOnBoard;
        private string name;
        List<Card> playerHand; // the player's two cards held in their hand

        public Player(string name, int startingChipCount, int position)
        {
            this.chipCount = startingChipCount;
            this.name = name;
            positionOnBoard = position;
            chipsInCurrentPot = 0;
        }

        // get the player's decision for their next move
        public abstract Move requestAction(List<Move> possibleMoves);


        // returns the player's hand
        public List<Card> getPlayerHand()
        {
            return playerHand;
        }

        // adds the 2 cards to the player's starting hand
        public void addCardsToHand(List<Card> cards)
        {
            playerHand = new List<Card>(cards);
        }

        // returns the player's chip count
        public int getChipCount()
        {
            return chipCount;
        }

        public int getPositionOnBoard()
        {
            return positionOnBoard;
        }

        public string getName()
        {
            return name;
        }

        public void addToChipsInCurrentPot(int amount)
        {
            this.chipsInCurrentPot += amount;
        }

        public int getChipsInCurrentPot()
        {
            return chipsInCurrentPot;
        }

        public void resetChipsInCurrentPot()
        {
            this.chipsInCurrentPot = 0;
        }

        // increases or decreases the player's chip count depending on if the amount is positive or negative
        public int modifyChipCount(int amount)
        {
            int result = 0;

            // check if this will put our player in negative chips
            if (amount < 0 && chipCount < Math.Abs(amount))
            {
                result = chipCount;
                chipCount = 0;
            }
            else
            {
                chipCount += amount;
                result = chipCount;
            }

            return result;
        }
    }
}
