﻿using System;
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
    /// written by Scott Boyce, Alex Ciaramella, and Mike Middleton
    /// </summary>
    class AIPlayer : Player
    {
        /// <summary>
        /// we need a way to store previous information for when there are multiple rounds of betting in the same action, ie preFlop we bet they raise we call
        /// this way we dont have to recompute every thing and we can know how much we already bet.
        /// we could also store this object for learning purposes.
        /// </summary>
        PreFlopMultiplierValues preFlopMultiplierValues;

        HandEvaluator handEval;

        //out means the hand is two cards with a difference of one ie 3,4 A,k
        //in is any hand that has 2 cards that could be used in a straight together
        //suited and inStraight are worth a lot less preflop then the other attributes
        //all these attributes will be affected by learning 
        bool suited, pair, lowStraightChance, highStraightChance, highCard, doubleHighCard;
        EvalResult currentHandValue;
        List<Move> possibleMoves;
        List<Player> players;

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
            handEval = new HandEvaluator();

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
            this.possibleMoves = possibleMoves;
            //return new Check();
            throw new NotImplementedException();
            //call appropriate action method based on where we are in the round
        }


        //*****these action methods may be better as a class so we can store information since it is likly there will be multiple rounds of betting 
        //it would probably be better use to classes to store the data then doing it inside this class
        public Move preFlopAction()
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

            Boolean canRaise = false;
            Boolean canCall = false;
            int raiseAmount = 1;
            int callAmount = 1;
            int highestChips = 0;

            // check which player in the list has the highest chips
            // the list of players will contain all other players still in the hand, excluding ourself
            foreach (Player player in players)
            {
                if (player.getChipCount() > highestChips)
                    highestChips = player.getChipCount();
            }

            // check if we are allowed to raise or call this turn
            // if we can, retrieve the minimum amounts associated with these actions
            foreach (Move move in possibleMoves)
            {
                if(move is Raise)
                {
                    raiseAmount = ((Raise)move).getMinimumRaise();
                    canRaise = true;
                }
                else if(move is Call)
                {
                    callAmount = ((Call)move).getCallAmount();
                    canCall = true;
                }
            }

            //very good hand
            if (value > preFlopMultiplierValues.getAverageMultiplier() * 2)
            {
                // we probably want to raise here depending on numbers of players left, our current chip total, and our current chips already in the pot

                // check if our current contribution to the pot is a low percentage of our chip count 
                // check how far off of the highest our contribution is (given in Call if it exists, if it does not then we are the contribution leader)
                // check how many players remain

            }
            //above average playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier() * 1.5)
            {
                // we may want to raise here. most likely want to call assumming the call amount is not a huge percentage of our chips
            }
            //slightly above average playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier() * 1.25)
            {
                // make a small raise if we are currently the chip leader for the given hand or the difference between our chip stacks is small
                
                // check if our stack is at least 75% of the leader's stack
                if(getChipCount()/highestChips > .75)
                {
                    // we can try to bully the opponents
                    // bet 10% of our chips
                    raiseAmount +=  Convert.ToInt32(getChipCount() * .1);
                    return new Raise(raiseAmount);
                }
                // otherwise call or check depending on the call amount
                if (!canCall)
                    return new Check();
                else
                {
                    // if the call amount is less than 10% of our chips, then 
                    if (getChipCount() / callAmount > 20)
                    {
                        return new Call(callAmount);
                    }

                    else
                        return new Fold();
                }
            }
            //playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier())
            {
                // make small calls or check if available
                // fold if the call is too large
                
                // if we are unable to call then we can check for free
                if(!canCall)
                {
                    return new Check();
                }
                // if the call is a small percentage of our chips then make it
                else if(canCall && getChipCount()/callAmount < .05)
                {
                    return new Call(callAmount);
                }
                else
                {
                    return new Fold();
                }
            }
            //currently not a playable hand
            else
            {
                // check if possible. if not then fold
                if (!canCall)
                    return new Check();
                // checking is not an option, fold
                return new Fold();
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
            List<Card> fiveCardHand = new List<Card>();
            fiveCardHand.AddRange(playerHand);
            fiveCardHand.AddRange(CardsOnBoard);
            currentHandValue = handEval.evaluateHand(fiveCardHand);
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
        public int getMostLikelyNextHandValue()
        {
            //Probability probCalc = new Probability();
            //Brute Force -- 1 Card ahead, evaluate all possible Hands
            List<Card> possibleHand;
            Dictionary<int, int> result = new Dictionary<int, int>();
            for(int i = 0; i <= 8; i++)
            {
                result.Add(i, 0);
            }
            EvalResult currentResult;
            foreach (Suit enumSuit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value enumValue in Enum.GetValues(typeof(Value)))
                {
                    possibleHand = playerHand;
                    possibleHand.Add(new Card(enumValue, enumSuit));
                    currentResult = handEval.evaluateHand(possibleHand);
                    result[currentResult.getHandValue()] += 1;
                }
            }
            int mostLikelyHandValue = 0;
            foreach(int key in result.Keys)
            {
                if(result[key] > result[mostLikelyHandValue])
                {
                    mostLikelyHandValue = key;
                }
            }

            return mostLikelyHandValue;
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

