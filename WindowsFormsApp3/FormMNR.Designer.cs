namespace WindowsFormsApp3
{
    partial class FormMNR
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
            this.TextBoxExpression = new System.Windows.Forms.TextBox();
            this.ButtonEval = new System.Windows.Forms.Button();
            this.TextBoxResult = new System.Windows.Forms.TextBox();
            this.ComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TextBoxExpression
            // 
            this.TextBoxExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxExpression.Location = new System.Drawing.Point(12, 15);
            this.TextBoxExpression.Name = "TextBoxExpression";
            this.TextBoxExpression.Size = new System.Drawing.Size(642, 22);
            this.TextBoxExpression.TabIndex = 0;
            // 
            // ButtonEval
            // 
            this.ButtonEval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonEval.Location = new System.Drawing.Point(660, 11);
            this.ButtonEval.Name = "ButtonEval";
            this.ButtonEval.Size = new System.Drawing.Size(132, 30);
            this.ButtonEval.TabIndex = 1;
            this.ButtonEval.Text = "Згенерувати";
            this.ButtonEval.UseVisualStyleBackColor = true;
            this.ButtonEval.Click += new System.EventHandler(this.ButtonEval_Click);
            // 
            // TextBoxResult
            // 
            this.TextBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxResult.Location = new System.Drawing.Point(12, 43);
            this.TextBoxResult.Multiline = true;
            this.TextBoxResult.Name = "TextBoxResult";
            this.TextBoxResult.ReadOnly = true;
            this.TextBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxResult.Size = new System.Drawing.Size(642, 395);
            this.TextBoxResult.TabIndex = 2;
            // 
            // ComboBox
            // 
            this.ComboBox.FormattingEnabled = true;
            this.ComboBox.Items.AddRange(new object[] {
            " ",
            "x",
            "x,y",
            "x,y,z"});
            this.ComboBox.Location = new System.Drawing.Point(660, 43);
            this.ComboBox.Name = "ComboBox";
            this.ComboBox.Size = new System.Drawing.Size(132, 24);
            this.ComboBox.TabIndex = 3;
            // 
            // FormMNR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 450);
            this.Controls.Add(this.ComboBox);
            this.Controls.Add(this.TextBoxResult);
            this.Controls.Add(this.ButtonEval);
            this.Controls.Add(this.TextBoxExpression);
            this.Name = "FormMNR";
            this.Text = "МНР";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxExpression;
        private System.Windows.Forms.Button ButtonEval;
        private System.Windows.Forms.TextBox TextBoxResult;
        private System.Windows.Forms.ComboBox ComboBox;
    }
}

