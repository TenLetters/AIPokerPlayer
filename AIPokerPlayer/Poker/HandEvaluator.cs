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
        // the enum starts listing at 0, but our cards start at value = 2 so we must offset in order to obtain the true face value
        private static int VALUE_OFFSET = 2;

        private static int HIGH_CARD = 0;
        private static int ONE_PAIR = 1;
        private static int TWO_PAIR = 2;
        private static int THREE_OF_A_KIND = 3;
        private static int STRAIGHT = 4;
        private static int FLUSH = 5;
        private static int FULL_HOUSE = 6;
        private static int FOUR_OF_A_KIND = 7;
        private static int STRAIGHT_FLUSH = 8;


        public EvalResult evaluateHand(List<Card> hand)
        {
            List<int> cardValues = getIntCardValues(hand);
            Dictionary<int, int> cardValueCounts = getCardValueCounts(cardValues);
            Dictionary<int, int> cardSuitCounts = getCardSuitCounts(hand);
        
            // create a new result with high card 2, hand result of a high card (no special sets found)
            EvalResult result = new EvalResult(2, HIGH_CARD);

            // find the value of the high card
            result.setHighCard(findHighCard(hand));

            // check for four of a kind
            Boolean fourOfAKind = hasCountCards(cardValueCounts, 4);

            // check for three of a kind
            Boolean threeOfAKind = hasCountCards(cardValueCounts, 3);

            // check for one pair
            Boolean onePair = hasCountCards(cardValueCounts, 2);

            // check for full house (three of a kind + two of a kind)
            Boolean fullHouse = threeOfAKind && onePair;

            // check for flush
            Boolean flush = hasFlush(cardSuitCounts);

            // check for straight
            Boolean straight = hasStraight(cardValues);

            // check for a stright flush
            Boolean straightFlush = straight && flush;

            // check for two pair
            Boolean twoPair = hasTwoPair(cardValueCounts);

            if (straightFlush)
                result.setHandValue(STRAIGHT_FLUSH);
            else if (fourOfAKind)
                result.setHandValue(FOUR_OF_A_KIND);
            else if (fullHouse)
                result.setHandValue(FULL_HOUSE);
            else if (flush)
                result.setHandValue(FLUSH);
            else if (straight)
                result.setHandValue(STRAIGHT);
            else if (threeOfAKind)
                result.setHandValue(THREE_OF_A_KIND);
            else if (twoPair)
                result.setHandValue(TWO_PAIR);
            else if (onePair)
                result.setHandValue(ONE_PAIR);

            return result;
                
        }

        // returns true if there are at least 5 cards whose values are in numerically increasing order (ace = 14 or ace = 1)
        public Boolean hasStraight(List<int> cardValues)
        {
            // sort our list in increasing order
           cardValues.Sort();

            int i = 2;

            // if the set contains an ace, then we must check for 1,2,3,4,5 so we should start at 1 instead of 2
            if (cardValues.Contains((int) Value.Ace))
                i = 1;
            // check for 5 sequential cards 
            for (; i <= 10; i++)
            {
                if (cardValues.Contains(i))
                {
                    if (cardValues.Contains(i + 1))
                    {
                        if (cardValues.Contains(i + 2))
                        {
                            if (cardValues.Contains(i + 3))
                            {
                                if (cardValues.Contains(i + 4))
                                    return true;
                            }
                        }
                    }
                }
            }

            return false;

        }

        // returns true if there are at least 5 cards with the same suit
        public Boolean hasFlush(Dictionary<int, int> cardSuitCounts)
        {
            // loop through all 4 suits (keys 0-3)
            for(int i = 0; i < 4; i++)
            {
                if (cardSuitCounts[i] >= 5)
                    return true;
            }

            return false;
        }

        // returns true if there are exactly two values of cards which each have 2 instances in the given hand
        public Boolean hasTwoPair(Dictionary<int, int> cardValueCounts)
        {
            int pairCount = 0;

            for (int i = 2; i <= (int)Value.Ace; i++)
            {
                if (cardValueCounts.ContainsKey(i))
                    if (cardValueCounts[i] == 2)
                        pairCount++;
            }

            return pairCount >= 2;
        }

        // returns true if there are exactly count cards with the same value, false otherwise
        public Boolean hasCountCards(Dictionary<int, int> cardValueCounts, int count)
        {
            for (int i = 2; i <= (int)Value.Ace; i++)
            {
                if (cardValueCounts.ContainsKey(i))
                    if (cardValueCounts[i] == count)
                        return true;
            }

            return false;
        }

        // returns a dictionary which contains the suits of the card (key) with its value being how many times that suit occurs in the given hand (value)
        public Dictionary<int, int> getCardSuitCounts(List<Card> hand)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            int clubCount = 0;
            int diamondCount = 0;
            int heartCount = 0;
            int spadeCount = 0;

            foreach (Card card in hand)
            {
                switch (card.suit)
                {
                    case Suit.Clubs:
                        clubCount++;
                        break;
                    case Suit.Diamonds:
                        diamondCount++;
                        break;
                    case Suit.Hearts:
                        heartCount++;
                        break;
                    case Suit.Spades:
                        spadeCount++;
                        break;
                }
            }
            result.Add((int) Suit.Clubs, clubCount);
            result.Add((int) Suit.Diamonds, diamondCount);
            result.Add((int) Suit.Hearts, heartCount);
            result.Add((int) Suit.Spades, spadeCount);

            return result;
        }

        // returns a dictionary which contains the value of the card (key) with its value being how many times that value occurs in the given hand (value)
        public Dictionary<int,int> getCardValueCounts(List<int> cardValues)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            foreach(int value in cardValues)
            {
                // if the dictionary contains the value as a key already, update the existing count
                if(result.ContainsKey(value))
                    result[value]++;
                // otherwise add the new item to the dictionary with count 1
                else
                    result.Add(value, 1);
            }

            return result;
        }

        // creates a list of int card values
        public List<int> getIntCardValues(List<Card> hand)
        {
            List<int> result = new List<int>();

            foreach (Card card in hand)
            {
                switch (card.value)
                {
                    case Value.Ace:
                        result.Add((int) Value.Ace);
                        break;
                    case Value.King:
                        result.Add((int) Value.King);
                        break;
                    case Value.Queen:
                        result.Add((int) Value.Queen);
                        break;
                    case Value.Jack:
                        result.Add((int) Value.Jack);
                        break;
                    case Value.Ten:
                        result.Add((int) Value.Ten);
                        break;
                    case Value.Nine:
                        result.Add((int) Value.Nine);
                        break;
                    case Value.Eight:
                        result.Add((int) Value.Eight);
                        break;
                    case Value.Seven:
                        result.Add((int) Value.Seven);
                        break;
                    case Value.Six:
                        result.Add((int) Value.Six);
                        break;
                    case Value.Five:
                        result.Add((int) Value.Five);
                        break;
                    case Value.Four:
                        result.Add((int) Value.Four);
                        break;
                    case Value.Three:
                        result.Add((int) Value.Three);
                        break;
                    case Value.Two:
                        result.Add((int) Value.Two);
                        break;
                }
            }
            return result;
        }

        // finds the value of the highest card in the hand (from 2 - 14)
        private int findHighCard(List<Card> hand)
        {
            int highCardValue = 2;
            int currentCardValue = 0;

            foreach(Card card in hand)
            {
                switch(card.value)
                {
                    case Value.Ace:
                         currentCardValue = (int) Value.Ace;
                         break;
                    case Value.King:
                        currentCardValue = (int) Value.King;
                        break;
                    case Value.Queen:
                        currentCardValue = (int) Value.Queen;
                        break;
                    case Value.Jack:
                        currentCardValue = (int) Value.Jack;
                        break;
                    case Value.Ten:
                        currentCardValue = (int) Value.Ten;
                        break;
                    case Value.Nine:
                        currentCardValue = (int) Value.Nine;
                        break;
                    case Value.Eight:
                        currentCardValue = (int) Value.Eight;
                        break;
                    case Value.Seven:
                        currentCardValue = (int) Value.Seven;
                        break;
                    case Value.Six:
                        currentCardValue = (int) Value.Six;
                        break;
                    case Value.Five:
                        currentCardValue = (int) Value.Five;
                        break;
                    case Value.Four:
                        currentCardValue = (int) Value.Four;
                        break;
                    case Value.Three:
                        currentCardValue = (int) Value.Three;
                        break;
                    case Value.Two:
                        currentCardValue = (int) Value.Two;
                        break;
                }

                // if the current card is the new max, update high card value
                if (currentCardValue > highCardValue)
                    highCardValue = currentCardValue;
            }
            // return the highest card value + the offset to obtain its true value (ace = 14, king = 13, ...)
            return highCardValue + VALUE_OFFSET;
        }
    }
}
