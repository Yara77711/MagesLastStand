namespace MagesLastStand
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gamePanel = new System.Windows.Forms.Panel();
            this.goldLabel = new System.Windows.Forms.Label();
            this.waveLabel = new System.Windows.Forms.Label();
            this.startWaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // gamePanel
            this.gamePanel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.gamePanel.Location = new System.Drawing.Point(0, 50);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(800, 600);
            this.gamePanel.TabIndex = 0;
            this.gamePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseClick);

            // goldLabel
            this.goldLabel.AutoSize = true;
            this.goldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.goldLabel.Location = new System.Drawing.Point(12, 9);
            this.goldLabel.Name = "goldLabel";
            this.goldLabel.Size = new System.Drawing.Size(69, 20);
            this.goldLabel.TabIndex = 1;
            this.goldLabel.Text = "Gold: 100";

            // waveLabel
            this.waveLabel.AutoSize = true;
            this.waveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.waveLabel.Location = new System.Drawing.Point(200, 9);
            this.waveLabel.Name = "waveLabel";
            this.waveLabel.Size = new System.Drawing.Size(64, 20);
            this.waveLabel.TabIndex = 2;
            this.waveLabel.Text = "Wave: 0";

            // startWaveButton
            this.startWaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.startWaveButton.Location = new System.Drawing.Point(325, 670);
            this.startWaveButton.Name = "startWaveButton";
            this.startWaveButton.Size = new System.Drawing.Size(150, 50);
            this.startWaveButton.TabIndex = 3;
            this.startWaveButton.Text = "Start Wave";
            this.startWaveButton.UseVisualStyleBackColor = true;
            this.startWaveButton.Click += new System.EventHandler(this.startWaveButton_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 750);
            this.Controls.Add(this.startWaveButton);
            this.Controls.Add(this.waveLabel);
            this.Controls.Add(this.goldLabel);
            this.Controls.Add(this.gamePanel);
            this.Name = "Form1";
            this.Text = "Mage\'s Last Stand";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Label goldLabel;
        private System.Windows.Forms.Label waveLabel;
        private System.Windows.Forms.Button startWaveButton;
    }
}