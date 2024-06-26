﻿using Microsoft.Win32;

namespace contracts_manager_app
{
    partial class RegistOrUpdate
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
            enter = new Button();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            fileChoice = new Button();
            deleteImage = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // nameBox
            // 
            nameBox.Location = new Point(679, 65);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(342, 31);
            nameBox.TabIndex = 1;
            // 
            // addressBox
            // 
            addressBox.Location = new Point(679, 189);
            addressBox.MaxLength = 30;
            addressBox.Name = "addressBox";
            addressBox.Size = new Size(342, 31);
            addressBox.TabIndex = 2;
            // 
            // telBox
            // 
            telBox.ImeMode = ImeMode.Disable;
            telBox.Location = new Point(679, 127);
            telBox.MaxLength = 15;
            telBox.Name = "telBox";
            telBox.ShortcutsEnabled = false;
            telBox.Size = new Size(342, 31);
            telBox.TabIndex = 3;
            telBox.KeyPress += telBox_KeyPress;
            // 
            // remarkBox
            // 
            remarkBox.Location = new Point(679, 251);
            remarkBox.MaxLength = 30;
            remarkBox.Multiline = true;
            remarkBox.Name = "remarkBox";
            remarkBox.Size = new Size(342, 97);
            remarkBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(380, 65);
            label1.Name = "label1";
            label1.Size = new Size(48, 25);
            label1.TabIndex = 5;
            label1.Text = "名前";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(380, 127);
            label2.Name = "label2";
            label2.Size = new Size(204, 25);
            label2.TabIndex = 6;
            label2.Text = "電話番号(数字15字以内)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(380, 248);
            label3.Name = "label3";
            label3.Size = new Size(132, 25);
            label3.TabIndex = 7;
            label3.Text = "備考(30字以内)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(380, 189);
            label4.Name = "label4";
            label4.Size = new Size(266, 25);
            label4.TabIndex = 8;
            label4.Text = "メールアドレス(30字以内、@を含む)";
            // 
            // nameError
            // 
            nameError.AutoSize = true;
            nameError.ForeColor = Color.Crimson;
            nameError.Location = new Point(679, 99);
            nameError.Name = "nameError";
            nameError.Size = new Size(22, 25);
            nameError.TabIndex = 9;
            nameError.Text = "  ";
            // 
            // telError
            // 
            telError.AutoSize = true;
            telError.ForeColor = Color.Crimson;
            telError.Location = new Point(679, 161);
            telError.Name = "telError";
            telError.Size = new Size(22, 25);
            telError.TabIndex = 13;
            telError.Text = "  ";
            // 
            // remarkError
            // 
            remarkError.AutoSize = true;
            remarkError.ForeColor = Color.Crimson;
            remarkError.Location = new Point(679, 351);
            remarkError.Name = "remarkError";
            remarkError.Size = new Size(22, 25);
            remarkError.TabIndex = 14;
            remarkError.Text = "  ";
            // 
            // addressError
            // 
            addressError.AutoSize = true;
            addressError.ForeColor = Color.Crimson;
            addressError.Location = new Point(679, 223);
            addressError.Name = "addressError";
            addressError.Size = new Size(22, 25);
            addressError.TabIndex = 15;
            addressError.Text = "  ";
            // 
            // formInfo
            // 
            formInfo.BackColor = SystemColors.ControlDark;
            formInfo.Font = new Font("Yu Gothic UI", 13F);
            formInfo.ForeColor = Color.White;
            formInfo.Location = new Point(-3, -1);
            formInfo.Name = "formInfo";
            formInfo.Size = new Size(3000, 36);
            formInfo.TabIndex = 16;
            formInfo.Text = "label5";
            // 
            // enter
            // 
            enter.BackColor = SystemColors.GradientActiveCaption;
            enter.Location = new Point(437, 381);
            enter.Name = "enter";
            enter.Size = new Size(173, 57);
            enter.TabIndex = 17;
            enter.Text = "button1";
            enter.UseVisualStyleBackColor = false;
            enter.Click += registOrUpdate_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Crimson;
            label5.Location = new Point(425, 65);
            label5.Name = "label5";
            label5.Size = new Size(30, 25);
            label5.TabIndex = 18;
            label5.Text = "※";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Crimson;
            label6.Location = new Point(580, 127);
            label6.Name = "label6";
            label6.Size = new Size(30, 25);
            label6.TabIndex = 19;
            label6.Text = "※";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.Crimson;
            label7.Location = new Point(643, 189);
            label7.Name = "label7";
            label7.Size = new Size(30, 25);
            label7.TabIndex = 20;
            label7.Text = "※";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.kkrn_icon_user_1;
            pictureBox1.Location = new Point(27, 65);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(269, 183);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // fileChoice
            // 
            fileChoice.Location = new Point(96, 267);
            fileChoice.Name = "fileChoice";
            fileChoice.Size = new Size(128, 45);
            fileChoice.TabIndex = 22;
            fileChoice.Text = "ファイル選択";
            fileChoice.UseVisualStyleBackColor = true;
            fileChoice.Click += fileChoice_Click;
            // 
            // deleteImage
            // 
            deleteImage.AutoSize = true;
            deleteImage.Location = new Point(105, 318);
            deleteImage.Name = "deleteImage";
            deleteImage.Size = new Size(110, 29);
            deleteImage.TabIndex = 23;
            deleteImage.Text = "画像削除";
            deleteImage.UseVisualStyleBackColor = true;
            // 
            // RegistOrUpdate
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 462);
            Controls.Add(deleteImage);
            Controls.Add(fileChoice);
            Controls.Add(pictureBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(enter);
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
            Name = "RegistOrUpdate";
            StartPosition = FormStartPosition.CenterParent;
            Text = "連絡先管理アプリ";
            Load += RegistOrUpdate_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
        private Button enter;
        private Label label5;
        private Label label6;
        private Label label7;
        private PictureBox pictureBox1;
        private Button fileChoice;
        private CheckBox deleteImage;
    }
}