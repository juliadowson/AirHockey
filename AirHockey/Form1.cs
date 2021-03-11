/* Julia Dowson 
 * Mr. T
 * March 11, 2021
 * This is a basic Air Hockey game.
 * */


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
using System.Threading;

namespace AirHockey
{
    public partial class Form1 : Form

    {
        int paddle1X = 10;
        int paddle1Y = 150;
        int player1Score = 0;

        int paddle2X = 600;
        int paddle2Y = 150;
        int player2Score = 0;

        int paddleWidth = 10;
        int paddleHeight = 60;
        int paddleSpeed = 4;

        int ballX = 295;
        int ballY = 195;
        int ballXSpeed = 5;
        int ballYSpeed = 5;
        int ballWidth = 10;
        int ballHeight = 10;

        bool wDown = false;
        bool sDown = false;
        bool dDown = false;
        bool aDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        Pen drawPen = new Pen(Color.White, 6);
        Pen drawRed = new Pen(Color.Red, 5);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SoundPlayer pop = new SoundPlayer(Properties.Resources.pop);
        SoundPlayer goal = new SoundPlayer(Properties.Resources.goal);

        public Form1()
        {
            InitializeComponent();
        }

        //draws all shapes on screen
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(drawPen, this.Width / 2 + 1, 0, this.Width / 2 + 1, this.Height);
            e.Graphics.DrawEllipse(drawRed, -37, 110, 95, 150);
            e.Graphics.DrawEllipse(drawRed, 570, 110, 95, 150);
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            e.Graphics.DrawLine(drawPen, 1, 140, 1, 230);
            e.Graphics.DrawLine(drawPen, 621, 140, 621, 230);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;

            }
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            #region player move controls

            //moves player 1 

            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && paddle1X > 0)
            {
                paddle1X -= paddleSpeed;
            }

            if (dDown == true && paddle1X < this.Width / 2 - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }


            //moves player 2 
            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > this.Width / 2 + 14 - paddleWidth)
            {
                paddle2X -= paddleSpeed;
            }

            if (rightArrowDown == true && paddle2X < this.Width - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }
            #endregion

            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;
            }
            if (ballX < 0 || ballX > this.Width - ballWidth)
            {
                ballXSpeed *= -1;
            }
            //creates the intersection rectangles
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle player1Net = new Rectangle(1, 140, 1, 145);
            Rectangle player2Net = new Rectangle(621, 140, 621, 145);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            //what happens if the ball hits player 1
            if (player1Rec.IntersectsWith(ballRec))
            {

                pop.Play();
                ballXSpeed *= -1;
                if (ballX <= paddle1X)
                {
                    ballX = paddle1X - paddleWidth - 1;
                }
                else
                {
                    ballX = paddle1X + paddleWidth + 1;
                }
            }
            //what happens if the ball hits player 1
            else if (player2Rec.IntersectsWith(ballRec))
            {

                pop.Play();
                ballXSpeed *= -1;
                if (ballX <= paddle2X)
                {
                    ballX = paddle2X - paddleWidth - 1;
                }
                else
                {
                    ballX = paddle2X + paddleWidth + 1;
                }

            }

            //what happens if the ball hits player 1's net
            if (player1Net.IntersectsWith(ballRec))
            {
                goal.Play();
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                paddle1X = 10;
                paddle1Y = 150;

                paddle2X = 600;
                paddle2Y = 150;

                ballX = 295;
                ballY = 195;
                ballXSpeed = 4;
                ballYSpeed = 4;
                ballWidth = 10;
                ballHeight = 10;

            }
            //what happens if the ball hits player 2's net
            if (player2Net.IntersectsWith(ballRec))
            {
                goal.Play();
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                paddle1X = 10;
                paddle1Y = 150;

                paddle2X = 600;
                paddle2Y = 150;

                ballX = 295;
                ballY = 195;
                ballXSpeed = 4;
                ballYSpeed = 4;
                ballWidth = 10;
                ballHeight = 10;

            }
            //adds players score
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winnerLabel.Text = "Player 1 Wins!";
              
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winner2Label.Text = "Player 2 Wins!";
                
            }

            Refresh();

        }
    }
}
