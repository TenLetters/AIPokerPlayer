using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Players;
using AIPokerPlayer.UI;
using AIPokerPlayer.Poker.Cards;
using AIPokerPlayer.Poker.Moves;

namespace AIPokerPlayer.Poker
{
    // Written by Alex Ciaramella
    // one round of poker
    public class Round
    {

        private HandEvaluator handEval = new HandEvaluator();
        private int highestChipsInPot = 0;
        private int potAmount = 0;
        private GameForm gameForm;
        private Player activePlayer = null;
        List<int> foldedPlayersPositions = new List<int>();
        int DELAY_SPEED = 500; //Visually in MS, how long we delay AIs for their turns so we can see what they do

        // plays this round of poker with the given players
        // removes players from the list if they have been knocked out (0 chips)
        // updates the game UI as needed
        public Player playRound(List<Player> players, GameForm gameForm, int indexOfBigBlindPlayer, int bigBlindAmount)
        {
            // save a global reference to the gameform for UI updates
            this.gameForm = gameForm;


            // create a shuffled deck of cards for this round of poker
            Deck deck = new Deck();
            

            // calculate the blinds and add them to the pot
            potAmount = calculateBlinds(players, indexOfBigBlindPlayer, bigBlindAmount);
            gameForm.setPotTotal(potAmount);

            // give each player their two starting cards
            dealStartingHands(players, deck);


            //Create the string we show at the end for player hands
            String playerHandString = Environment.NewLine +  "****Player Hands****";
            foreach(Player p in players)
            {
                playerHandString += Environment.NewLine + p.getName() + Environment.NewLine +
                    p.getPlayerHand()[0].toString() + ", " + p.getPlayerHand()[1].toString() + Environment.NewLine;
            }
            playerHandString += "****End of Player Hands****";

            // each round has 4 possible betting sessions : pre-flop, post-flop, post-turn, post-river
            for (int i = 0; i < 4; i++)
            { 
                // check to make sure there is atleast 2 players who havent folded yet or else we have a winner
                int remainingPlayers = players.Count - foldedPlayersPositions.Count;
                if (remainingPlayers > 1)
                {
                    // add flop, turn, river cards to player hands
                    progressGameState(players, deck);
                    System.Threading.Thread.Sleep(DELAY_SPEED);
                    performAllMoves(players, foldedPlayersPositions, remainingPlayers, indexOfBigBlindPlayer);
                }
                else
                    break;
            }


            // determine the winner of this round after all rounds have finished or only 1 person has not folded
            Player winner = determineWinner(players, foldedPlayersPositions);
            // reset all chip contributions to the pot for this round to 0
            foreach(Player player in players)
            {
                player.resetChipsInCurrentPot();
                player.setMoveChoice(null);
                if (player is AIPlayer)
                    {
                        ((AIPlayer)player).learnAndCleanUp(winner);
                    }
            }

            //Round is over, reveal all player hands
            gameForm.appendHistory(playerHandString);

            winner.modifyChipCount(potAmount);

            //Update our history with the round winner information
            List<Card> winningHand = winner.getPlayerHand();
            gameForm.appendHistory(Environment.NewLine + winner.getName() + " has won the round!");
            gameForm.appendHistory("Pot Winnings: " + potAmount);
            gameForm.appendHistory("Winning Hand: " + winningHand[0].toString() + " and " + winningHand[1].toString());

            gameForm.setPotTotal(0);
            gameForm.updatePlayerChipCount(winner);
            gameForm.clearRevealedCards();

            return winner;  
        }

        public Player getActivePlayer() { return activePlayer; }
        public int getPotAmount() { return potAmount; }
        public int getHighestChipsInPot() { return highestChipsInPot; }

        // calculates the small and large blinds and adds them to the pot
        private int calculateBlinds(List<Player> players, int indexOfBigBlindPlayer, int bigBlindAmount)
        {

            // small blind is equal to half of the big blind
            int smallBlindAmount = bigBlindAmount / 2;

            // find position of the small blinds player
            // he is located one seat left of the big blind player
            int indexOfSmallBlindPlayer = findPlayer(players, indexOfBigBlindPlayer, -1);

            // take the small and big blinds from the players
            bigBlindAmount = players[indexOfBigBlindPlayer].modifyChipCount(-bigBlindAmount);
            players[indexOfBigBlindPlayer].addToChipsInCurrentPot(bigBlindAmount);
            //UI for Big Blinds
            gameForm.appendHistory("Round Big Blind: " + bigBlindAmount);
            gameForm.appendHistory(players[indexOfBigBlindPlayer].getName() + " has paid the big blind.");

            smallBlindAmount = players[indexOfSmallBlindPlayer].modifyChipCount(-smallBlindAmount);
            players[indexOfSmallBlindPlayer].addToChipsInCurrentPot(smallBlindAmount);
            //UI for small Blinds
            gameForm.appendHistory("Round Small Blind: " + smallBlindAmount);
            gameForm.appendHistory(players[indexOfSmallBlindPlayer].getName() + " has paid the small blind.");

            if (bigBlindAmount > smallBlindAmount)
                highestChipsInPot = bigBlindAmount;
            else
                highestChipsInPot = smallBlindAmount;

            // update UI
            gameForm.updatePlayerChipCount(players[indexOfBigBlindPlayer]);
            gameForm.updatePlayerChipCount(players[indexOfSmallBlindPlayer]);

            gameForm.appendHistory("");//new line

            return bigBlindAmount + smallBlindAmount;
        }

