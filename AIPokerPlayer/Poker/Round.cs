using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Players;
using AIPokerPlayer.UI;
using AIPokerPlayer.Poker.Cards;

namespace AIPokerPlayer.Poker
{
    // one round of poker
    class Round
    {
        // plays this round of poker with the given players
        // removes players from the list if they have been knocked out (0 chips)
        // updates the game UI as needed
        public Player playRound(List<Player> players, GameForm gameForm, int indexOfBigBlindPlayer, int bigBlindAmount)
        {
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

            // determine the winner of this round after all rounds have finished or only 1 person has not folded
            return determineWinner(players, foldedPlayersPositions);     
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
            result += players[indexOfBigBlindPlayer].modifyChipCount(bigBlindAmount);
            result += players[indexOfSmallBlindPlayer].modifyChipCount(smallBlindAmount);

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
                        // get the players move
                        players[i].requestAction();
                        // if check, do nothing
                        // if fold, add to folded player list, increment playersFoldedThisRound
                        playersFoldedThisRound++;
                        // if raise, take bet and add to pot, set last to move to this player, wasThereARaise = true
                        // if call, take bet and add to pot
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

            foreach (Player player in players)
                player.addCardsToHand(boardCards);
        }

        // determines which hand is the strong from the given player lists
        private Player determineWinner(List<Player> players, List<int> folderPlayerPositions)
        {
            // keep track of the current best hand
            Player winner = null;

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
            return playerOne;
        }
    }
}
