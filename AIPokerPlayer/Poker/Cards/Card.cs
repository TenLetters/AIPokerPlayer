using System;
using System.Drawing;
using System.IO;

namespace AIPokerPlayer.Poker.Cards
{
    // Written by Scott Boyce

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
        public Value value;
        public Suit suit;

        public String getPath() { return path; }
        public Image getImage() { return myImage; }


        public Card(Value value, Suit suit)
        {
            this.value = value;
            this.suit = suit;
            imageLocation += value + "_of_" + suit + ".png";
            using (FileStream myStream = new FileStream(imageLocation, FileMode.Open))
            {
                myImage = Image.FromStream(myStream);
            }
            //myImage = CreateNonIndexedImage(imageLocation);
        }

        public String toString()
        {
            String s = value + " of " + suit;
            return s;
        }
    }

}
