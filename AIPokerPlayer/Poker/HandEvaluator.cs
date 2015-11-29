using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPokerPlayer.Poker.Cards;

namespace AIPokerPlayer.Poker
{
    // Written by Alex Ciaramella
    // Assigns a given set of cards a value depending on what sets it contains in Poker (Pair, Flush, etc)
    // returns the highest value set the hand contains and the high card for breaking ties
    class HandEvaluator
    {
        public EvalResult evaluateHand(List<Card> hand)
        {
            // create a new result with high card 2, hand result 0
            EvalResult result = new EvalResult(2, 0);

            // find the value of the high card
            result.setHighCard(findHighCard(hand));

            // check for a stright flush
            return 8;

            // check for four of a kind
            return 7;

            // check for full house
            return 6;

            // check for flush
            return 5;

            // check for straight
            return 4;

            // check for three of a kind
            return 3;

            // check for two pair
            return 2;

            // check for one pair
            return 1;

            // high card
            return 0;
        }

        private int findHighCard(List<Card> hand)
        {
            foreach(Card card in hand)
            {

            }
        }
    }
}
