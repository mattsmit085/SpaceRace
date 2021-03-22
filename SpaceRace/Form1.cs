using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRace
{
    public partial class Form1 : Form
    {
        string scene = "title";

        int heroX = 280;
        int heroY = 540;
        int heroWidth = 40;
        int heroHeight = 10;
        int heroSpeed = 10;

        List<int> rockXList = new List<int>();
        List<int> rockYList = new List<int>();
        List<int> rockSpeedList = new List<int>();
        int ballSize = 5;

        int score = 0;
        int time = 1000;

        bool leftDown = false;
        bool rightDown = false;
        bool spaceDown = false;
        bool escapeDown = false;

        string winner = "";

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;
        public Form1()
        {
            InitializeComponent();
        }

        public void GameInitialize()
        {
            scene = "run";
            gameTimer.Enabled = true;
            heroX = 280;
            heroY = 540;
            score = 0;
            time = 1000;
            titleLabel.Text = "";
            subtitleLabel.Text = $"";

        }
        public void GameTitle()
        {
            scene = "title";
            gameTimer.Enabled = false;
            heroX = 0;
            heroY = 0;
            score = 0;
            time = 1000;

            titleLabel.Text = "SPACE RACE";
            subtitleLabel.Text = $"Space - Start || Esc - Quit";
        }
        public void GameEnd()
        {
            scene = "end";
            gameTimer.Enabled = false;
            heroX = 0;
            heroY = 0;
            score = 0;
            time = 1000;

            titleLabel.Text = "GAME OVER";
            subtitleLabel.Text = $"{winner} Wins!";

        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            time--;
            timeLabel.Text = $"{time}";

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    spaceDown = true;
                    if (scene == "title" || scene == "end") { GameInitialize(); }
                    break;
                case Keys.Escape:
                    escapeDown = true;
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //update labels 

            timeLabel.Text = $"{time}";

            p1ScoreLabel.Text = $"Score: {score}";
            p2ScoreLabel.Text = $"Score: {score}";
            if (scene == "run")
            {
                e.Graphics.FillRectangle(whiteBrush, 400, 40, 5, 490);
            }


        }
    }
}
