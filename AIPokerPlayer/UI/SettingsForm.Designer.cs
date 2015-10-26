namespace AIPokerPlayer
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.GameSettingsLbl = new System.Windows.Forms.Label();
            this.StrtChipLbl = new System.Windows.Forms.Label();
            this.numericStartChips = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPlayerOne = new System.Windows.Forms.Panel();
            this.labelPlayerOne = new System.Windows.Forms.Label();
            this.radioPlayerOneAI = new System.Windows.Forms.RadioButton();
            this.textPlayerOneName = new System.Windows.Forms.TextBox();
            this.radioPlayerOneHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerOneEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerTwo = new System.Windows.Forms.Panel();
            this.labelPlayerTwo = new System.Windows.Forms.Label();
            this.radioPlayerTwoAI = new System.Windows.Forms.RadioButton();
            this.textPlayerTwoName = new System.Windows.Forms.TextBox();
            this.radioPlayer2Human = new System.Windows.Forms.RadioButton();
            this.radioPlayerTwoEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerThree = new System.Windows.Forms.Panel();
            this.labelPlayerThree = new System.Windows.Forms.Label();
            this.radioPlayerThreeAI = new System.Windows.Forms.RadioButton();
            this.textPlayerThreeName = new System.Windows.Forms.TextBox();
            this.radioPlayerThreeHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerThreeEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerFour = new System.Windows.Forms.Panel();
            this.labelPlayerFour = new System.Windows.Forms.Label();
            this.radioPlayerFourAI = new System.Windows.Forms.RadioButton();
            this.textPlayerFourName = new System.Windows.Forms.TextBox();
            this.radioPlayerFourHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerFourEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerFive = new System.Windows.Forms.Panel();
            this.labelPlayerFive = new System.Windows.Forms.Label();
            this.radioPlayerFiveAI = new System.Windows.Forms.RadioButton();
            this.textPlayerFiveName = new System.Windows.Forms.TextBox();
            this.radioPlayerFiveHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerFiveEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerSix = new System.Windows.Forms.Panel();
            this.labelPlayerSix = new System.Windows.Forms.Label();
            this.radioPlayerSixAI = new System.Windows.Forms.RadioButton();
            this.textPlayerSixName = new System.Windows.Forms.TextBox();
            this.radioPlayerSixHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerSixEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerSeven = new System.Windows.Forms.Panel();
            this.labelPlayerSeven = new System.Windows.Forms.Label();
            this.radioPlayerSevenAI = new System.Windows.Forms.RadioButton();
            this.textPlayerSevenName = new System.Windows.Forms.TextBox();
            this.radioPlayerSevenHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerSevenEmpty = new System.Windows.Forms.RadioButton();
            this.panelPlayerEight = new System.Windows.Forms.Panel();
            this.labelPlayerEight = new System.Windows.Forms.Label();
            this.radioPlayerEightAI = new System.Windows.Forms.RadioButton();
            this.textPlayerEightName = new System.Windows.Forms.TextBox();
            this.radioPlayerEightHuman = new System.Windows.Forms.RadioButton();
            this.radioPlayerEightEmpty = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericStartChips)).BeginInit();
            this.panelPlayerOne.SuspendLayout();
            this.panelPlayerTwo.SuspendLayout();
            this.panelPlayerThree.SuspendLayout();
            this.panelPlayerFour.SuspendLayout();
            this.panelPlayerFive.SuspendLayout();
            this.panelPlayerSix.SuspendLayout();
            this.panelPlayerSeven.SuspendLayout();
            this.panelPlayerEight.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(114, 398);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(158, 23);
            this.buttonStartGame.TabIndex = 16;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // GameSettingsLbl
            // 
            this.GameSettingsLbl.AutoSize = true;
            this.GameSettingsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameSettingsLbl.Location = new System.Drawing.Point(109, 9);
            this.GameSettingsLbl.Name = "GameSettingsLbl";
            this.GameSettingsLbl.Size = new System.Drawing.Size(184, 29);
            this.GameSettingsLbl.TabIndex = 17;
            this.GameSettingsLbl.Text = "Game Settings";
            // 
            // StrtChipLbl
            // 
            this.StrtChipLbl.AutoSize = true;
            this.StrtChipLbl.Location = new System.Drawing.Point(77, 353);
            this.StrtChipLbl.Name = "StrtChipLbl";
            this.StrtChipLbl.Size = new System.Drawing.Size(75, 13);
            this.StrtChipLbl.TabIndex = 18;
            this.StrtChipLbl.Text = "Starting Chips:";
            // 
            // numericStartChips
            // 
            this.numericStartChips.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericStartChips.Location = new System.Drawing.Point(175, 351);
            this.numericStartChips.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericStartChips.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericStartChips.Name = "numericStartChips";
            this.numericStartChips.Size = new System.Drawing.Size(120, 20);
            this.numericStartChips.TabIndex = 19;
            this.numericStartChips.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Player Name";
            // 
            // panelPlayerOne
            // 
            this.panelPlayerOne.Controls.Add(this.labelPlayerOne);
            this.panelPlayerOne.Controls.Add(this.radioPlayerOneAI);
            this.panelPlayerOne.Controls.Add(this.textPlayerOneName);
            this.panelPlayerOne.Controls.Add(this.radioPlayerOneHuman);
            this.panelPlayerOne.Controls.Add(this.radioPlayerOneEmpty);
            this.panelPlayerOne.Location = new System.Drawing.Point(15, 64);
            this.panelPlayerOne.Name = "panelPlayerOne";
            this.panelPlayerOne.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerOne.TabIndex = 55;
            // 
            // labelPlayerOne
            // 
            this.labelPlayerOne.AutoSize = true;
            this.labelPlayerOne.Location = new System.Drawing.Point(14, 11);
            this.labelPlayerOne.Name = "labelPlayerOne";
            this.labelPlayerOne.Size = new System.Drawing.Size(62, 13);
            this.labelPlayerOne.TabIndex = 0;
            this.labelPlayerOne.Text = "Player One:";
            // 
            // radioPlayerOneAI
            // 
            this.radioPlayerOneAI.AutoSize = true;
            this.radioPlayerOneAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerOneAI.Name = "radioPlayerOneAI";
            this.radioPlayerOneAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerOneAI.TabIndex = 20;
            this.radioPlayerOneAI.Text = "AI";
            this.radioPlayerOneAI.UseVisualStyleBackColor = true;
            this.radioPlayerOneAI.CheckedChanged += new System.EventHandler(this.radioPlayerOneAI_CheckedChanged);
            // 
            // textPlayerOneName
            // 
            this.textPlayerOneName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerOneName.Name = "textPlayerOneName";
            this.textPlayerOneName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerOneName.TabIndex = 22;
            this.textPlayerOneName.Text = "Human1";
            // 
            // radioPlayerOneHuman
            // 
            this.radioPlayerOneHuman.AutoSize = true;
            this.radioPlayerOneHuman.Checked = true;
            this.radioPlayerOneHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerOneHuman.Name = "radioPlayerOneHuman";
            this.radioPlayerOneHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerOneHuman.TabIndex = 21;
            this.radioPlayerOneHuman.TabStop = true;
            this.radioPlayerOneHuman.UseVisualStyleBackColor = true;
            this.radioPlayerOneHuman.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioPlayerOneEmpty
            // 
            this.radioPlayerOneEmpty.AutoSize = true;
            this.radioPlayerOneEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerOneEmpty.Name = "radioPlayerOneEmpty";
            this.radioPlayerOneEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerOneEmpty.TabIndex = 23;
            this.radioPlayerOneEmpty.Text = "Empty";
            this.radioPlayerOneEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerOneEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerOneEmpty_CheckedChanged);
            // 
            // panelPlayerTwo
            // 
            this.panelPlayerTwo.Controls.Add(this.labelPlayerTwo);
            this.panelPlayerTwo.Controls.Add(this.radioPlayerTwoAI);
            this.panelPlayerTwo.Controls.Add(this.textPlayerTwoName);
            this.panelPlayerTwo.Controls.Add(this.radioPlayer2Human);
            this.panelPlayerTwo.Controls.Add(this.radioPlayerTwoEmpty);
            this.panelPlayerTwo.Location = new System.Drawing.Point(15, 98);
            this.panelPlayerTwo.Name = "panelPlayerTwo";
            this.panelPlayerTwo.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerTwo.TabIndex = 56;
            // 
            // labelPlayerTwo
            // 
            this.labelPlayerTwo.AutoSize = true;
            this.labelPlayerTwo.Location = new System.Drawing.Point(14, 11);
            this.labelPlayerTwo.Name = "labelPlayerTwo";
            this.labelPlayerTwo.Size = new System.Drawing.Size(63, 13);
            this.labelPlayerTwo.TabIndex = 0;
            this.labelPlayerTwo.Text = "Player Two:";
            // 
            // radioPlayerTwoAI
            // 
            this.radioPlayerTwoAI.AutoSize = true;
            this.radioPlayerTwoAI.Checked = true;
            this.radioPlayerTwoAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerTwoAI.Name = "radioPlayerTwoAI";
            this.radioPlayerTwoAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerTwoAI.TabIndex = 20;
            this.radioPlayerTwoAI.TabStop = true;
            this.radioPlayerTwoAI.Text = "AI";
            this.radioPlayerTwoAI.UseVisualStyleBackColor = true;
            this.radioPlayerTwoAI.CheckedChanged += new System.EventHandler(this.radioPlayerTwoAI_CheckedChanged);
            // 
            // textPlayerTwoName
            // 
            this.textPlayerTwoName.Enabled = false;
            this.textPlayerTwoName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerTwoName.Name = "textPlayerTwoName";
            this.textPlayerTwoName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerTwoName.TabIndex = 22;
            this.textPlayerTwoName.Text = "Barry";
            // 
            // radioPlayer2Human
            // 
            this.radioPlayer2Human.AutoSize = true;
            this.radioPlayer2Human.Location = new System.Drawing.Point(79, 11);
            this.radioPlayer2Human.Name = "radioPlayer2Human";
            this.radioPlayer2Human.Size = new System.Drawing.Size(14, 13);
            this.radioPlayer2Human.TabIndex = 21;
            this.radioPlayer2Human.UseVisualStyleBackColor = true;
            this.radioPlayer2Human.CheckedChanged += new System.EventHandler(this.radioPlayer2Human_CheckedChanged);
            // 
            // radioPlayerTwoEmpty
            // 
            this.radioPlayerTwoEmpty.AutoSize = true;
            this.radioPlayerTwoEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerTwoEmpty.Name = "radioPlayerTwoEmpty";
            this.radioPlayerTwoEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerTwoEmpty.TabIndex = 23;
            this.radioPlayerTwoEmpty.Text = "Empty";
            this.radioPlayerTwoEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerTwoEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerTwoEmpty_CheckedChanged);
            // 
            // panelPlayerThree
            // 
            this.panelPlayerThree.Controls.Add(this.labelPlayerThree);
            this.panelPlayerThree.Controls.Add(this.radioPlayerThreeAI);
            this.panelPlayerThree.Controls.Add(this.textPlayerThreeName);
            this.panelPlayerThree.Controls.Add(this.radioPlayerThreeHuman);
            this.panelPlayerThree.Controls.Add(this.radioPlayerThreeEmpty);
            this.panelPlayerThree.Location = new System.Drawing.Point(15, 132);
            this.panelPlayerThree.Name = "panelPlayerThree";
            this.panelPlayerThree.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerThree.TabIndex = 57;
            // 
            // labelPlayerThree
            // 
            this.labelPlayerThree.AutoSize = true;
            this.labelPlayerThree.Location = new System.Drawing.Point(7, 11);
            this.labelPlayerThree.Name = "labelPlayerThree";
            this.labelPlayerThree.Size = new System.Drawing.Size(70, 13);
            this.labelPlayerThree.TabIndex = 0;
            this.labelPlayerThree.Text = "Player Three:";
            // 
            // radioPlayerThreeAI
            // 
            this.radioPlayerThreeAI.AutoSize = true;
            this.radioPlayerThreeAI.Checked = true;
            this.radioPlayerThreeAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerThreeAI.Name = "radioPlayerThreeAI";
            this.radioPlayerThreeAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerThreeAI.TabIndex = 20;
            this.radioPlayerThreeAI.TabStop = true;
            this.radioPlayerThreeAI.Text = "AI";
            this.radioPlayerThreeAI.UseVisualStyleBackColor = true;
            this.radioPlayerThreeAI.CheckedChanged += new System.EventHandler(this.radioPlayerThreeAI_CheckedChanged);
            // 
            // textPlayerThreeName
            // 
            this.textPlayerThreeName.Enabled = false;
            this.textPlayerThreeName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerThreeName.Name = "textPlayerThreeName";
            this.textPlayerThreeName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerThreeName.TabIndex = 22;
            this.textPlayerThreeName.Text = "Quinn";
            // 
            // radioPlayerThreeHuman
            // 
            this.radioPlayerThreeHuman.AutoSize = true;
            this.radioPlayerThreeHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerThreeHuman.Name = "radioPlayerThreeHuman";
            this.radioPlayerThreeHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerThreeHuman.TabIndex = 21;
            this.radioPlayerThreeHuman.UseVisualStyleBackColor = true;
            this.radioPlayerThreeHuman.CheckedChanged += new System.EventHandler(this.radioPlayerThreeHuman_CheckedChanged);
            // 
            // radioPlayerThreeEmpty
            // 
            this.radioPlayerThreeEmpty.AutoSize = true;
            this.radioPlayerThreeEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerThreeEmpty.Name = "radioPlayerThreeEmpty";
            this.radioPlayerThreeEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerThreeEmpty.TabIndex = 23;
            this.radioPlayerThreeEmpty.Text = "Empty";
            this.radioPlayerThreeEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerThreeEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerThreeEmpty_CheckedChanged);
            // 
            // panelPlayerFour
            // 
            this.panelPlayerFour.Controls.Add(this.labelPlayerFour);
            this.panelPlayerFour.Controls.Add(this.radioPlayerFourAI);
            this.panelPlayerFour.Controls.Add(this.textPlayerFourName);
            this.panelPlayerFour.Controls.Add(this.radioPlayerFourHuman);
            this.panelPlayerFour.Controls.Add(this.radioPlayerFourEmpty);
            this.panelPlayerFour.Location = new System.Drawing.Point(15, 166);
            this.panelPlayerFour.Name = "panelPlayerFour";
            this.panelPlayerFour.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerFour.TabIndex = 56;
            // 
            // labelPlayerFour
            // 
            this.labelPlayerFour.AutoSize = true;
            this.labelPlayerFour.Location = new System.Drawing.Point(13, 11);
            this.labelPlayerFour.Name = "labelPlayerFour";
            this.labelPlayerFour.Size = new System.Drawing.Size(63, 13);
            this.labelPlayerFour.TabIndex = 0;
            this.labelPlayerFour.Text = "Player Four:";
            // 
            // radioPlayerFourAI
            // 
            this.radioPlayerFourAI.AutoSize = true;
            this.radioPlayerFourAI.Checked = true;
            this.radioPlayerFourAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerFourAI.Name = "radioPlayerFourAI";
            this.radioPlayerFourAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerFourAI.TabIndex = 20;
            this.radioPlayerFourAI.TabStop = true;
            this.radioPlayerFourAI.Text = "AI";
            this.radioPlayerFourAI.UseVisualStyleBackColor = true;
            this.radioPlayerFourAI.CheckedChanged += new System.EventHandler(this.radioPlayerFourAI_CheckedChanged);
            // 
            // textPlayerFourName
            // 
            this.textPlayerFourName.Enabled = false;
            this.textPlayerFourName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerFourName.Name = "textPlayerFourName";
            this.textPlayerFourName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerFourName.TabIndex = 22;
            this.textPlayerFourName.Text = "Bobby-Joe";
            // 
            // radioPlayerFourHuman
            // 
            this.radioPlayerFourHuman.AutoSize = true;
            this.radioPlayerFourHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerFourHuman.Name = "radioPlayerFourHuman";
            this.radioPlayerFourHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerFourHuman.TabIndex = 21;
            this.radioPlayerFourHuman.UseVisualStyleBackColor = true;
            this.radioPlayerFourHuman.CheckedChanged += new System.EventHandler(this.radioPlayerFourHuman_CheckedChanged);
            // 
            // radioPlayerFourEmpty
            // 
            this.radioPlayerFourEmpty.AutoSize = true;
            this.radioPlayerFourEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerFourEmpty.Name = "radioPlayerFourEmpty";
            this.radioPlayerFourEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerFourEmpty.TabIndex = 23;
            this.radioPlayerFourEmpty.Text = "Empty";
            this.radioPlayerFourEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerFourEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerFourEmpty_CheckedChanged);
            // 
            // panelPlayerFive
            // 
            this.panelPlayerFive.Controls.Add(this.labelPlayerFive);
            this.panelPlayerFive.Controls.Add(this.radioPlayerFiveAI);
            this.panelPlayerFive.Controls.Add(this.textPlayerFiveName);
            this.panelPlayerFive.Controls.Add(this.radioPlayerFiveHuman);
            this.panelPlayerFive.Controls.Add(this.radioPlayerFiveEmpty);
            this.panelPlayerFive.Location = new System.Drawing.Point(15, 200);
            this.panelPlayerFive.Name = "panelPlayerFive";
            this.panelPlayerFive.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerFive.TabIndex = 56;
            // 
            // labelPlayerFive
            // 
            this.labelPlayerFive.AutoSize = true;
            this.labelPlayerFive.Location = new System.Drawing.Point(15, 11);
            this.labelPlayerFive.Name = "labelPlayerFive";
            this.labelPlayerFive.Size = new System.Drawing.Size(62, 13);
            this.labelPlayerFive.TabIndex = 0;
            this.labelPlayerFive.Text = "Player Five:";
            // 
            // radioPlayerFiveAI
            // 
            this.radioPlayerFiveAI.AutoSize = true;
            this.radioPlayerFiveAI.Checked = true;
            this.radioPlayerFiveAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerFiveAI.Name = "radioPlayerFiveAI";
            this.radioPlayerFiveAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerFiveAI.TabIndex = 20;
            this.radioPlayerFiveAI.TabStop = true;
            this.radioPlayerFiveAI.Text = "AI";
            this.radioPlayerFiveAI.UseVisualStyleBackColor = true;
            this.radioPlayerFiveAI.CheckedChanged += new System.EventHandler(this.radioPlayerFiveAI_CheckedChanged);
            // 
            // textPlayerFiveName
            // 
            this.textPlayerFiveName.Enabled = false;
            this.textPlayerFiveName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerFiveName.Name = "textPlayerFiveName";
            this.textPlayerFiveName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerFiveName.TabIndex = 22;
            this.textPlayerFiveName.Text = "Beth";
            // 
            // radioPlayerFiveHuman
            // 
            this.radioPlayerFiveHuman.AutoSize = true;
            this.radioPlayerFiveHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerFiveHuman.Name = "radioPlayerFiveHuman";
            this.radioPlayerFiveHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerFiveHuman.TabIndex = 21;
            this.radioPlayerFiveHuman.UseVisualStyleBackColor = true;
            this.radioPlayerFiveHuman.CheckedChanged += new System.EventHandler(this.radioPlayerFiveHuman_CheckedChanged);
            // 
            // radioPlayerFiveEmpty
            // 
            this.radioPlayerFiveEmpty.AutoSize = true;
            this.radioPlayerFiveEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerFiveEmpty.Name = "radioPlayerFiveEmpty";
            this.radioPlayerFiveEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerFiveEmpty.TabIndex = 23;
            this.radioPlayerFiveEmpty.Text = "Empty";
            this.radioPlayerFiveEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerFiveEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerFiveEmpty_CheckedChanged);
            // 
            // panelPlayerSix
            // 
            this.panelPlayerSix.Controls.Add(this.labelPlayerSix);
            this.panelPlayerSix.Controls.Add(this.radioPlayerSixAI);
            this.panelPlayerSix.Controls.Add(this.textPlayerSixName);
            this.panelPlayerSix.Controls.Add(this.radioPlayerSixHuman);
            this.panelPlayerSix.Controls.Add(this.radioPlayerSixEmpty);
            this.panelPlayerSix.Location = new System.Drawing.Point(15, 234);
            this.panelPlayerSix.Name = "panelPlayerSix";
            this.panelPlayerSix.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerSix.TabIndex = 56;
            // 
            // labelPlayerSix
            // 
            this.labelPlayerSix.AutoSize = true;
            this.labelPlayerSix.Location = new System.Drawing.Point(20, 11);
            this.labelPlayerSix.Name = "labelPlayerSix";
            this.labelPlayerSix.Size = new System.Drawing.Size(56, 13);
            this.labelPlayerSix.TabIndex = 0;
            this.labelPlayerSix.Text = "Player Six:";
            // 
            // radioPlayerSixAI
            // 
            this.radioPlayerSixAI.AutoSize = true;
            this.radioPlayerSixAI.Checked = true;
            this.radioPlayerSixAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerSixAI.Name = "radioPlayerSixAI";
            this.radioPlayerSixAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerSixAI.TabIndex = 20;
            this.radioPlayerSixAI.TabStop = true;
            this.radioPlayerSixAI.Text = "AI";
            this.radioPlayerSixAI.UseVisualStyleBackColor = true;
            this.radioPlayerSixAI.CheckedChanged += new System.EventHandler(this.radioPlayerSixAI_CheckedChanged);
            // 
            // textPlayerSixName
            // 
            this.textPlayerSixName.Enabled = false;
            this.textPlayerSixName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerSixName.Name = "textPlayerSixName";
            this.textPlayerSixName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerSixName.TabIndex = 22;
            this.textPlayerSixName.Text = "Slim";
            // 
            // radioPlayerSixHuman
            // 
            this.radioPlayerSixHuman.AutoSize = true;
            this.radioPlayerSixHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerSixHuman.Name = "radioPlayerSixHuman";
            this.radioPlayerSixHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerSixHuman.TabIndex = 21;
            this.radioPlayerSixHuman.UseVisualStyleBackColor = true;
            this.radioPlayerSixHuman.CheckedChanged += new System.EventHandler(this.radioPlayerSixHuman_CheckedChanged);
            // 
            // radioPlayerSixEmpty
            // 
            this.radioPlayerSixEmpty.AutoSize = true;
            this.radioPlayerSixEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerSixEmpty.Name = "radioPlayerSixEmpty";
            this.radioPlayerSixEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerSixEmpty.TabIndex = 23;
            this.radioPlayerSixEmpty.Text = "Empty";
            this.radioPlayerSixEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerSixEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerSixEmpty_CheckedChanged);
            // 
            // panelPlayerSeven
            // 
            this.panelPlayerSeven.Controls.Add(this.labelPlayerSeven);
            this.panelPlayerSeven.Controls.Add(this.radioPlayerSevenAI);
            this.panelPlayerSeven.Controls.Add(this.textPlayerSevenName);
            this.panelPlayerSeven.Controls.Add(this.radioPlayerSevenHuman);
            this.panelPlayerSeven.Controls.Add(this.radioPlayerSevenEmpty);
            this.panelPlayerSeven.Location = new System.Drawing.Point(15, 268);
            this.panelPlayerSeven.Name = "panelPlayerSeven";
            this.panelPlayerSeven.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerSeven.TabIndex = 56;
            // 
            // labelPlayerSeven
            // 
            this.labelPlayerSeven.AutoSize = true;
            this.labelPlayerSeven.Location = new System.Drawing.Point(3, 11);
            this.labelPlayerSeven.Name = "labelPlayerSeven";
            this.labelPlayerSeven.Size = new System.Drawing.Size(73, 13);
            this.labelPlayerSeven.TabIndex = 0;
            this.labelPlayerSeven.Text = "Player Seven:";
            // 
            // radioPlayerSevenAI
            // 
            this.radioPlayerSevenAI.AutoSize = true;
            this.radioPlayerSevenAI.Checked = true;
            this.radioPlayerSevenAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerSevenAI.Name = "radioPlayerSevenAI";
            this.radioPlayerSevenAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerSevenAI.TabIndex = 20;
            this.radioPlayerSevenAI.TabStop = true;
            this.radioPlayerSevenAI.Text = "AI";
            this.radioPlayerSevenAI.UseVisualStyleBackColor = true;
            this.radioPlayerSevenAI.CheckedChanged += new System.EventHandler(this.radioPlayerSevenAI_CheckedChanged);
            // 
            // textPlayerSevenName
            // 
            this.textPlayerSevenName.Enabled = false;
            this.textPlayerSevenName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerSevenName.Name = "textPlayerSevenName";
            this.textPlayerSevenName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerSevenName.TabIndex = 22;
            this.textPlayerSevenName.Text = "Two-Toed Tom";
            // 
            // radioPlayerSevenHuman
            // 
            this.radioPlayerSevenHuman.AutoSize = true;
            this.radioPlayerSevenHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerSevenHuman.Name = "radioPlayerSevenHuman";
            this.radioPlayerSevenHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerSevenHuman.TabIndex = 21;
            this.radioPlayerSevenHuman.UseVisualStyleBackColor = true;
            this.radioPlayerSevenHuman.CheckedChanged += new System.EventHandler(this.radioPlayerSevenHuman_CheckedChanged);
            // 
            // radioPlayerSevenEmpty
            // 
            this.radioPlayerSevenEmpty.AutoSize = true;
            this.radioPlayerSevenEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerSevenEmpty.Name = "radioPlayerSevenEmpty";
            this.radioPlayerSevenEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerSevenEmpty.TabIndex = 23;
            this.radioPlayerSevenEmpty.Text = "Empty";
            this.radioPlayerSevenEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerSevenEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerSevenEmpty_CheckedChanged);
            // 
            // panelPlayerEight
            // 
            this.panelPlayerEight.Controls.Add(this.labelPlayerEight);
            this.panelPlayerEight.Controls.Add(this.radioPlayerEightAI);
            this.panelPlayerEight.Controls.Add(this.textPlayerEightName);
            this.panelPlayerEight.Controls.Add(this.radioPlayerEightHuman);
            this.panelPlayerEight.Controls.Add(this.radioPlayerEightEmpty);
            this.panelPlayerEight.Location = new System.Drawing.Point(15, 302);
            this.panelPlayerEight.Name = "panelPlayerEight";
            this.panelPlayerEight.Size = new System.Drawing.Size(378, 32);
            this.panelPlayerEight.TabIndex = 56;
            // 
            // labelPlayerEight
            // 
            this.labelPlayerEight.AutoSize = true;
            this.labelPlayerEight.Location = new System.Drawing.Point(10, 11);
            this.labelPlayerEight.Name = "labelPlayerEight";
            this.labelPlayerEight.Size = new System.Drawing.Size(66, 13);
            this.labelPlayerEight.TabIndex = 0;
            this.labelPlayerEight.Text = "Player Eight:";
            // 
            // radioPlayerEightAI
            // 
            this.radioPlayerEightAI.AutoSize = true;
            this.radioPlayerEightAI.Checked = true;
            this.radioPlayerEightAI.Location = new System.Drawing.Point(205, 9);
            this.radioPlayerEightAI.Name = "radioPlayerEightAI";
            this.radioPlayerEightAI.Size = new System.Drawing.Size(35, 17);
            this.radioPlayerEightAI.TabIndex = 20;
            this.radioPlayerEightAI.TabStop = true;
            this.radioPlayerEightAI.Text = "AI";
            this.radioPlayerEightAI.UseVisualStyleBackColor = true;
            this.radioPlayerEightAI.CheckedChanged += new System.EventHandler(this.radioPlayerEightAI_CheckedChanged);
            // 
            // textPlayerEightName
            // 
            this.textPlayerEightName.Enabled = false;
            this.textPlayerEightName.Location = new System.Drawing.Point(99, 8);
            this.textPlayerEightName.Name = "textPlayerEightName";
            this.textPlayerEightName.Size = new System.Drawing.Size(100, 20);
            this.textPlayerEightName.TabIndex = 22;
            this.textPlayerEightName.Text = "Malinda";
            // 
            // radioPlayerEightHuman
            // 
            this.radioPlayerEightHuman.AutoSize = true;
            this.radioPlayerEightHuman.Location = new System.Drawing.Point(79, 11);
            this.radioPlayerEightHuman.Name = "radioPlayerEightHuman";
            this.radioPlayerEightHuman.Size = new System.Drawing.Size(14, 13);
            this.radioPlayerEightHuman.TabIndex = 21;
            this.radioPlayerEightHuman.UseVisualStyleBackColor = true;
            this.radioPlayerEightHuman.CheckedChanged += new System.EventHandler(this.radioPlayerEightHuman_CheckedChanged);
            // 
            // radioPlayerEightEmpty
            // 
            this.radioPlayerEightEmpty.AutoSize = true;
            this.radioPlayerEightEmpty.Location = new System.Drawing.Point(245, 9);
            this.radioPlayerEightEmpty.Name = "radioPlayerEightEmpty";
            this.radioPlayerEightEmpty.Size = new System.Drawing.Size(54, 17);
            this.radioPlayerEightEmpty.TabIndex = 23;
            this.radioPlayerEightEmpty.Text = "Empty";
            this.radioPlayerEightEmpty.UseVisualStyleBackColor = true;
            this.radioPlayerEightEmpty.CheckedChanged += new System.EventHandler(this.radioPlayerEightEmpty_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "label2";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 435);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelPlayerEight);
            this.Controls.Add(this.panelPlayerSeven);
            this.Controls.Add(this.panelPlayerSix);
            this.Controls.Add(this.panelPlayerFive);
            this.Controls.Add(this.panelPlayerFour);
            this.Controls.Add(this.panelPlayerThree);
            this.Controls.Add(this.panelPlayerTwo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericStartChips);
            this.Controls.Add(this.StrtChipLbl);
            this.Controls.Add(this.GameSettingsLbl);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.panelPlayerOne);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericStartChips)).EndInit();
            this.panelPlayerOne.ResumeLayout(false);
            this.panelPlayerOne.PerformLayout();
            this.panelPlayerTwo.ResumeLayout(false);
            this.panelPlayerTwo.PerformLayout();
            this.panelPlayerThree.ResumeLayout(false);
            this.panelPlayerThree.PerformLayout();
            this.panelPlayerFour.ResumeLayout(false);
            this.panelPlayerFour.PerformLayout();
            this.panelPlayerFive.ResumeLayout(false);
            this.panelPlayerFive.PerformLayout();
            this.panelPlayerSix.ResumeLayout(false);
            this.panelPlayerSix.PerformLayout();
            this.panelPlayerSeven.ResumeLayout(false);
            this.panelPlayerSeven.PerformLayout();
            this.panelPlayerEight.ResumeLayout(false);
            this.panelPlayerEight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Label GameSettingsLbl;
        private System.Windows.Forms.Label StrtChipLbl;
        private System.Windows.Forms.NumericUpDown numericStartChips;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPlayerOne;
        private System.Windows.Forms.RadioButton radioPlayerOneEmpty;
        private System.Windows.Forms.TextBox textPlayerOneName;
        private System.Windows.Forms.RadioButton radioPlayerOneHuman;
        private System.Windows.Forms.RadioButton radioPlayerOneAI;
        private System.Windows.Forms.Label labelPlayerOne;
        private System.Windows.Forms.Panel panelPlayerTwo;
        private System.Windows.Forms.Label labelPlayerTwo;
        private System.Windows.Forms.RadioButton radioPlayerTwoAI;
        private System.Windows.Forms.TextBox textPlayerTwoName;
        private System.Windows.Forms.RadioButton radioPlayer2Human;
        private System.Windows.Forms.RadioButton radioPlayerTwoEmpty;
        private System.Windows.Forms.Panel panelPlayerThree;
        private System.Windows.Forms.Label labelPlayerThree;
        private System.Windows.Forms.RadioButton radioPlayerThreeAI;
        private System.Windows.Forms.TextBox textPlayerThreeName;
        private System.Windows.Forms.RadioButton radioPlayerThreeHuman;
        private System.Windows.Forms.RadioButton radioPlayerThreeEmpty;
        private System.Windows.Forms.Panel panelPlayerFour;
        private System.Windows.Forms.Label labelPlayerFour;
        private System.Windows.Forms.RadioButton radioPlayerFourAI;
        private System.Windows.Forms.TextBox textPlayerFourName;
        private System.Windows.Forms.RadioButton radioPlayerFourHuman;
        private System.Windows.Forms.RadioButton radioPlayerFourEmpty;
        private System.Windows.Forms.Panel panelPlayerFive;
        private System.Windows.Forms.Label labelPlayerFive;
        private System.Windows.Forms.RadioButton radioPlayerFiveAI;
        private System.Windows.Forms.TextBox textPlayerFiveName;
        private System.Windows.Forms.RadioButton radioPlayerFiveHuman;
        private System.Windows.Forms.RadioButton radioPlayerFiveEmpty;
        private System.Windows.Forms.Panel panelPlayerSix;
        private System.Windows.Forms.Label labelPlayerSix;
        private System.Windows.Forms.RadioButton radioPlayerSixAI;
        private System.Windows.Forms.TextBox textPlayerSixName;
        private System.Windows.Forms.RadioButton radioPlayerSixHuman;
        private System.Windows.Forms.RadioButton radioPlayerSixEmpty;
        private System.Windows.Forms.Panel panelPlayerSeven;
        private System.Windows.Forms.Label labelPlayerSeven;
        private System.Windows.Forms.RadioButton radioPlayerSevenAI;
        private System.Windows.Forms.TextBox textPlayerSevenName;
        private System.Windows.Forms.RadioButton radioPlayerSevenHuman;
        private System.Windows.Forms.RadioButton radioPlayerSevenEmpty;
        private System.Windows.Forms.Panel panelPlayerEight;
        private System.Windows.Forms.Label labelPlayerEight;
        private System.Windows.Forms.RadioButton radioPlayerEightAI;
        private System.Windows.Forms.TextBox textPlayerEightName;
        private System.Windows.Forms.RadioButton radioPlayerEightHuman;
        private System.Windows.Forms.RadioButton radioPlayerEightEmpty;
        private System.Windows.Forms.Label label2;
    }
}

