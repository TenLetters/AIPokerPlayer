using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPokerPlayer.Poker.Cards;
using AIPokerPlayer.Poker.Moves;
using AIPokerPlayer.Poker;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace AIPokerPlayer.Players
{
    /// <summary>
    /// written by Scott Boyce
    /// </summary>
    class AIPlayer : Player
    {
        /// <summary>
        /// we need a way to store previous information for when there are multiple rounds of betting in the same action, ie preFlop we bet they raise we call
        /// this way we dont have to recompute every thing and we can know how much we already bet.
        /// we could also store this object for learning purposes.
        /// </summary>
        PreFlopMultiplierValues preFlopMultiplierValues;

        //out means the hand is two cards with a difference of one ie 3,4 A,k
        //in is any hand that has 2 cards that could be used in a straight together
        //suited and inStraight are worth a lot less preflop then the other attributes
        //all these attributes will be affected by learning 
        bool suited, pair, lowStraightChance, highStraightChance, highCard, doubleHighCard;
        EvalResult currentHandValue;

        List<Card> CardsOnBoard;

        public AIPlayer(string name, int startingChipCount, int position)
            : base(name, startingChipCount, position)
        {
            if (File.Exists("PreFlopMultiplierValuesInfo.osl"))
            {
                preFlopMultiplierValues = null;
                //Open the file written above and read values from it.
                Stream stream = File.Open("PreFlopMultiplierValuesInfo.osl", FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                preFlopMultiplierValues = (PreFlopMultiplierValues)bformatter.Deserialize(stream);
                stream.Close();
            }
            else 
            {
                preFlopMultiplierValues = new PreFlopMultiplierValues();
            }

        }

        public void serializeMultiplierValues()
        {
            Stream stream = File.Open("PreFlopMultiplierValuesInfo.osl", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, preFlopMultiplierValues);
            stream.Close();
        }

        public override Move requestAction(List<Move> possibleMoves)
        {
            throw new NotImplementedException();
            //call appropriate action method based on where we are in the round
        }


        //*****these action methods may be better as a class so we can store information since it is likly there will be multiple rounds of betting 
        //it would probably be better use to classes to store the data then doing it inside this class
        public void preFlopAction()
        {
            //Code to set up booleans based on given hand
            //assuming hand holds only 2 cards
            if (playerHand[0].suit == playerHand[1].suit)
            {
                suited = true;
            }
            else if (playerHand[0].value == playerHand[1].value)
            {
                pair = true;
            }
            // get integer values of cards 
            int cardOne = ((int)playerHand[0].value), cardTwo = ((int)playerHand[1].value);
            // if both cards are with in one point and the cards are not with in the last or first few cards in order, this hand has a good chace of becomming a straight
            if ((cardOne == (cardTwo + 1) || cardTwo == (cardOne + 1)) && (cardOne + cardTwo > 3) && (cardOne + cardTwo < 19))
            {
                highStraightChance = true;
            }
            // if the cards are within five numbers of one another or are the Ace Two wrap around then there is a low chace for a straight
            else if (Math.Abs(cardOne - cardTwo) < 5 || (cardOne == 12 && cardTwo == 0) || (cardTwo == 12 && cardOne == 0))
            {
                lowStraightChance = true;
            }
            if (cardOne > 10 || cardTwo > 10)
            {
                highCard = true;
                if (cardOne > 10 && cardTwo > 10)
                {
                    doubleHighCard = true;
                }
            }
            double value = preFlopHandValue();
            //very good hand
            if (value > preFlopMultiplierValues.getAverageMultiplier() * 2)
            {

            }
            //above average playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier() * 1.5)
            {

            }
            //slightly above average playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier() * 1.25)
            {

            }
            //playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier())
            {

            }
            //currently not a playable hand
            else
            {

            }

            //use the value of the hand to decide to call raise check or fold
            //also use this value to determine the number of chips to risk using for raise or calling
            //essentailly the number of chip we think this hand is worth

            //return a move with the desired play
            // need a logic block to deal with the case of multiple rounds of betting durring preflop

            //always "check" if we possible before folding

        }

        /// <summary>
        /// determines the value of a hand before the flop based on the above bool attributes
        /// </summary>
        /// <returns></returns>
        public double preFlopHandValue()
        {
            double value = 1;
            // used bool attributes to assign a hand value, this will be affected by learning
            if (suited)
            {
                value *= preFlopMultiplierValues.getSuitedMultiplier();
            }
            else if (pair)
            {
                value *= preFlopMultiplierValues.getPairMultiplier();
            }
            if (highStraightChance)
            {
                value *= preFlopMultiplierValues.getHighStraightChanceMultiplier();
            }
            if (lowStraightChance)
            {
                value *= preFlopMultiplierValues.getLowStraightChanceMultiplier();
            }
            if (doubleHighCard)
            {
                value *= preFlopMultiplierValues.getDoubleHighCardMultiplier();
            }
            else if (highCard)
            {
                value *= preFlopMultiplierValues.getHighCardMultiplier();
            }
            return value;
        }

        /// <summary>
        /// determines what to do in any round of betting between the flop and the turn
        /// may need hand/ possible actions(moves) as a param/s
        /// will return a move that we want to preform
        /// </summary>
        public void postFlopAction()
        {
            // at this point we have a good idea of what our hand is going to be or can possibly be
            // have a logic block that determines what move to make based on the returns from the 2 methods below
            // may also consider if the player(s) are prone to bluffing
            // may act different based on number of chip or an aggression attribute
            // learning could affect the action taken

        }

        /// <summary>
        /// using our current hand (the five cards we currently see, hand + flop) determine what we have
        /// than compare that to what any other player is likly to have(only has to be done once for any number of players since both players have unknown cards)
        /// **scoring could be affected by learing 
        /// </summary>
        /// <returns>return a rating based on the comparison, greather than 1 we likly have the better hand, less than one we likly have the worse hand </returns>
        public double isOurHandLiklyBetterThanAnyOtherPlayers() { return 0; }

        /// <summary>
        /// use probabilty to determine if it is likely our hand will improve with the comming cards
        /// this methods will probably return a list of possible hands with the probability of drawing to that hand 
        /// </summary>
        public void willOurHandGetBetter()
        {

        }

        /// <summary>
        /// will be similar to post flop action
        /// </summary>
        public void postTurnAction()
        {

        }


        /// <summary>
        /// choose a move to preform after the river card has been shown
        /// </summary>
        public void postRiverAction()
        {
            // Now we know what our best hand is and know that another player either has a good hand or is bluffing since in almost all hands that get here we plan to bet(excluding the possible case were our hand is bad and we keep checkin)
            // again we will use isOurHandLiklyBetterThanAnyOtherPlayers to determine if our hand is better and we will bet accordingly
            // here we must again consider bluffing and our aggression level based on relative chip count(our compared to the players at the table)
        }
    }

}

