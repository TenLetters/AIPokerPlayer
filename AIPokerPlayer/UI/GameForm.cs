using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIPokerPlayer.Poker.Cards;
using System.Drawing;
using AIPokerPlayer.Players;
using AIPokerPlayer.Poker.Moves;
using AIPokerPlayer.Poker;

/*
*   AIPokerPlayer.UI.GameForm
*   
*   Main gameboard GUI
*   Updates all the visuals by setting images and keeping track of labels
*   Provides the user standard actions such as hit, stay, fold
*   Author: Mike Middleton
*/

namespace AIPokerPlayer.UI
{
    public partial class GameForm : Form
    {
        Game game; //reference to the modal game object

        List<Label> labelPlayerNames; //List of player name labels
        List<Label> labelChipCount; //List of player chip count labels
        List<PictureBox> revealedCards; //The picture boxes for cards that are on the board shown to all players
        List<List<PictureBox>> playerHands; //List of each players hands (their two cards)
        Image DEFAULT_CARDBACK = Image.FromFile("../../Resources/Playing-card-back.jpg");
        Image CARD_PLACEMENT = Image.FromFile("../../Resources/Playing-card-placement.jpg");
        int MAX_PLAYER_COUNT = 8;
        int revealedCardsCount;
        int minimumRaise;

        public GameForm(Game game)
        {
            InitializeComponent();
            this.game = game;
            revealedCardsCount = 0;
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

            numericUpDownRaiseAmount.Maximum = int.MaxValue;
            numericUpDownRaiseAmount.Minimum = 0;

            //Mini test suite
            /*
            Card test1 = new Card(Value.Ace, Suit.Clubs);
            Card test2 = new Card(Value.Eight, Suit.Diamonds);
            Card test3 = new Card(Value.Jack, Suit.Hearts);

            revealCard(test1);
            revealCard(test2);

            clearRevealedCards();

            revealCard(test3);

            Player p = new HumanPlayer("Jim", 1000, 2);

            Card test4 = new Card(Value.Two, Suit.Clubs);
            Card test5 = new Card(Value.Seven, Suit.Diamonds);
            List<Card> testHand = new List<Card>();
            testHand.Add(test4);
            testHand.Add(test5);
            p.addCardsToHand(testHand);

            showPlayerHand(p);
            updatePlayer(p);
            */
            //Game game = new Game()
        }

        /*
        *   Param: List<Player>
        *   Updates visual info for a list of players
        */
        public void updatePlayers(List<Player> playerList)
        {
            //Maximum of Eight Players
            if (playerList.Count <= MAX_PLAYER_COUNT)
            {
                Player current;
                for (int i = 0; i < playerList.Count(); i++)
                {
                    if (playerList[i] != null)
                    {
                        current = playerList[i];
                        updatePlayer(current);
                    }
                    else
                    {
                        //this is an empty player spot, just skip over
                    }
                }
            }
            else
            {
                throw new Exception("Expected no more than " + MAX_PLAYER_COUNT + " player list size. Recieved " + playerList.Count);
            }
        }

        /*
        *   Param: Player
        *   Updates visual info for a single player
        */
        public void updatePlayer(Player player)
        {
            updatePlayerName(player);
            updatePlayerChipCount(player);
        }

        /*
        *   Param: Player
        *   Updates the UI Player Name for a given player.
        */
        public void updatePlayerName(Player player)
        {
            labelPlayerNames[player.getPositionOnBoard()].Text = player.getName();
        }

        /*
        *   Param: Player
        *   Updates a single player's chip count to the amount specified
        */
        public void updatePlayerChipCount(Player player)
        {
            this.Invoke((MethodInvoker)delegate {
                labelChipCount[player.getPositionOnBoard()].Text = player.getChipCount().ToString(); // runs on UI thread
            });
        }

