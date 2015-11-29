using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Poker.Cards
{
    public class Card
    {
        // enum of card value
        enum Value { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
        // enum of card suit
        enum Suit { Club, Spade, Heart, Diamond };
        // image string
        String imageLocation;
        Value value;
        Suit suit;


        public Card(Value value, string suit)
        {
            this.value = value;
        }
    }

}
