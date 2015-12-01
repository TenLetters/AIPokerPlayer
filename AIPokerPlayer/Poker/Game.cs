﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIPokerPlayer.Players;
using AIPokerPlayer.UI;
using System.Threading;
using System.ComponentModel;
using AIPokerPlayer.Poker.Cards;

namespace AIPokerPlayer.Poker
{
    // Written by Scott Boyce and Alex Ciaramella
    public class Game
    {
        // all players who are still in the game with a chip count > 0
        List<Player> activePlayers;

        // how many rounds we have played
        // used to determine when to increase blinds
        int roundCount = 0;

        // a reference to the UI of the game
        GameForm gameForm;

        // holds the starting blind amount for this game equal to the starting chip cound % 100
        int startingBlindAmount = 0;

        Thread thread;

        //The Current Round
        Round currentRound;

        public Game(List<Player> players)
        {
            if (players.Count > 0)
            {
                currentRound = null;
                thread = new Thread(this.play);
                gameForm = new GameForm(this);
                gameForm.Show();
                // set up gameform with player names and starting chip counts
                gameForm.updatePlayers(players);

                this.activePlayers = players;
                startingBlindAmount = players[0].getChipCount() / 100;
                thread.Start();
            }
        }

        //Gets the current round we on
        public Round getCurrentRound() { return currentRound; }
        //get active player list
        public List<Player> getActivePlayerList() { return activePlayers; }

        // keep looping through rounds until a winner is determined
        // returns the winner
        public void play()
        {
            Player roundWinner = activePlayers[0];

            while(activePlayers.Count > 1)
            {
                gameForm.setRound(roundCount+1);//calculations use rounds 0-7 but visually we will start at 1
                gameForm.appendHistory("=======ROUND " + (roundCount + 1) + "=======");
                currentRound = new Round();
                // start a new round(hand) the location of the bigblind player is equal to the round count mod the number of active players the current big blind is equal to the startblind plus an additional starting blind for every 10 rounds played
                roundWinner = currentRound.playRound(activePlayers, gameForm, roundCount % activePlayers.Count, startingBlindAmount * (1 + roundCount/10));
                gameForm.appendHistory("=====END of ROUND " + (roundCount + 1) + "=====");
                roundCount++; 

                // check if anyone has reached 0 chips and remove them from the game
                for (int i = 0; i < activePlayers.Count;)
                {
                    if (activePlayers[i].getChipCount() == 0)
                        activePlayers.RemoveAt(i);
                    else
                        i++;                        
                }
            }

            gameForm.appendHistory(activePlayers[0].getName() + " is the winner of the game!");
            gameForm.appendHistory(activePlayers[0].getName() + " ended with " + activePlayers[0].getChipCount() + " chips.");
        }

        //Currently we only use one extra thread
        public void shutDownWorkingThreads()
        {
            thread.Abort();
        }
    }
}
