using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

// MATTHEW SMITH
// MARCH 22 2021
// THEODORE

namespace SpaceRace
{
    public partial class Form1 : Form
    {
        // VARIABLES
        string scene = "title";

        SoundPlayer music = new SoundPlayer(Properties.Resources.music);
        SoundPlayer death = new SoundPlayer(Properties.Resources.death);
        SoundPlayer gameover = new SoundPlayer(Properties.Resources.gameover);

        int p1X = 200;
        int p1Y = 350;
        int p2X = 570;
        int p2Y = 350;

        Random Randgen = new Random();

        int heroWidth = 30;
        int heroHeight = 50;
        int heroSpeed = 5;

        int rockSpeed = 3;

        int safezone = 350;

        List<int> rockXList = new List<int>();
        List<int> rockXRList = new List<int>();
        List<int> rockYList = new List<int>();
        List<int> rockSpeedList = new List<int>();
        int rockSize = 5;

        int p1score = 0;
        int p2score = 0;
        int time = 1000;

        bool upDown = false;
        bool downDown = false;
        bool wDown = false;
        bool sDown = false;
        bool spaceDown = false;
        bool escapeDown = false;

        string outcome = "";

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        public Form1()
        {
            InitializeComponent();
            GameTitle();
        }

        // SCENE OPTIONS
        public void GameInitialize()
        {
            music.Stop();
            scene = "run";
            gameTimer.Enabled = true;
            p1score = 0;
            p2score = 0;
            time = 1000;
            titleLabel.Text = "";
            subtitleLabel.Text = $"";

        }
        public void GameTitle()
        {
            music.Play();
            scene = "title";
            gameTimer.Enabled = false;
            p1score = 0;
            p2score = 0;
            time = 1000;

            titleLabel.Text = "SPACE RACE";
            subtitleLabel.Text = $"Space - Start || Esc - Quit";
        }
        public void GameEnd()
        {
            gameover.Play();
            scene = "end";
            gameTimer.Enabled = false;
            time = 1000;

            titleLabel.Text = "GAME OVER";
            subtitleLabel.Text = $"{outcome}\n SPACE - PLAY AGAIN || ESC - QUIT";

        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //RANDOM VALUES
            int random = randGen.Next(0, safezone);
            if (random < 20)
            {
                    rockYList.Add(randGen.Next(10,safezone));
                    rockXList.Add(10);

            }

            if (random < 20)
            {
                rockYList.Add(randGen.Next(10, safezone));
                rockXRList.Add(800);

            }

            //ROCK MOVEMENT
            for (int i = 0; i < rockXList.Count(); i++)
            { rockXList[i] += rockSpeed; }

           for (int i = 0; i < rockXRList.Count(); i++)
            { rockXRList[i] -= rockSpeed; }

            //MAKE + CHECK COLLISIONS
            Rectangle P1Rec = new Rectangle(p1X, p1Y, heroWidth, heroHeight);
            Rectangle P2Rec = new Rectangle(p2X, p2Y, heroWidth, heroHeight);
            
            for (int i = 0; i < rockXList.Count(); i++)
            { 
                Rectangle Rock1Rec = new Rectangle(rockXList[i], rockYList[i], rockSize, rockSize);
                Rectangle Rock2Rec = new Rectangle(rockXRList[i], rockYList[i], rockSize, rockSize);
                if (P1Rec.IntersectsWith(Rock1Rec) || P1Rec.IntersectsWith(Rock2Rec))
                { p1Y = 350;
                    death.Play();
                }
               
            }
            for (int i = 0; i < rockXList.Count(); i++)
            {
                Rectangle Rock2Rec = new Rectangle(rockXList[i], rockYList[i], rockSize, rockSize);
                Rectangle Rock1Rec = new Rectangle(rockXRList[i], rockYList[i], rockSize, rockSize);
                if (P2Rec.IntersectsWith(Rock1Rec) || P2Rec.IntersectsWith(Rock2Rec))
                { p2Y = 350;
                    death.Play();
                }
            }
            for (int i = 0; i < rockXList.Count(); i++)
            { Rectangle Rock2Rec = new Rectangle(rockXRList[i], rockYList[i], rockSize, rockSize); }

            // SCORE SETTING
            if (p1Y == 0)
            {
                p1score++;
                p1Y = 350;
            }

            if (p2Y == 0)
            {
                p2score++;
                p2Y = 350;
            }

            //TIMER RUN OUT
            time--;
            if (time == 0)
            {
                if (p1score == p2score)
                { outcome = "TIE!"; }
                else if (p1score > p2score)
                { outcome = "Player 1 WINS!"; }
                else
                { outcome = "Player 2 WINS!"; }
                GameEnd();
            }

            timeLabel.Text = $"{time}";
            

            //PLAYER 2 MOVEMENT
            if (upDown == true && p2Y > 0)
            { p2Y -= heroSpeed; }
            if (downDown == true && p2Y < 490 - heroHeight)
            { p2Y += heroSpeed; }

            //PLAYER 1 MOVEMENT
            if (wDown == true && p1Y > 0)
            { p1Y -= heroSpeed; }
            else if (sDown == true && p1Y < 490 - heroHeight)
            { p1Y += heroSpeed; }
            Refresh();
        }
      
        // KEY DOWN CHECK
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    spaceDown = true;
                    if (scene == "title" || scene == "end") { GameInitialize(); }
                    break;
                case Keys.Escape:
                    if (scene == "title" || scene == "end") { Application.Exit(); }
                    escapeDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
            }

        }
        // PAINTING
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //update labels 

            timeLabel.Text = $"{time}";

            p1ScoreLabel.Text = $"Score: {p1score}";
            p2ScoreLabel.Text = $"Score: {p2score}";
            if (scene == "run")
            {
                e.Graphics.FillRectangle(whiteBrush, 400, 40, 5, 490);

                e.Graphics.FillRectangle(whiteBrush, p1X, p1Y, heroWidth, heroHeight);
                e.Graphics.FillRectangle(whiteBrush, p2X, p2Y, heroWidth, heroHeight);
              
                for (int i = 0; i < rockXList.Count(); i++) 
                { 
                    e.Graphics.FillRectangle(whiteBrush, rockXList[i], rockYList[i], rockSize, rockSize); 
              
                }
            
                for (int i = 0; i < rockXRList.Count(); i++) 
                { 
                    e.Graphics.FillRectangle(whiteBrush, rockXRList[i], rockYList[i], rockSize, rockSize);
                }
                e.Graphics.FillRectangle(whiteBrush, p2X, p2Y, heroWidth, heroHeight);
            }

        }

        // KEY UP
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
            }
        }
    }
}
