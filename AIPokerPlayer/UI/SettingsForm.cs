using System;
using System.Linq;
using System.Windows.Forms;
using AIPokerPlayer.UI;
using System.Collections.Generic;
using AIPokerPlayer.Players;
using AIPokerPlayer.Poker;

/*
*   AIPokerPlayer.UI.SettingsForm
*   
*   If there is an intialize class, it should instantiate this object.
*   This is the primary class that starts the Poker Application
*   Provides the user with Settings options for players and the game
*/

namespace AIPokerPlayer
{
    public partial class SettingsForm : Form
    {

        private int startingChips;
        private readonly int MAX_PLAYER_COUNT = 8;
        private String[] playerList;
        private String[] playerNameList;
        private TextBox[] textBoxPlayerNames;
        private Panel[] panelGroups;
        private List<Player> players;

        public SettingsForm()
        {
            InitializeComponent();

            textBoxPlayerNames = new TextBox[MAX_PLAYER_COUNT];
            panelGroups = new Panel[MAX_PLAYER_COUNT];
            playerNameList = new String[MAX_PLAYER_COUNT];
            playerList = new String[MAX_PLAYER_COUNT];
            players = new List<Player>();

            //Add TextBoxes to Array for reference
            textBoxPlayerNames[0] = textPlayerOneName;
            textBoxPlayerNames[1] = textPlayerTwoName;
            textBoxPlayerNames[2] = textPlayerThreeName;
            textBoxPlayerNames[3] = textPlayerFourName;
            textBoxPlayerNames[4] = textPlayerFiveName;
            textBoxPlayerNames[5] = textPlayerSixName;
            textBoxPlayerNames[6] = textPlayerSevenName;
            textBoxPlayerNames[7] = textPlayerEightName;

            //Add Panels to Array for reference
            panelGroups[0] = panelPlayerOne;
            panelGroups[1] = panelPlayerTwo;
            panelGroups[2] = panelPlayerThree;
            panelGroups[3] = panelPlayerFour;
            panelGroups[4] = panelPlayerFive;
            panelGroups[5] = panelPlayerSix;
            panelGroups[6] = panelPlayerSeven;
            panelGroups[7] = panelPlayerEight;

            setPlayerList();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //is this necessary?
        }

        public int getStartingChips() { return startingChips; }
        public String[] getPlayerNameList() { return playerNameList; }
        public void setStartingChips() { startingChips = (int)numericStartChips.Value; }
        public String[] getPlayerList() { return playerList; }
        /*
        *   Cycles through TextBoxes and radio buttons to gather player name data, and player type
        *   Returns: void
        */
        public void setPlayerList()
        {
            for (int i = 0; i < MAX_PLAYER_COUNT; i++)
                {
                    playerNameList[i] = textBoxPlayerNames[i].Text;
                    var checkedButton = panelGroups[i].Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
                    playerList[i] = checkedButton.Text;
                    if (playerList[i].Equals(""))//"" Radio Button is actually the Human selection.
                    {
                        playerList[i] = "Human";
                    }
                }
        }

        /*
        *   Finalizes all the settings that will be passed on, by re-updating all of our lists.
        *   Returns: void
        */
        private void finalizeSettings()
        {
            setStartingChips();
            setPlayerList();
            Player tmpPlayer;

            for(int i = 0; i < MAX_PLAYER_COUNT; i++)
            {
                tmpPlayer = null;

                if(playerList[i].Equals("Human"))
                {
                    tmpPlayer = new HumanPlayer(playerNameList[i], startingChips, i);
                }
                else if (playerList[i].Equals("AI"))
                {
                    tmpPlayer = new AIPlayer(playerNameList[i], startingChips, i);
                }
                else
                {
                    //slot was empty, don't add anything to our list
                }

                if(tmpPlayer != null)
                {
                    players.Add(tmpPlayer);
                }
            }
        }

        /*
        *   Will pass all necessary settings data to the next controller.
        *   Hides settings UI
        *   Returns: void
        */
        private void StartGame_Click(object sender, EventArgs e)
        {
            finalizeSettings();
            //Pass 'this' to the next controller object
            Game game = new Game(players);

            //GameForm gameForm = new GameForm();
            //gameForm.Show();
            this.Hide();
        }

        //Player One Radio Events
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerOneName.Text = "Human1";
            this.textPlayerOneName.Enabled = true;
        }

        private void radioPlayerOneEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerOneName.Text = "";
            this.textPlayerOneName.Enabled = false;
        }

        private void radioPlayerOneAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerOneName.Text = "Jim";
            this.textPlayerOneName.Enabled = false;
        }

        //Player Two Radio Events
        private void radioPlayer2Human_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerTwoName.Text = "Human2";
            this.textPlayerTwoName.Enabled = true;
        }

        private void radioPlayerTwoAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerTwoName.Text = "Barry";
            this.textPlayerTwoName.Enabled = false;
        }

        private void radioPlayerTwoEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerTwoName.Text = "";
            this.textPlayerTwoName.Enabled = false;
        }

        //Player Three Radio Events
        private void radioPlayerThreeHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerThreeName.Text = "Human3";
            this.textPlayerThreeName.Enabled = true;
        }

        private void radioPlayerThreeAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerThreeName.Text = "Quinn";
            this.textPlayerThreeName.Enabled = false;
        }

        private void radioPlayerThreeEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerThreeName.Text = "";
            this.textPlayerThreeName.Enabled = false;
        }

        //Player Four Radio Events
        private void radioPlayerFourHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerFourName.Text = "Human4";
            this.textPlayerFourName.Enabled = true;
        }

        private void radioPlayerFourAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerFourName.Text = "Bobby-Joe";
            this.textPlayerFourName.Enabled = false;
        }

        private void radioPlayerFourEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerFourName.Text = "";
            this.textPlayerFourName.Enabled = false;
        }

        //Player Five Radio Events
        private void radioPlayerFiveHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerFiveName.Text = "Human5";
            this.textPlayerFiveName.Enabled = true;
        }

        private void radioPlayerFiveAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerFiveName.Text = "Beth";
            this.textPlayerFiveName.Enabled = false;
        }

        private void radioPlayerFiveEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerFiveName.Text = "";
            this.textPlayerFiveName.Enabled = false;
        }

        //Player Six Radio Events
        private void radioPlayerSixHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerSixName.Text = "Human6";
            this.textPlayerSixName.Enabled = true;
        }

        private void radioPlayerSixAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerSixName.Text = "Slim";
            this.textPlayerSixName.Enabled = false;
        }

        private void radioPlayerSixEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerSixName.Text = "";
            this.textPlayerSixName.Enabled = false;
        }

        //Player Seven Radio Events
        private void radioPlayerSevenHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerSevenName.Text = "Human7";
            this.textPlayerSevenName.Enabled = true;
        }

        private void radioPlayerSevenAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerSevenName.Text = "Two-Toed Tom";
            this.textPlayerSevenName.Enabled = false;
        }

        private void radioPlayerSevenEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerSevenName.Text = "";
            this.textPlayerSevenName.Enabled = false;
        }

        //Player Eight Radio Events
        private void radioPlayerEightHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerEightName.Text = "Human8";
            this.textPlayerEightName.Enabled = true;
        }

        private void radioPlayerEightAI_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerEightName.Text = "Malinda";
            this.textPlayerEightName.Enabled = false;
        }

        private void radioPlayerEightEmpty_CheckedChanged(object sender, EventArgs e)
        {
            this.textPlayerEightName.Text = "";
            this.textPlayerEightName.Enabled = false;
        }
    }
}