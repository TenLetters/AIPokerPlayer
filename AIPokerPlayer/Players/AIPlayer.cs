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
        int numRaisesThisRound;
        int round;

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
            // always start with pre-flop
            round = 0;

        }

        public void serializeMultiplierValues()
        {
            Stream stream = File.Open("PreFlopMultiplierValuesInfo.osl", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, preFlopMultiplierValues);
            stream.Close();
        }

        public override Move requestAction(List<Move> possibleMoves, List<Player> players)
        {
            this.possibleMoves = possibleMoves;
            this.players = players;
            numRaisesThisRound = 0;

            return getMoveBasedOnRound();
            //call appropriate action method based on where we are in the round
        }


        /// <summary>
        /// Returns the pre-flop action for round = 0, post-flop action for round = 1 || round = 2, and post-river action for round =3
        /// </summary>
        /// <returns></returns>
        public Move getMoveBasedOnRound()
        {
            switch (round)
            {
                case 0:
                    {
                        round++;
                        return preFlopAction();
                    }
                case 1:
                    {
                        round++;
                        return postFlopAction();
                    }
                case 2:
                    {
                        round++;
                        return postFlopAction();
                    }
                case 3:
                    {
                        round++;
                        return postRiverAction();
                    }
            }
            // something went wrong and we lost our track of what position in the game we are
            // fold our hand
            return new Fold();
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
                if (players.Count < 3)
                {
                    // since there are so few players, all still in probably have decent hands and will call any bet
                    // raise to put more money into the pot but not a large bet
                    raiseAmount += Convert.ToInt32(getChipCount() * .1);
                }
                else // there are still a lot of players remaining in this round of betting
                {
                    // since there are still a lot of players in this round we will make a larger bet to force out weaker hands that may
                    // end up beating us if they stick around for too long with a lucky flop/river
                    raiseAmount += Convert.ToInt32(getChipCount() * .2);
                }

                // we can be aggressive if we have a solid ranking in the chip leaderboard and if we have not already raised twice this round
                if (getChipCount() / highestChips > .85 && numRaisesThisRound < 3)
                {
                    // we are the chip leader or very close and there are few players remaining
                    // play aggressively
                    numRaisesThisRound++;
                    return new Raise(raiseAmount);
                }
                else
                // either we are not high on the chip leaderboard or we have raised enough this round already
                // check whether we should call or check this hand
                {
                    // if we can check for free and we are not high on chips let's test the waters before diving all in
                    if (!canCall)
                        return new Check();
                    else
                    {
                        // we have a really strong hand and probably cannot afford to give it up and hope for a better one which may not come
                        // even if the call makes us go all in let's try for it since we have a good shot at winning
                        return new Call(callAmount);
                    }
                }
            }
            //above average playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier() * 1.5)
            {
                // we may want to raise here. most likely want to call assumming the call amount is not a huge percentage of our chips

                // if there are only a few players remaining then we should call and try to stay in the pot since our chances are good
                if(players.Count < 3)
                {
                    return new Call(callAmount);
                }
                else
                // if there are a lot of players left then we should raise in order to force out weaker hands
                {
                    // only raise if we havent already this round
                    if (numRaisesThisRound < 1)
                    {
                        raiseAmount += Convert.ToInt32(getChipCount() * .1);
                        numRaisesThisRound++;
                        return new Raise(raiseAmount);
                    }
                    else
                        return new Call(callAmount);
                }
            }
            //slightly above average playable hand
            else if (value > preFlopMultiplierValues.getAverageMultiplier() * 1.25)
            {
                // make a small raise if we are currently the chip leader for the given hand or the difference between our chip stacks is small
                
                // check if our stack is at least 75% of the leader's stack and we have not raised this round yet
                if(getChipCount()/highestChips > .75 && numRaisesThisRound < 1)
                {
                    // we can try to bully the opponents
                    // bet 10% of our chips
                    raiseAmount +=  Convert.ToInt32(getChipCount() * .1);
                    numRaisesThisRound++;
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
        public Move postFlopAction()
        {
            List<Card> fiveCardHand = new List<Card>();
            fiveCardHand.AddRange(playerHand);
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
        public Move postRiverAction()
        {
            
        }

        public void learnAndCleanUp(Player winner)
        {
            if (getName() == winner.getName()) // we won so increment our attributes since they lead to a winning hand
            {
                if (suited) preFlopMultiplierValues.incrementSuitedMultiplier();
                if (pair) preFlopMultiplierValues.incrementPairMultiplier();
                if (highStraightChance) preFlopMultiplierValues.incrementHighStraightChanceMultiplier();
                if (lowStraightChance) preFlopMultiplierValues.incrementLowStraightChanceMultiplier();
                if (highCard) preFlopMultiplierValues.incrementHighCardMultiplier();
                if (doubleHighCard) preFlopMultiplierValues.incrementDoubleHighCardMultiplier();
            }
            else //not the winner decrement our attributes since they did not lead to a winning hand
            {
                if (suited) preFlopMultiplierValues.decrementSuitedMultiplier();
                if (pair) preFlopMultiplierValues.decrementPairMultiplier();
                if (highStraightChance) preFlopMultiplierValues.decrementHighStraightChanceMultiplier();
                if (lowStraightChance) preFlopMultiplierValues.decrementLowStraightChanceMultiplier();
                if (highCard) preFlopMultiplierValues.decrementHighCardMultiplier();
                if (doubleHighCard) preFlopMultiplierValues.decrementDoubleHighCardMultiplier();
            }
            resetRoundBasedVariables();
            serializeMultiplierValues();
        }
        /// <summary>
        /// used to reset round specific variables after each round
        /// </summary>
        public void resetRoundBasedVariables()
        { 
        
        }
    }

}

