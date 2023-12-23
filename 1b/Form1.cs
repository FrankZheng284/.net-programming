using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MathProblem
{
    public partial class Form1 : Form
    {
        private int a, b, c, answer, score;

        public Form1()
        {
            InitializeComponent();
            start();
        }
        
        private void start()
        {
            score = 0;
            scoreLabel.Text = "总分: " + score.ToString();
            Form1_Load();
            timer1.Start();
        }
        
        private void Form1_Load()
        {
            Random rnd = new Random();
            a = rnd.Next(1, 40);
            b = rnd.Next(1, 40);
            c = rnd.Next(1, 3);
            answerBox.Text = "";
            if (c == 1)
            {
                question.Text = a.ToString() + "+" + b.ToString() + "=";
                answer = a + b;
            }
            else
            {
                question.Text = a.ToString() + "-" + b.ToString() + "=";
                answer = a - b;
            }


        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (answerBox.Text == answer.ToString())
            {
                score = score + 10;
                label1.Text = "上题状况：正确";
            }
            else
            {
                score = score - 5;
                label1.Text = "上题状况：错误";
            }
            scoreLabel.Text = "总分: " + score.ToString();
            Form1_Load();
            timer1.Stop();
            timer1.Start();
                
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            button1_Click(null, null);
        }
    }
}