        // finds the index of the player sitting seatsAwayFromPlayer seats away from the location of the known player
        // will be n seats to the right seatsAwayFromPlayer is positive, n seats to the left if it is negative
        private int findPlayer(List<Player> players, int indexOfKnownPlayer, int seatsAwayFromKnownPlayer)
        {
            // move over seatsAwayFromPlayer and then use modulus to adjust the number to be between 0-players.count - 1 for a valid seat position
            int position = indexOfKnownPlayer + seatsAwayFromKnownPlayer;

            // account for player who have folded
            // keep going in the direction of the seats away variable (left or right) until we find someone who is here
            while (foldedPlayersPositions.Contains(position))
            {
                if (seatsAwayFromKnownPlayer < 0)
                    position--;
                else
                    position++;

                if (position < 0)
                    position += players.Count;
            }

            // modulus operator only works on positive numbers
            if (position < 0)
                position += players.Count;

            return position % (players.Count);
        }

        // gives each player in the list 2 cards for their hand
        private void dealStartingHands(List<Player> players, Deck deck)
        {
            foreach(Player player in players)
            {
                player.addStartingCardsToHand(deck.getNextHand());
            }
        }

        // asks each player for the move, starting with the player to the right of the big blind
        private void performAllMoves(List<Player> players, List<int> foldedPlayersPositions, int remainingPlayers, int indexOfBigBlindPlayer)
        {
            // keep track of who is last to move which will change if anyone raises
            int lastToMove = indexOfBigBlindPlayer;
            int turnsTaken = 0;

            int playersFoldedThisRound = 0;

           
            // loop through all moves
            int i = findPlayer(players, indexOfBigBlindPlayer, 1);
            for (gameForm.setBigBlindPlayer(players[lastToMove]); ; i++)
            {

                if (i > players.Count - 1)
                    i = 0;

                // check if this player has already folded
                if (!foldedPlayersPositions.Contains(i))
                {
                    // update Ui to show player's cards
                    gameForm.updateForPlayerTurn(players[i]);
                    gameForm.appendHistory(players[i].getName() + "'s turn.");

                    activePlayer = players[i];//set the currently active player

                    //Slow down during AI turns
                    System.Threading.Thread.Sleep(DELAY_SPEED);

                    // check possible moves for the player
                    List<Move> possibleMoves = new List<Move>();
                    // folding is always possible
                    possibleMoves.Add(new Fold());
                    // can only raise if your chip count > minimum raise amount
                    // minimum raise amount is 1 chip (current leading contribution - player contribution = call amount; must have at least that many chips to raise)
                    if (players[i].getChipCount() > (highestChipsInPot - players[i].getChipsInCurrentPot()))
                        possibleMoves.Add(new Raise(highestChipsInPot - players[i].getChipsInCurrentPot()));

                    // can only call if your chips > 0 and your contribution != max
                    if (players[i].getChipCount() > 0 && players[i].getChipsInCurrentPot() != highestChipsInPot)
                    {
                        // the call amount is the difference between the highest contribution and the players
                        int callAmount = highestChipsInPot - players[i].getChipsInCurrentPot();
                        // if the difference is greater than the players current chip count, the player must go all in
                        if (callAmount < 0)
                            callAmount = players[i].getChipCount();
                        possibleMoves.Add(new Call(callAmount));
                    }
                    // can only check if you cannot call ((chips != maximum || chips == maximum) and chips == 0)
                    else
                    {
                        possibleMoves.Add(new Check());
                    }

                    // Update the UI with the possible moves for the player
                    gameForm.setAvailableButtons(possibleMoves);

                    // get a list of all players who havent folded and arent the current player
                    List<Player> playersStillInRound = new List<Player>();

                    for (int j = 0; j < players.Count; j++)
                    {
                        if (j != i && !foldedPlayersPositions.Contains(j))
                            playersStillInRound.Add(players[j]);
                    }

                    // get the players move
                    Move selectedMove = null;
                    players[i].setMoveChoice(null);
                    selectedMove = players[i].requestAction(possibleMoves, new List<Player>(playersStillInRound));
                    gameForm.disableAllButtons();
                    if (selectedMove is Fold)
                    {
                        // if fold, add to folded player list, increment playersFoldedThisRound
                        foldedPlayersPositions.Add(i);
                        playersFoldedThisRound++;
                        remainingPlayers--;
                        gameForm.appendHistory(players[i].getName() + " folded.");
                    }
                    else if (selectedMove is Raise)
                    {
                        // if raise, take bet and add to pot
                        int raiseAmount = ((Raise)selectedMove).getRaiseAmount();
                        players[i].modifyChipCount(-raiseAmount);
                        players[i].addToChipsInCurrentPot(raiseAmount);
                        // update the pot contribution leader for this round
                        highestChipsInPot = players[i].getChipsInCurrentPot();
                        potAmount = potAmount + raiseAmount;
                        gameForm.appendHistory(players[i].getName() + " raised by " + raiseAmount + ".");
                        gameForm.appendHistory("Current call total: " + highestChipsInPot + ".");
                        // everyone needs a new turn to react to the raise
                        turnsTaken = 0;
                    }
                    else if (selectedMove is Call)
                    {
                        // if call, take bet and add to pot
                        int callAmount = highestChipsInPot - players[i].getChipsInCurrentPot();
                        //callAmount = players[i].modifyChipCount(-callAmount);
                        players[i].modifyChipCount(-callAmount);
                        players[i].addToChipsInCurrentPot(callAmount);
                        potAmount = potAmount + callAmount;
                        gameForm.appendHistory(players[i].getName() + " called and put " + callAmount + " into the pot.");


                    }
                    else if (selectedMove is Check) // if check, only update history
                    {
                        gameForm.appendHistory(players[i].getName() + " checked.");
                    }
                    
                    turnsTaken++;
                    if (selectedMove == null)//for testing while ai doesn't make a move
                    {
                        gameForm.appendHistory("Move was null. Turn Skipped.");
                    }

                    gameForm.updatePlayerChipCount(players[i]);
                    gameForm.setPotTotal(potAmount);

                    if ((players.Count - foldedPlayersPositions.Count) <= 1)
                    {
                        break;
                    }
                    if (turnsTaken == remainingPlayers)
                        break;
                }
            }

        }

