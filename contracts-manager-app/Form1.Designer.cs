namespace contracts_manager_app
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
            searchBox = new TextBox();
            search = new Button();
            register = new Button();
            import = new Button();
            dataGridView1 = new DataGridView();
            export = new Button();
            searchError = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchBox.Location = new Point(704, 92);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(304, 31);
            searchBox.TabIndex = 0;
            // 
            // search
            // 
            search.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            search.Location = new Point(1014, 92);
            search.Name = "search";
            search.Size = new Size(57, 31);
            search.TabIndex = 1;
            search.Text = "検索";
            search.UseVisualStyleBackColor = true;
            search.Click += search_Click;
            // 
            // register
            // 
            register.Location = new Point(12, 77);
            register.Name = "register";
            register.Size = new Size(104, 60);
            register.TabIndex = 2;
            register.Text = "新規追加";
            register.UseVisualStyleBackColor = true;
            register.Click += register_Click;
            // 
            // import
            // 
            import.Location = new Point(122, 77);
            import.Name = "import";
            import.Size = new Size(104, 60);
            import.TabIndex = 3;
            import.Text = "インポート";
            import.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 154);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1059, 568);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // export
            // 
            export.Location = new Point(232, 77);
            export.Name = "export";
            export.Size = new Size(104, 60);
            export.TabIndex = 5;
            export.Text = "エクスポート";
            export.UseVisualStyleBackColor = true;
            // 
            // searchError
            // 
            searchError.AutoSize = true;
            searchError.ForeColor = Color.Crimson;
            searchError.Location = new Point(704, 126);
            searchError.Name = "searchError";
            searchError.Size = new Size(22, 25);
            searchError.TabIndex = 6;
            searchError.Text = "  ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 734);
            Controls.Add(searchError);
            Controls.Add(export);
            Controls.Add(dataGridView1);
            Controls.Add(import);
            Controls.Add(register);
            Controls.Add(search);
            Controls.Add(searchBox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox searchBox;
        private Button search;
        private Button register;
        private Button import;
        private DataGridView dataGridView1;
        private Button export;
        private Label searchError;
    }
}
