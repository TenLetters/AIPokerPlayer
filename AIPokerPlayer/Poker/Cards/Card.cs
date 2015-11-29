using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Poker.Cards
{
    // Written by Scott Boyce
    public class Card
    {
        // enum of card value
        public enum Value { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
        // enum of card suit
        public enum Suit { Club, Spade, Heart, Diamond };
        // image string
        String imageLocation;
        public Value value;
        public Suit suit;


        public Card(Value value, string suit)
        {
            this.value = value;
        }
    }

}
