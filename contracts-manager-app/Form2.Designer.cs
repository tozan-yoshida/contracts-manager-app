namespace contracts_manager_app
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
            nameError = new Label();
            telError = new Label();
            remarkError = new Label();
            addressError = new Label();
            formInfo = new Label();
            SuspendLayout();
            // 
            // registOrUpdate
            // 
            registOrUpdate.Location = new Point(345, 457);
            registOrUpdate.Name = "registOrUpdate";
            registOrUpdate.Size = new Size(112, 34);
            registOrUpdate.TabIndex = 0;
            registOrUpdate.UseVisualStyleBackColor = true;
            registOrUpdate.Click += registOrUpdate_Click;
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
            addressBox.Location = new Point(345, 212);
            addressBox.Name = "addressBox";
            addressBox.Size = new Size(391, 31);
            addressBox.TabIndex = 2;
            // 
            // telBox
            // 
            telBox.ImeMode = ImeMode.Disable;
            telBox.Location = new Point(345, 150);
            telBox.Name = "telBox";
            telBox.ShortcutsEnabled = false;
            telBox.Size = new Size(391, 31);
            telBox.TabIndex = 3;
            telBox.KeyPress += telBox_KeyPress;
            // 
            // remarkBox
            // 
            remarkBox.Location = new Point(345, 274);
            remarkBox.Multiline = true;
            remarkBox.Name = "remarkBox";
            remarkBox.Size = new Size(391, 97);
            remarkBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(105, 91);
            label1.Name = "label1";
            label1.Size = new Size(48, 25);
            label1.TabIndex = 5;
            label1.Text = "名前";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(105, 153);
            label2.Name = "label2";
            label2.Size = new Size(168, 25);
            label2.TabIndex = 6;
            label2.Text = "電話番号(15字以内)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(105, 274);
            label3.Name = "label3";
            label3.Size = new Size(132, 25);
            label3.TabIndex = 7;
            label3.Text = "備考(30字以内)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(105, 215);
            label4.Name = "label4";
            label4.Size = new Size(190, 25);
            label4.TabIndex = 8;
            label4.Text = "メールアドレス(30字以内)";
            // 
            // nameError
            // 
            nameError.AutoSize = true;
            nameError.ForeColor = Color.Crimson;
            nameError.Location = new Point(345, 122);
            nameError.Name = "nameError";
            nameError.Size = new Size(22, 25);
            nameError.TabIndex = 9;
            nameError.Text = "  ";
            // 
            // telError
            // 
            telError.AutoSize = true;
            telError.ForeColor = Color.Crimson;
            telError.Location = new Point(345, 184);
            telError.Name = "telError";
            telError.Size = new Size(22, 25);
            telError.TabIndex = 13;
            telError.Text = "  ";
            // 
            // remarkError
            // 
            remarkError.AutoSize = true;
            remarkError.ForeColor = Color.Crimson;
            remarkError.Location = new Point(345, 374);
            remarkError.Name = "remarkError";
            remarkError.Size = new Size(22, 25);
            remarkError.TabIndex = 14;
            remarkError.Text = "  ";
            // 
            // addressError
            // 
            addressError.AutoSize = true;
            addressError.ForeColor = Color.Crimson;
            addressError.Location = new Point(345, 246);
            addressError.Name = "addressError";
            addressError.Size = new Size(22, 25);
            addressError.TabIndex = 15;
            addressError.Text = "  ";
            // 
            // formInfo
            // 
            formInfo.AutoSize = true;
            formInfo.Font = new Font("Yu Gothic UI", 13F);
            formInfo.Location = new Point(12, 9);
            formInfo.Name = "formInfo";
            formInfo.Size = new Size(83, 36);
            formInfo.TabIndex = 16;
            formInfo.Text = "label5";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 531);
            Controls.Add(formInfo);
            Controls.Add(addressError);
            Controls.Add(remarkError);
            Controls.Add(telError);
            Controls.Add(nameError);
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
            Text = "連絡先画面アプリ";
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
        private Label nameError;
        private Label telError;
        private Label remarkError;
        private Label addressError;
        private Label formInfo;
    }
}