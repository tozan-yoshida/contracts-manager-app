﻿namespace contracts_manager_app
{
    partial class Form2
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
            registOrUpdate = new Button();
            nameBox = new TextBox();
            addressBox = new TextBox();
            telBox = new TextBox();
            remarkBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // registOrUpdate
            // 
            registOrUpdate.Location = new Point(345, 457);
            registOrUpdate.Name = "registOrUpdate";
            registOrUpdate.Size = new Size(112, 34);
            registOrUpdate.TabIndex = 0;
            registOrUpdate.Text = "button1";
            registOrUpdate.UseVisualStyleBackColor = true;
            registOrUpdate.Click += button1_Click;
            // 
            // nameBox
            // 
            nameBox.Location = new Point(345, 88);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(391, 31);
            nameBox.TabIndex = 1;
            // 
            // addressBox
            // 
            addressBox.Location = new Point(345, 221);
            addressBox.Name = "addressBox";
            addressBox.Size = new Size(391, 31);
            addressBox.TabIndex = 2;
            // 
            // telBox
            // 
            telBox.Location = new Point(345, 151);
            telBox.Name = "telBox";
            telBox.Size = new Size(391, 31);
            telBox.TabIndex = 3;
            // 
            // remarkBox
            // 
            remarkBox.Location = new Point(345, 293);
            remarkBox.Multiline = true;
            remarkBox.Name = "remarkBox";
            remarkBox.Size = new Size(391, 97);
            remarkBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(183, 94);
            label1.Name = "label1";
            label1.Size = new Size(48, 25);
            label1.TabIndex = 5;
            label1.Text = "名前";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(183, 154);
            label2.Name = "label2";
            label2.Size = new Size(84, 25);
            label2.TabIndex = 6;
            label2.Text = "電話番号";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(183, 296);
            label3.Name = "label3";
            label3.Size = new Size(96, 25);
            label3.TabIndex = 7;
            label3.Text = "備考(30字)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(183, 227);
            label4.Name = "label4";
            label4.Size = new Size(106, 25);
            label4.TabIndex = 8;
            label4.Text = "メールアドレス";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 531);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(remarkBox);
            Controls.Add(telBox);
            Controls.Add(addressBox);
            Controls.Add(nameBox);
            Controls.Add(registOrUpdate);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button registOrUpdate;
        private TextBox nameBox;
        private TextBox addressBox;
        private TextBox telBox;
        private TextBox remarkBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}