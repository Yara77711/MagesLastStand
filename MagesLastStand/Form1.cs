using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MagesLastStand
{
    public partial class Form1 : Form
    {
        private GameEngine gameEngine;
        private Bitmap buffer;
        private Graphics bufferGraphics;
        private System.Windows.Forms.Timer gameTimer; // ����� �������� ����

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // ��������� ����
            this.ClientSize = new Size(800, 750);
            this.MinimumSize = new Size(800, 750);
            this.MaximumSize = new Size(800, 750);

            // ��������� �������� ����
            gamePanel.Size = new Size(800, 600);
            gamePanel.Location = new Point(0, 50);

            // ������������� ������
            startWaveButton.Location = new Point(
                (this.ClientSize.Width - startWaveButton.Width) / 2,
                this.ClientSize.Height - startWaveButton.Height - 20);

            // ������������� �������
            buffer = new Bitmap(gamePanel.Width, gamePanel.Height);
            bufferGraphics = Graphics.FromImage(buffer);
            gameEngine = new GameEngine();

            // ��������� ������� Windows Forms
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 30;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            gameEngine.Update();
            DrawGame();
            UpdateUI();
        }

        private void DrawGame()
        {
            bufferGraphics.Clear(Color.DarkSlateGray);

            // ��������� ����
            for (int i = 0; i < gameEngine.Path.Count - 1; i++)
            {
                bufferGraphics.DrawLine(Pens.White, gameEngine.Path[i], gameEngine.Path[i + 1]);
            }

            // ��������� ������
            foreach (var enemy in gameEngine.Enemies)
            {
                bufferGraphics.FillEllipse(Brushes.Red,
                    enemy.Position.X - 15,
                    enemy.Position.Y - 15,
                    30, 30);
            }

            // ��������� �����
            foreach (var tower in gameEngine.Towers)
            {
                Brush towerBrush = tower.Type == "Arbalest" ? Brushes.Blue : Brushes.Orange;
                bufferGraphics.FillRectangle(towerBrush,
                    tower.Position.X - 20,
                    tower.Position.Y - 20,
                    40, 40);

                // ������ �����
                bufferGraphics.DrawEllipse(Pens.Yellow,
                    tower.Position.X - tower.Range,
                    tower.Position.Y - tower.Range,
                    tower.Range * 2,
                    tower.Range * 2);
            }

            gamePanel.CreateGraphics().DrawImage(buffer, 0, 0);
        }

        private void UpdateUI()
        {
            goldLabel.Text = $"Gold: {GameSettings.Gold}";
            waveLabel.Text = $"Wave: {GameSettings.Wave}";
            startWaveButton.Enabled = !gameEngine.WaveInProgress;
        }

        private void startWaveButton_Click(object sender, EventArgs e)
        {
            gameEngine.StartWave();
            UpdateUI();
        }

        private void gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                gameEngine.AddTower(Point.Round(e.Location), "Arbalest");
            }
            else if (e.Button == MouseButtons.Right)
            {
                gameEngine.AddTower(Point.Round(e.Location), "ChaosCrystal");
            }
            UpdateUI();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            gameTimer?.Stop();
            bufferGraphics?.Dispose();
            buffer?.Dispose();
        }
    }
}