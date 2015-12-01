using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Poker.Cards
{
    // Written by Alex Ciaramella
    class Deck
    {
        // our list of cards
        List<Card> deck;
        // the current index in the deck
        int currentIndex;
        // the state of the game (time for turn, river, or flop)
        int stateOfGame;

        Random random = new Random();

        public Deck()
        {
            // put all of the cards into the deck in order
            deck = new List<Card>();
            createOrderedDeck();
            shuffle();
            stateOfGame = 0;
        }
       

        //Creates a deck ordered by each value in a suit, then moving to the next suit
        private void createOrderedDeck()
        {
            deck = new List<Card>();

            foreach (Suit enumSuit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value enumValue in Enum.GetValues(typeof(Value)))
                {
                    deck.Add(new Card(enumValue, enumSuit));
                }
            }
        }

        //shuffles the deck by using Time-based Random
        private void shuffle()
        {
            Random random = new Random();
            shuffleDeck(random);
        }

        //Overloaded to allow shuffling a deck given seed value, for testing purposes
        private void shuffle(int seed)
        {
            Random random = new Random(seed);
            shuffleDeck(random);
        }

        // shuffle the deck of cards using the Fisher-Yates Shuffle method
        // reset the currentIndex to 0
        private void shuffleDeck(Random random)
        {
            int n = deck.Count;

            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(random.NextDouble() * (n - i));
                Card card = deck[r];
                deck[r] = deck[i];
                deck[i] = card;
            }
            currentIndex = 0;
            
        }

        // returns the next n cards from the top of the deck
        private List<Card> getNextCards(int numberOfCards)
        {
            List<Card> result = new List<Card>();

            // add the cards to our result
            for (int i = currentIndex; i < numberOfCards + currentIndex; i++)
            {
                result.Add(deck[i]);
            }

            // increment our deck counter to signify we have drawn cards
            currentIndex += numberOfCards;

            return result;
        }

        // returns the proper cards based on the state of the game and increments it
        public List<Card> getBoardCards()
        {
            switch (stateOfGame)
            {
                case 0:
                    stateOfGame++;
                    return new List<Card>();
                case 1:
                    stateOfGame++;
                    return getFlop();
                case 2:
                    stateOfGame++;
                    return getTurn();
                case 3:
                    return getRiver();
            }
            return new List<Card>();
        }

        // returns the top 2 cards of the deck for the player's hand
        public List<Card> getNextHand()
        {
            return getNextCards(2);
        }

        // returns the turn cards
        private List<Card> getFlop()
        {
            return getNextCards(3);
        }

        // returns the turn card
        private List<Card> getTurn()
        {
            return getNextCards(1);
        }

        // returns the river card
        private List<Card> getRiver()
        {
            return getNextCards(1);
        }
    }
}
