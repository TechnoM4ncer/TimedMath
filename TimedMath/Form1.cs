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

namespace TimedMath
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        //these integers store the numbers for the addition problem
        int addend1;
        int addend2;

        //these integers store the numbers for the subtraction problem
        int minuend;
        int subtrahend;

        //these integers store the numbers for the multiplcation problem
        int multiplicand;
        int multiplier;

        //these interges store the numbers for the division problem
        int dividend;
        int divisor;

        //this integer keeps track of time remaining
        int timeLeft;

        //Create sound player for correct answer sound
        private void playSound()
        {
            SoundPlayer correctSound = new SoundPlayer(@"C:/Windows/Media/tada.wav");
            correctSound.Play();
        }
        public void StartTheQuiz()
        {
            //fill in the addition problem
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            //convert random addends to strings
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            //resets sum NumericUpDown to 0
            sum.Value = 0;

            //fill in subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            //fill in the multipication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            //fill in division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            //Start the timer
            timeLeft = 30;
            timeLabel.Text = "30 Seconds";
            timer1.Start();
        }
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) 
                && (minuend - subtrahend == difference.Value) 
                && (multiplicand * multiplier == product.Value) 
                && (dividend / divisor == quotient.Value))
            {
                return true;
            } else
            {
                return false;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
            timeLabel.BackColor = Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!");
                startButton.Enabled = true;

            }
            else if (timeLeft > 0)
                 {
                if (timeLeft <= 6)
                {
                    timeLabel.BackColor = Color.Red;
                }
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's Up!";
                MessageBox.Show("You didn't finish in time, sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
        private void sum_ValueChanged(object sender, EventArgs e)
        {
            if (sum.Value == addend1 + addend2)
            {
                playSound();
            }
        }

        private void difference_ValueChanged(object sender, EventArgs e)
        {
            if (difference.Value == minuend - subtrahend)
            {
                playSound();
            }
        }

        private void product_ValueChanged(object sender, EventArgs e)
        {
            if (product.Value == multiplicand * multiplier)
            {
                playSound();
            }
        }

        private void quotient_ValueChanged(object sender, EventArgs e)
        {
            if (quotient.Value == dividend / divisor)
            {
                playSound();
            }
        }
    }
}
