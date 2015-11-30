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
    class Round
    {

        private HandEvaluator handEval = new HandEvaluator();
        private int highestChipsInPot = 0;
        private GameForm gameForm;

        // plays this round of poker with the given players
        // removes players from the list if they have been knocked out (0 chips)
        // updates the game UI as needed
        public Player playRound(List<Player> players, GameForm gameForm, int indexOfBigBlindPlayer, int bigBlindAmount)
        {
            // save a global reference to the gameform for UI updates
            this.gameForm = gameForm;

            // keep track of which players folded so we don't ask them for another move if their turn comes up again
            List<int> foldedPlayersPositions = new List<int>();

            // create a shuffled deck of cards for this round of poker
            Deck deck = new Deck();

            // calculate the blinds and add them to the pot
            int potAmount = calculateBlinds(players, indexOfBigBlindPlayer, bigBlindAmount);

            // give each player their two starting cards
            dealStartingHands(players, deck);


            // each round has 4 possible betting sessions : pre-flop, post-flop, post-turn, post-river
            for(int i = 0; i < 4; i++)
            {
                // check to make sure there is atleast 2 players who havent folded yet or else we have a winner
                int remainingPlayers = players.Count - foldedPlayersPositions.Count;
                if (remainingPlayers > 1)
                {
                    // add flop, turn, river cards to player hands
                    progressGameState(players, deck);
                    performAllMoves(players, foldedPlayersPositions, remainingPlayers, indexOfBigBlindPlayer);
                }
                else
                    break;
            }

            // reset all chip contributions to the pot for this round to 0
            foreach(Player player in players)
            {
                player.resetChipsInCurrentPot();
            }

            // determine the winner of this round after all rounds have finished or only 1 person has not folded
            Player winner = determineWinner(players, foldedPlayersPositions);
            winner.modifyChipCount(potAmount);

            gameForm.updatePlayerChipCount(winner);
            return winner;  
        }
        
        // calculates the small and large blinds and adds them to the pot
        private int calculateBlinds(List<Player> players, int indexOfBigBlindPlayer, int bigBlindAmount)
        {
            int result = 0;

            // small blind is equal to half of the big blind
            int smallBlindAmount = bigBlindAmount / 2;

            // find position of the small blinds player
            // he is located one seat left of the big blind player
            int indexOfSmallBlindPlayer = findPlayer(players, indexOfBigBlindPlayer, -1);

            // take the small and big blinds from the players
            result += players[indexOfBigBlindPlayer].modifyChipCount(-bigBlindAmount);
            players[indexOfBigBlindPlayer].addToChipsInCurrentPot(bigBlindAmount);
            // the big blind is the most chips by any player in the pot to start
            highestChipsInPot = bigBlindAmount;
            result += players[indexOfSmallBlindPlayer].modifyChipCount(-smallBlindAmount);
            players[indexOfSmallBlindPlayer].addToChipsInCurrentPot(smallBlindAmount);


            // update UI
            gameForm.updatePlayerChipCount(players[indexOfBigBlindPlayer]);
            gameForm.updatePlayerChipCount(players[indexOfSmallBlindPlayer]);
            return result;
        }

        // finds the index of the player sitting seatsAwayFromPlayer seats away from the location of the known player
        // will be n seats to the right seatsAwayFromPlayer is positive, n seats to the left if it is negative
        private int findPlayer(List<Player> players, int indexOfKnownPlayer, int seatsAwayFromKnownPlayer)
        {
            // move over seatsAwayFromPlayer and then use modulus to adjust the number to be between 0-players.count - 1 for a valid seat position
            return (indexOfKnownPlayer + seatsAwayFromKnownPlayer) % players.Count;
        }

        // gives each player in the list 2 cards for their hand
        private void dealStartingHands(List<Player> players, Deck deck)
        {
            foreach(Player player in players)
            {
                player.addCardsToHand(deck.getNextHand());
            }
        }

        // asks each player for the move, starting with the player to the right of the big blind
        private void performAllMoves(List<Player> players, List<int> foldedPlayersPositions, int remainingPlayers, int indexOfBigBlindPlayer)
        {
            // keep track of who is last to move which will change if anyone raises
            int lastToMove = indexOfBigBlindPlayer;
            Boolean wasThereARaise = false;
            // keep track of how many players have made a move this turn
            int moveCount = 0;

            Boolean done = false;

            // loop until we finish all moves and there were no raises
            while (!done)
            {
                int playersFoldedThisRound = 0;
                // loop through all moves
                for (int i = findPlayer(players, indexOfBigBlindPlayer, 1); moveCount < remainingPlayers; i++)
                {
                    // check if this player has already folded
                    if (!foldedPlayersPositions.Contains(i))
                    {
                        // update Ui to show player's cards
                        gameForm.showPlayerHand(players[i]);

                        // check possible moves for the player
                        List<Move> possibleMoves = new List<Move>();
                        // folding is always possible
                        possibleMoves.Add(new Fold());
                        // can only raise if your chip count > minimum raise amount
                        // minimum raise amount is 1 chip (current leading contribution - player contribution = call amount; must have at least that many chips to raise)
                        if (players[i].getChipCount() - highestChipsInPot + players[i].getChipsInCurrentPot() > 0)
                            possibleMoves.Add(new Raise(players[i].getChipCount() - highestChipsInPot));
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
                        // can only check if your chips in pot = maximum contribution
                        if (players[i].getChipsInCurrentPot() == highestChipsInPot)
                            possibleMoves.Add(new Check());

                        // Update the UI with the possible moves for the player
                        gameForm.setAvailableButtons(possibleMoves);

                        // get the players move
                        Move selectedMove = players[i].requestAction(possibleMoves);
                        if (selectedMove is Fold)
                        {
                            // if fold, add to folded player list, increment playersFoldedThisRound
                            foldedPlayersPositions.Add(i);
                            playersFoldedThisRound++;
                        }
                        else if (selectedMove is Raise)
                        {
                            // if raise, take bet and add to pot
                            int raiseAmount = ((Raise)selectedMove).getRaiseAmount();
                            players[i].modifyChipCount(-raiseAmount);
                            players[i].addToChipsInCurrentPot(raiseAmount);
                            // update the pot contribution leader for this round
                            highestChipsInPot = players[i].getChipsInCurrentPot();
                            //change last to move to this player
                            lastToMove = i;
                            // update raise boolean
                            wasThereARaise = true;
                        }
                        else if (selectedMove is Call)
                        {
                            // if call, take bet and add to pot
                            int callAmount = highestChipsInPot - players[i].getChipsInCurrentPot();
                            callAmount = players[i].modifyChipCount(-callAmount);
                            players[i].addToChipsInCurrentPot(callAmount);

                        }
                        // if check, do nothing
                        moveCount++;
                    }
                }
                remainingPlayers -= playersFoldedThisRound;
                // we are done if there were no raises
                done = !wasThereARaise;
            }
            
        }

        // asks the deck for the next set of board cards (flop, turn, river)
        private void progressGameState(List<Player> players, Deck deck)
        {
            List<Card> boardCards = deck.getBoardCards();
            gameForm.revealBoardCards(boardCards);

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
            Player winner = players[0];

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
