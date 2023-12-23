namespace MathProblem
{
    partial class Form1
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
            this.timer1 = new System.Timers.Timer();
            this.label1 = new System.Windows.Forms.Label();
            this.question = new System.Windows.Forms.Label();
            this.answerBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.scoreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(41, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "上题状况：";
            // 
            // question
            // 
            this.question.Font = new System.Drawing.Font("宋体", 90F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.question.Location = new System.Drawing.Point(89, 132);
            this.question.Name = "question";
            this.question.Size = new System.Drawing.Size(417, 128);
            this.question.TabIndex = 3;
            this.question.Text = "12+20=";
            // 
            // answerBox
            // 
            this.answerBox.Font = new System.Drawing.Font("宋体", 90F);
            this.answerBox.Location = new System.Drawing.Point(494, 129);
            this.answerBox.Name = "answerBox";
            this.answerBox.Size = new System.Drawing.Size(205, 144);
            this.answerBox.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(561, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // scoreLabel
            // 
            this.scoreLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.scoreLabel.Font = new System.Drawing.Font("宋体", 30F);
            this.scoreLabel.Location = new System.Drawing.Point(560, 370);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(228, 56);
            this.scoreLabel.TabIndex = 6;
            this.scoreLabel.Text = "总分：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.answerBox);
            this.Controls.Add(this.question);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox answerBox;

        private System.Windows.Forms.Label scoreLabel;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label question;

        private System.Timers.Timer timer1;

        #endregion
    }
}