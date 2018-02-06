namespace SpinSpin.Interface
{
    partial class Action
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
            this.stepNameLabel = new System.Windows.Forms.Label();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.nextStepButton = new System.Windows.Forms.Button();
            this.previousStepButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stepNameLabel
            // 
            this.stepNameLabel.AutoSize = true;
            this.stepNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.stepNameLabel.Location = new System.Drawing.Point(12, 9);
            this.stepNameLabel.Name = "stepNameLabel";
            this.stepNameLabel.Size = new System.Drawing.Size(64, 25);
            this.stepNameLabel.TabIndex = 0;
            this.stepNameLabel.Text = "Krok: ";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultTextBox.Location = new System.Drawing.Point(13, 57);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultTextBox.Size = new System.Drawing.Size(729, 461);
            this.resultTextBox.TabIndex = 1;
            // 
            // nextStepButton
            // 
            this.nextStepButton.Location = new System.Drawing.Point(611, 524);
            this.nextStepButton.Name = "nextStepButton";
            this.nextStepButton.Size = new System.Drawing.Size(131, 38);
            this.nextStepButton.TabIndex = 2;
            this.nextStepButton.Text = "Następny krok";
            this.nextStepButton.UseVisualStyleBackColor = true;
            this.nextStepButton.Click += new System.EventHandler(this.nextStepButton_Click);
            // 
            // previousStepButton
            // 
            this.previousStepButton.Enabled = false;
            this.previousStepButton.Location = new System.Drawing.Point(13, 524);
            this.previousStepButton.Name = "previousStepButton";
            this.previousStepButton.Size = new System.Drawing.Size(113, 38);
            this.previousStepButton.TabIndex = 3;
            this.previousStepButton.Text = "Poprzedni krok";
            this.previousStepButton.UseVisualStyleBackColor = true;
            this.previousStepButton.Click += new System.EventHandler(this.previousStepButton_Click);
            // 
            // printButton
            // 
            this.printButton.Enabled = false;
            this.printButton.Location = new System.Drawing.Point(296, 524);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(160, 38);
            this.printButton.TabIndex = 4;
            this.printButton.Text = "Drukuj do pliku";
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // Action
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 565);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.previousStepButton);
            this.Controls.Add(this.nextStepButton);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.stepNameLabel);
            this.Name = "Action";
            this.Text = "Action";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label stepNameLabel;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Button nextStepButton;
        private System.Windows.Forms.Button previousStepButton;
        private System.Windows.Forms.Button printButton;
    }
}