namespace Hard_Lab_5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            ButtonRun = new Button();
            textBox = new TextBox();
            checkBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.DarkSlateGray;
            pictureBox.Location = new Point(274, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(720, 697);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // ButtonRun
            // 
            ButtonRun.BackColor = Color.DimGray;
            ButtonRun.BackgroundImageLayout = ImageLayout.None;
            ButtonRun.FlatStyle = FlatStyle.Popup;
            ButtonRun.Font = new Font("Arial Black", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonRun.ForeColor = Color.White;
            ButtonRun.Location = new Point(12, 210);
            ButtonRun.Name = "ButtonRun";
            ButtonRun.Size = new Size(94, 29);
            ButtonRun.TabIndex = 3;
            ButtonRun.Text = "Run";
            ButtonRun.UseVisualStyleBackColor = false;
            ButtonRun.Click += ButtonRunClick;
            // 
            // textBox
            // 
            textBox.BackColor = Color.DimGray;
            textBox.BorderStyle = BorderStyle.None;
            textBox.Font = new Font("Arial Black", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBox.ForeColor = Color.White;
            textBox.Location = new Point(12, 12);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.RightToLeft = RightToLeft.No;
            textBox.Size = new Size(256, 192);
            textBox.TabIndex = 4;
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.CheckAlign = ContentAlignment.MiddleRight;
            checkBox.Location = new Point(112, 213);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(78, 24);
            checkBox.TabIndex = 5;
            checkBox.Text = "Is tree?";
            checkBox.UseVisualStyleBackColor = true;
            checkBox.CheckedChanged += CheckBoxCheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateGray;
            ClientSize = new Size(1006, 721);
            Controls.Add(checkBox);
            Controls.Add(textBox);
            Controls.Add(ButtonRun);
            Controls.Add(pictureBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Button ButtonRun;
        private TextBox textBox;
        private CheckBox checkBox;
    }
}