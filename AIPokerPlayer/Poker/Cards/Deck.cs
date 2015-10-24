using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Poker.Cards
{
    class Deck
    {
        // our list of cards
        List<Card> deck;
        // the current index in the deck
        int currentIndex;

        public Deck()
        {
            deck = new List<Card>();
            // put all of the cards into the deck in order
            shuffle();
        }

        // shuffle the deck of cards using the Fisher-Yates Shuffle method
        // reset the currentIndex to 0
        private void shuffle()
        {

        }

        // returns the next n cards from the top of the deck
        private List<Card> getNextCards(int numberOfCards)
        {
            List<Card> result = new List<Card>();

            // add the cards to our result
            for (int i = 0; i < numberOfCards; i++)
            {
                result.Add(deck[i]);
            }

            // increment our deck counter to signify we have drawn cards
            currentIndex += numberOfCards;
            
            return result;
        }

        // returns the top 2 cards of the deck for the player's hand
        public List<Card> getNextHand()
        {
            return getNextCards(2);
        }
    }
}