        /*
        *   Param: Player
        *   Shows (player index i)'s hand by updating the respective picture boxes to the images of the given cards
        *   On showing cards, should there be a next button so when multiple humans, play they don't accidently see other players?
        */
        public void showPlayerHand(Player player)
        {
            this.Invoke((MethodInvoker)delegate
            {
                hidePlayerHands();
                //Expecting hand size of generally two cards
                int expectedHandSize = 2;
                List<Card> hand = player.getPlayerHand();
                List<PictureBox> playerHand = playerHands[player.getPositionOnBoard()];
                for (int j = 0; j < expectedHandSize; j++)
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
                playerHands[player.getPositionOnBoard()] = playerHand;
            });
        }

        /*
       *   Param:
       *   Hide all player hands
       */
        public void hidePlayerHands()
        {
            for (int j = 0; j < MAX_PLAYER_COUNT; j++)
            {
                playerHands[j][0].Image = DEFAULT_CARDBACK;
                playerHands[j][1].Image = DEFAULT_CARDBACK;
            }
        }

        /*
        *   Param: Card
        *   Update the picture boxes for the revealed cards.
        */
        private void revealCard(Card card)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (revealedCardsCount <= 4)//Clear the revealed cards otherwise
                {
                    revealedCards[revealedCardsCount].Image = card.getImage();
                    revealedCardsCount++;
                    appendHistory("Revealed: " + card.toString());
                }
                else
                {
                    clearRevealedCards();
                }
            });
        }

        /*
        * Updates the cards on the board.
        */
        public void revealBoardCards(List<Card> cards)
        {
            this.Invoke((MethodInvoker)delegate
            {
                foreach (Card card in cards)
                revealCard(card);
            });
        }

        /*
        *   Param:
        *   Clears the board and shows empty positions for the revealed Cards
        */
        public void clearRevealedCards()
        {
            this.Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < revealedCards.Count; i++)
                {
                    revealedCards[i].Image = CARD_PLACEMENT;
                    revealedCardsCount = 0;
                }
            });
        }

        /*
        *   Param: Player
        *   Update the necessary UI information for a player's turn
        */
        public void updateForPlayerTurn(Player player)
        {
            showPlayerHand(player);
            setPlayerTurn(player);
            setPlayerPotContribution(player);
            setCallAmount(player);
        }

        /*
         *   Param: Player
         *   Update the current player turn to Player's name
         */
        public void setPlayerTurn(Player player)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelPlayerNameTurn.Text = player.getName();
            });

        }

        /*
       *   Param: Player
       *   Update the player's pot contribution
       */
        public void setPlayerPotContribution(Player player)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelPotContributionAmount.Text = player.getChipsInCurrentPot().ToString();
            });

        }

        /*
       *   Param: Player
       *   Update the how much a player would need to pay to call
       */
        public void setCallAmount(Player player)
        {
            this.Invoke((MethodInvoker)delegate
            {
                int difference = game.getCurrentRound().getHighestChipsInPot()-player.getChipsInCurrentPot();
                buttonCall.Text = "Call: " + difference.ToString();
            });

        }

        /*
        *   Param: Player
        *   Update the current big blind player
        */
        public void setBigBlindPlayer(Player player)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelBigBlindPlayerName.Text = player.getName();
            });

        }

        /*
        *   Param: int
        *   Update the pot total
        */
        public void setPotTotal(int i)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelPotTotal.Text = i.ToString();
            });
        }

        /*
        *   Param: int
        *   Update the round count label
        */
        public void setRound(int i)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelRoundCount.Text = i.ToString();
            });
        }

        /*
        *   Param: String
        *   Adds the string to a new line in the history text box
        */
        public void appendHistory(String line)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBoxHistory.AppendText(line + Environment.NewLine);
            });
        }

        /*
        *   Param: List<Move>
        *   Takes a list of moves which will determine what the player's options are for their turn
        */
        public void setAvailableButtons(List<Move> moves)
        {
            disableAllButtons();
            foreach (Move move in moves)
            {
                if (move is Raise)
                {
                    this.Invoke((MethodInvoker)delegate {
                        minimumRaise = ((Raise)move).getMinimumRaise();
                        numericUpDownRaiseAmount.Value = minimumRaise;
                        buttonRaise.Enabled = true;
                        numericUpDownRaiseAmount.Enabled = true;
                    });
                }

                if (move is Call)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        buttonCall.Enabled = true;
                    });
                }

                if (move is Check)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        buttonCheck.Enabled = true;
                    });
                }

                if (move is Fold)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        buttonFold.Enabled = true;
                    });
                }
            }
        }

        /*
        *   Param:
        *   Disable all four buttons: Check, Fold, Call, Raise
        */
        public void disableAllButtons()
        {
            this.Invoke((MethodInvoker)delegate
            {
                buttonRaise.Enabled = false;
            });
            this.Invoke((MethodInvoker)delegate
            {
                buttonCall.Enabled = false;
            });
            this.Invoke((MethodInvoker)delegate
            {
                buttonCheck.Enabled = false;
            });
            this.Invoke((MethodInvoker)delegate
            {
                buttonFold.Enabled = false;
            });
            this.Invoke((MethodInvoker)delegate
            {
                numericUpDownRaiseAmount.Enabled = false;
            });
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        //Quit Button: fully exits the application
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        //New Game Button: creates a new settings form and quits this window
        //We will have to shut down all other threads.
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.shutDownWorkingThreads();
            SettingsForm newGame = new SettingsForm();
            newGame.Show();
            this.Close();
        }

        //Check Button: create Move check and set it for the player
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            Move moveChoice = new Check();
            Player activePlayer = game.getCurrentRound().getActivePlayer();
            if(activePlayer != null)
            {
                activePlayer.setMoveChoice(moveChoice);
            }
            else
            {
                throw new Exception("activePlayer was null while attempting to check in GameForm.");
            }

        }

        //Fold Button:
        private void buttonFold_Click(object sender, EventArgs e)
        {
            Move moveChoice = new Fold();
            Player activePlayer = game.getCurrentRound().getActivePlayer();
            if (activePlayer != null)
            {
                activePlayer.setMoveChoice(moveChoice);
            }
            else
            {
                throw new Exception("activePlayer was null while attempting to fold in GameForm.");
            }
        }

        //Call Button:
        private void buttonCall_Click(object sender, EventArgs e)
        {
            int raiseAmount = 0;
            Move moveChoice = new Call(raiseAmount);
            Player activePlayer = game.getCurrentRound().getActivePlayer();
            if (activePlayer != null)
            {
                activePlayer.setMoveChoice(moveChoice);
            }
            else
            {
                throw new Exception("activePlayer was null while attempting to call in GameForm.");
            }
        }

        //Raise Button:
        private void buttonRaise_Click(object sender, EventArgs e)
        {
            int raiseAmount = (int)numericUpDownRaiseAmount.Value;
            int playerChipCount = game.getCurrentRound().getActivePlayer().getChipCount();
            if (raiseAmount >= minimumRaise)
            {
                if (playerChipCount >= raiseAmount)
                {
                    Raise moveChoice = new Raise(raiseAmount);
                    moveChoice.setRaiseAmount(raiseAmount);
                    Player activePlayer = game.getCurrentRound().getActivePlayer();
                    if (activePlayer != null)
                    {
                        activePlayer.setMoveChoice(moveChoice);
                        minimumRaise = 1;//reset minimum raise for later use
                    }
                    else
                    {
                        throw new Exception("activePlayer was null while attempting to raise in GameForm.");
                    }
                }
                else
                {
                    MessageBox.Show("Your Chips: " + playerChipCount + Environment.NewLine +
                        "Raise Amount: " + raiseAmount + Environment.NewLine + Environment.NewLine +
                        "You do not have enough chips to raise by this much.");
                }
            }
            else
            {
                MessageBox.Show("Your Chips: " + playerChipCount + Environment.NewLine +
                        "Minimum Raise: " + minimumRaise + Environment.NewLine +
                        "Raise Amount: " + raiseAmount + Environment.NewLine + Environment.NewLine +
                        "Your raise amount must be greater than or equal to the Minimum Raise, but less than or equal to Your Chips. By raising by the Minimum Raise you will call that amount and match the current highest raise.");
            }
        }
    }
}
