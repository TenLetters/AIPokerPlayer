using System;
using System.Drawing;

namespace AIPokerPlayer.Poker.Cards
{
    // enum of card value
    public enum Value { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
    // enum of card suit
    public enum Suit { Clubs, Spades, Hearts, Diamonds };

    public class Card
    {   
        //For reference. For embedding resources, we need the app-path
        String path = System.Reflection.Assembly.GetExecutingAssembly().Location;
        // image location relative to our bin/debug/.exe
        String imageLocation = "../../Resources/PNG-deck-cards/";
        Image myImage;
        Value value;
        Suit suit;

        public String getPath() { return path; }
        public Image getImage() { return myImage; }
        

        public Card(Value value, Suit suit)
        {
            this.value = value;
            this.suit = suit;
            imageLocation += value + "_of_" + suit + ".png";
            myImage = Image.FromFile(imageLocation);
        }
    }

}
