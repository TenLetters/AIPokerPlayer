using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIPokerPlayer.Poker.Cards;
using System.Drawing;
using AIPokerPlayer.Players;

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

        List<Label> labelPlayerNames; //List of player name labels
        List<Label> labelChipCount; //List of player chip count labels
        List<PictureBox> revealedCards; //The picture boxes for cards that are on the board shown to all players
        List<List<PictureBox>> playerHands; //List of each players hands (their two cards)
        Image DEFAULT_CARDBACK = Image.FromFile("../../Resources/Playing-card-back.jpg");
        Image CARD_PLACEMENT = Image.FromFile("../../Resources/Playing-card-placement.jpg");
        int MAX_PLAYER_COUNT = 8;
        int revealedCardsCount;

        public GameForm()
        {
            InitializeComponent();
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
            labelChipCount[player.getPositionOnBoard()].Text = player.getChipCount().ToString();
        }

        /*
        *   Param: Player
        *   Shows (player index i)'s hand by updating the respective picture boxes to the images of the given cards
        *   On showing cards, should there be a next button so when multiple humans, play they don't accidently see other players?
        */
        public void showPlayerHand(Player player)
        {
            //Expecting hand size of generally two cards
            int expectedHandSize = 2;
            List<Card> hand = player.getPlayerHand();
            if (hand.Count <= expectedHandSize)//If < then show card back, if > throw exception
            {
                List<PictureBox> playerHand = playerHands[player.getPositionOnBoard()];
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
                playerHands[player.getPositionOnBoard()] = playerHand;
            }
            else
            {
                throw new Exception("Expected no more than " + expectedHandSize + " hand size. Recieved " + hand.Count);
            }
        }

        /*
        *   Param: Card
        *   Update the picture boxes for the revealed cards.
        */
        public void revealCard(Card card)
        {
            if (revealedCardsCount <= 4)//Clear the revealed cards otherwise
            {
                revealedCards[revealedCardsCount].Image = card.getImage();
                revealedCardsCount++;
            }
            else
            {
                clearRevealedCards();
            }
        }

        /*
        *   Param:
        *   Clears the board and shows empty positions for the revealed Cards
        */
        public void clearRevealedCards()
        {
            for(int i = 0; i < revealedCards.Count; i++)
            { 
                revealedCards[i].Image = CARD_PLACEMENT;
                revealedCardsCount = 0;
            }
        }

        /*
       *   Param: Player
       *   Update the current player turn to Player's name
       */
        public void setPlayerTurn(Player player)
        {
            labelPlayerNameTurn.Text = player.getName();
        }

        /*
        *   Param: int
        *   Update the round count label
        */
        public void setRound(int i)
        {
            labelRoundCount.Text = i.ToString();
        }

        /*
        *   Param: List<Move>
        *   Takes a list of moves which will determine what the player's options are for their turn
        */
       /* public void setAvailableButtons(List<Move> moves)
        {
            foreach(Move move in moves)
            {
                if(move is Raise)
                {
                    buttonRaise.Enabled = true;
                }

                if (move is Call)
                {
                    buttonCall.Enabled = true;
                }

                if (move is Check)
                {
                    buttonCheck.Enabled = true;
                }

                if (move is Fold)
                {
                    buttonFold.Enabled = true;
                }
            }
        }*/

        /*
        *   Param:
        *   Disable all four buttons: Check, Fold, Call, Raise
        */
        public void disableAllButtons()
        {
            buttonRaise.Enabled = false;
            buttonCall.Enabled = false;
            buttonCheck.Enabled = false;
            buttonFold.Enabled = false;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