        // asks the deck for the next set of board cards (flop, turn, river)
        private void progressGameState(List<Player> players, Deck deck)
        {
            List<Card> boardCards = deck.getBoardCards();
            if (boardCards.Count > 0)//Special case: only appends a new line for post-flop and beyond for formatting purposes
            {
                gameForm.appendHistory(""); //new line
            }
            gameForm.revealBoardCards(boardCards);
            if (boardCards.Count > 0)//Special case: only appends a new line for post-flop and beyond for formatting purposes
            {
                gameForm.appendHistory(""); //new line
            }

            foreach (Player player in players)
                player.addCardsToHand(boardCards);
        }

        // determines which hand is the strong from the given player lists
        private Player determineWinner(List<Player> players, List<int> folderPlayerPositions)
        {

            // check if only one player remains
            if(players.Count == folderPlayerPositions.Count - 1)
            {
                for (int i = 0; i < players.Count; i++)
                    if (!folderPlayerPositions.Contains(i))
                        return players[i];
            }

            // keep track of the current best hand
            //set to first person's hand who isnt in foldedPlayerPositions
            Player winner = null;
            for (int i = 0; i < players.Count; i++)
            {
                    if (!(folderPlayerPositions.Contains(i)))
                    {
                        winner = players[i];
                    }       
            }

            // check all player's cards for the best hand
            for(int i = 1; i < players.Count; i++)
            {
                // don't check folded player's cards
                if(!folderPlayerPositions.Contains(i))
                {
                    // set winner's hand to the better hand between the two
                    winner = compareHands(winner, players[i]);
                }
            }

            return winner;
        }

        // returns the player who has the stronger hand between the two
        private Player compareHands(Player playerOne, Player playerTwo)
        {
            EvalResult playerOneResult = handEval.evaluateHand(playerOne.getPlayerHand());
            EvalResult playerTwoResult = handEval.evaluateHand(playerTwo.getPlayerHand());

            // the player with the higher move result wins
            if(playerOneResult.getHandValue() != playerTwoResult.getHandValue())
            {
                if(playerTwoResult.getHandValue() > playerOneResult.getHandValue())
                    return playerTwo;
            }
            else // in the case of a tie in hand values, compare high card
            {
                if (playerTwoResult.getHighCard() > playerOneResult.getHighCard())
                    return playerTwo;
            }

            // otherwise player one's value was better so he/she wins
            return playerOne;
        }
    }
}
