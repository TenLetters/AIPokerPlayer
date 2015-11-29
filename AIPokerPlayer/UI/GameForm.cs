﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIPokerPlayer.Poker.Cards;
using System.Drawing;

/*
*   AIPokerPlayer.UI.GameForm
*   
*   Main gameboard GUI
*   Updates all the visuals by setting images and keeping track of labels
*   Provides the user standard actions such as hit, stay, fold
*/

namespace AIPokerPlayer.UI
{
    public partial class GameForm : Form
    {

        List<Label> labelPlayerNames; //List of player name labels
        List<Label> labelChipCount; //List of player chip count labels
        List<PictureBox> revealedCards; //The picture boxes for cards that are on the board shown to all players
        List<List<PictureBox>> playerHands; //List of each players hands (their two cards)
        Image DEFAULT_CARDBACK = Image.FromFile("../../Resources/Playing-card-back.jpg");

        public GameForm()
        {
            InitializeComponent();
            labelPlayerNames = new List<Label>();
            labelChipCount = new List<Label>();
            revealedCards = new List<PictureBox>();
            playerHands = new List<List<PictureBox>>();
            List<PictureBox> playerHand;

            //Add Labels to labelPlayerNames array for reference
            labelPlayerNames.Add(pOneName);
            labelPlayerNames.Add(pTwoName);
            labelPlayerNames.Add(pThreeName);
            labelPlayerNames.Add(pFourName);
            labelPlayerNames.Add(pFiveName);
            labelPlayerNames.Add(pSixName);
            labelPlayerNames.Add(pSevenName);
            labelPlayerNames.Add(pEightName);

            //Add Labels to labelChipCount array for reference
            labelChipCount.Add(labelPOneChipCount);
            labelChipCount.Add(labelPTwoChipCount);
            labelChipCount.Add(labelPThreeChipCount);
            labelChipCount.Add(labelPFourChipCount);
            labelChipCount.Add(labelPFiveChipCount);
            labelChipCount.Add(labelPSixChipCount);
            labelChipCount.Add(labelPSevenChipCount);
            labelChipCount.Add(labelPEightChipCount);

            //Add pictureBoxes to revealedCards array for reference
            revealedCards.Add(pictureBoxFlopOne);
            revealedCards.Add(pictureBoxFlopTwo);
            revealedCards.Add(pictureBoxFlopThree);
            revealedCards.Add(pictureBoxTurn);
            revealedCards.Add(pictureBoxRiver);

            //Add pictureBoxes to playerHands array for reference
            //Player One
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPOneCardOne);
            playerHand.Add(pictureBoxPOneCardTwo);
            playerHands.Add(playerHand);
            //Player Two
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPTwoCardOne);
            playerHand.Add(pictureBoxPTwoCardTwo);
            playerHands.Add(playerHand);
            //Player Three
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPThreeCardOne);
            playerHand.Add(pictureBoxPThreeCardTwo);
            playerHands.Add(playerHand);
            //Player Four
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPFourCardOne);
            playerHand.Add(pictureBoxPFourCardTwo);
            playerHands.Add(playerHand);
            //Player Five
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPFiveCardOne);
            playerHand.Add(pictureBoxPFiveCardTwo);
            playerHands.Add(playerHand);
            //Player Six
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPSixCardOne);
            playerHand.Add(pictureBoxPSixCardTwo);
            playerHands.Add(playerHand);
            //Player Seven
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPSevenCardOne);
            playerHand.Add(pictureBoxPSevenCardTwo);
            playerHands.Add(playerHand);
            //Player Eight
            playerHand = new List<PictureBox>();
            playerHand.Add(pictureBoxPEightCardOne);
            playerHand.Add(pictureBoxPEightCardTwo);
            playerHands.Add(playerHand);


            //Mini test suite
            /*
            Card test1 = new Card(Value.Ace, Suit.Clubs);
            Card test2 = new Card(Value.Eight, Suit.Diamonds);
            Card test3 = new Card(Value.Jack, Suit.Hearts);

            List<Card> testflop = new List<Card>();
            testflop.Add(test1);
            testflop.Add(test2);
            testflop.Add(test3);

            showFlop(testflop);

            revealedCards[0].Image = test1.getImage();
            revealedCards[1].Image = test2.getImage();
            revealedCards[2].Image = test3.getImage();

            Card test4 = new Card(Value.Two, Suit.Clubs);
            Card test5 = new Card(Value.Seven, Suit.Diamonds);
            List<Card> testHand = new List<Card>();
            testHand.Add(test4);
            testHand.Add(test5);

            showPlayerHand(1, testHand);
            */
        }

        /*
        *   Param: List<String>
        *   Given a list of strings representing each player's name, set all labels respectively
        */
        public void updatePlayerNames(List<String> playerNameList)
        {
            for (int i = 0; i < playerNameList.Count(); i++)
            {
                labelPlayerNames[i].Text = playerNameList[i];
            }
        }

        /*
        *   Param: int position, int chipCount
        *   Updates a single player's chip count to the amount specified
        */
        public void updatePlayerChipCount(int pos, int chipCount)
        {
            labelChipCount[pos].Text = chipCount.ToString();
        }

        /*
        *   Param: List<int>
        *   Given a list of integers representing each player's chip count, set all labels respectively
        */
        public void updateAllChipCounts(List<int> chipCountList)
        {
            for (int i = 0; i < chipCountList.Count(); i++)
            {
                updatePlayerChipCount(i, chipCountList[i]);
            }
        }

        /*
        *   Param: int, List<Card>
        *   Shows (player index i)'s hand by updating the respective picture boxes to the images of the given cards
        *   On showing cards, should there be a next button so when multiple humans, play they don't accidently see other players?
        */
        public void showPlayerHand(int i, List<Card> hand)
        {
            //Expecting hand size of generally two cards
            int expectedHandSize = 2;
            if (hand.Count <= expectedHandSize)//If < then show card back, if > throw exception
            {
                List<PictureBox> playerHand = playerHands[i];
                for (int j = 0; j < hand.Count; j++)
                {
                    if (hand[j] != null)
                    {
                        playerHand[j].Image = hand[j].getImage();
                    }
                    else
                    {
                        playerHand[j].Image = DEFAULT_CARDBACK;
                    }
                }
                playerHands[i] = playerHand;
            }
            else
            {
                throw new Exception("Expected no more than " + expectedHandSize + " hand size. Recieved " + hand.Count);
            }
        }

        /*
        *   Param: int, List<Card>
        *   Update the picture boxes for the first three cards revealed to all players
        */
        public void showFlop(List<Card> flop)
        {
            //The flop contains 3 cards
            int expectedSize = 3;
            if (flop.Count == expectedSize)//If != throw exception
            {
                for (int i = 0; i < flop.Count; i++)
                {
                    if (flop[i] != null)
                    {
                        revealedCards[i].Image = flop[i].getImage();
                    }
                    else
                    {
                        revealedCards[i].Image = DEFAULT_CARDBACK; //this should never happen
                    }
                }
            }
            else
            {
                throw new Exception("Expected " + expectedSize + " cards for the flop. Recieved " + flop.Count);
            }
        }

        /*
        *   Param: Card
        *   Update the picture boxes for the turn card
        */
        public void showTurn(Card turn)
        {
            if (turn != null) //The turn is the 4th card revealed. Pos: 3
            {
                revealedCards[3].Image = turn.getImage();
            }
            else
            {
                revealedCards[3].Image = DEFAULT_CARDBACK; //this should never happen
            }
        }

        /*
        *   Param: Card
        *   Update the picture boxes for the river card
        */
        public void showRiver(Card river)
        {
            if (river != null) //The river is the last card revealed. Pos: 4
            {
                revealedCards[4].Image = river.getImage();
            }
            else
            {
                revealedCards[4].Image = DEFAULT_CARDBACK; //this should never happen
            }
        }


        /*
       *   Param: Player
       *   Update the current player turn to Player's name
       */
        /*public void setPlayerTurn(Player p)
        {
            labelPlayerNameTurn.Text = p.getName();
        }*/

        /*
        *   Param: int
        *   Update the round count label
        */
        public void setRound(int i)
        {
            labelRoundCount.Text = i.ToString();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
