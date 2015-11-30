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
            createNewUnsortedDeck();
            shuffle();
            stateOfGame = 0;
        }
        
        public void createNewUnsortedDeck()
        {
            deck = new List<Card>();

            // add all 4 twos
            deck.Add(new Card(Value.Two, Suit.Clubs));
            deck.Add(new Card(Value.Two, Suit.Diamonds));
            deck.Add(new Card(Value.Two, Suit.Hearts));
            deck.Add(new Card(Value.Two, Suit.Spades));
            // add all 4 threes
            deck.Add(new Card(Value.Three, Suit.Clubs));
            deck.Add(new Card(Value.Three, Suit.Diamonds));
            deck.Add(new Card(Value.Three, Suit.Hearts));
            deck.Add(new Card(Value.Three, Suit.Spades));
            // add all 4 fours
            deck.Add(new Card(Value.Four, Suit.Clubs));
            deck.Add(new Card(Value.Four, Suit.Diamonds));
            deck.Add(new Card(Value.Four, Suit.Hearts));
            deck.Add(new Card(Value.Four, Suit.Spades));
            // add all 4 fives
            deck.Add(new Card(Value.Five, Suit.Clubs));
            deck.Add(new Card(Value.Five, Suit.Diamonds));
            deck.Add(new Card(Value.Five, Suit.Hearts));
            deck.Add(new Card(Value.Five, Suit.Spades));
            // add all 4 sixes
            deck.Add(new Card(Value.Six, Suit.Clubs));
            deck.Add(new Card(Value.Six, Suit.Diamonds));
            deck.Add(new Card(Value.Six, Suit.Hearts));
            deck.Add(new Card(Value.Six, Suit.Spades));
            // add all 4 sevens
            deck.Add(new Card(Value.Seven, Suit.Clubs));
            deck.Add(new Card(Value.Seven, Suit.Diamonds));
            deck.Add(new Card(Value.Seven, Suit.Hearts));
            deck.Add(new Card(Value.Seven, Suit.Spades));
            // add all 4 eights
            deck.Add(new Card(Value.Eight, Suit.Clubs));
            deck.Add(new Card(Value.Eight, Suit.Diamonds));
            deck.Add(new Card(Value.Eight, Suit.Hearts));
            deck.Add(new Card(Value.Eight, Suit.Spades));
            // add all 4 nines
            deck.Add(new Card(Value.Nine, Suit.Clubs));
            deck.Add(new Card(Value.Nine, Suit.Diamonds));
            deck.Add(new Card(Value.Nine, Suit.Hearts));
            deck.Add(new Card(Value.Nine, Suit.Spades));
            // add all 4 tens
            deck.Add(new Card(Value.Ten, Suit.Clubs));
            deck.Add(new Card(Value.Ten, Suit.Diamonds));
            deck.Add(new Card(Value.Ten, Suit.Hearts));
            deck.Add(new Card(Value.Ten, Suit.Spades));
            // add all 4 jacks
            deck.Add(new Card(Value.Jack, Suit.Clubs));
            deck.Add(new Card(Value.Jack, Suit.Diamonds));
            deck.Add(new Card(Value.Jack, Suit.Hearts));
            deck.Add(new Card(Value.Jack, Suit.Spades));
            // add all 4 queens
            deck.Add(new Card(Value.Queen, Suit.Clubs));
            deck.Add(new Card(Value.Queen, Suit.Diamonds));
            deck.Add(new Card(Value.Queen, Suit.Hearts));
            deck.Add(new Card(Value.Queen, Suit.Spades));
            // add all 4 kings
            deck.Add(new Card(Value.King, Suit.Clubs));
            deck.Add(new Card(Value.King, Suit.Diamonds));
            deck.Add(new Card(Value.King, Suit.Hearts));
            deck.Add(new Card(Value.King, Suit.Spades));
            // add all 4 aces
            deck.Add(new Card(Value.Ace, Suit.Clubs));
            deck.Add(new Card(Value.Ace, Suit.Diamonds));
            deck.Add(new Card(Value.Ace, Suit.Hearts));
            deck.Add(new Card(Value.Ace, Suit.Spades));
        }

        // shuffle the deck of cards using the Fisher-Yates Shuffle method
        // reset the currentIndex to 0
        private void shuffle()
        {
            int n = deck.Count;

            for(int i = 0; i < n; i++)
            {
                int r = i + (int)(random.NextDouble() * (n - i));
                Card card = deck[r];
                deck[r] = deck[i];
                deck[i] = card;
            }
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
