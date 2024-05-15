namespace contracts_manager_app
{
    partial class InquiryScreen
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            searchBox = new TextBox();
            search = new Button();
            register = new Button();
            import = new Button();
            dataGridView1 = new DataGridView();
            contactBindingSource = new BindingSource(components);
            export = new Button();
            searchError = new Label();
            showAllContacts = new Button();
            window1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)contactBindingSource).BeginInit();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchBox.Location = new Point(1090, 80);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(304, 31);
            searchBox.TabIndex = 0;
            // 
            // search
            // 
            search.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            search.Location = new Point(1400, 80);
            search.Name = "search";
            search.Size = new Size(57, 31);
            search.TabIndex = 1;
            search.Text = "検索";
            search.UseVisualStyleBackColor = true;
            search.Click += search_Click;
            // 
            // register
            // 
            register.BackColor = SystemColors.GradientActiveCaption;
            register.Location = new Point(12, 65);
            register.Name = "register";
            register.Size = new Size(104, 60);
            register.TabIndex = 2;
            register.Text = "新規追加";
            register.UseVisualStyleBackColor = false;
            register.Click += register_Click;
            // 
            // import
            // 
            import.Location = new Point(122, 65);
            import.Name = "import";
            import.Size = new Size(104, 60);
            import.TabIndex = 3;
            import.Text = "インポート";
            import.UseVisualStyleBackColor = true;
            import.Click += import_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Yu Gothic UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Location = new Point(12, 154);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 43;
            dataGridView1.Size = new Size(1445, 250);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // contactBindingSource
            // 
            contactBindingSource.DataSource = typeof(Contact);
            // 
            // export
            // 
            export.Location = new Point(232, 65);
            export.Name = "export";
            export.Size = new Size(104, 60);
            export.TabIndex = 5;
            export.Text = "エクスポート";
            export.UseVisualStyleBackColor = true;
            export.Click += export_Click;
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
            // showAllContacts
            // 
            showAllContacts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            showAllContacts.Location = new Point(1311, 117);
            showAllContacts.Name = "showAllContacts";
            showAllContacts.Size = new Size(146, 31);
            showAllContacts.TabIndex = 7;
            showAllContacts.Text = "再読込";
            showAllContacts.UseVisualStyleBackColor = true;
            showAllContacts.Click += showAllContacts_Click;
            // 
            // window1
            // 
            window1.BackColor = SystemColors.ControlDark;
            window1.Font = new Font("Yu Gothic UI", 13F);
            window1.ForeColor = Color.White;
            window1.Location = new Point(0, 0);
            window1.Name = "window1";
            window1.Size = new Size(3000, 38);
            window1.TabIndex = 8;
            window1.Text = "照会画面";
            // 
            // InquiryScreen
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1469, 469);
            Controls.Add(window1);
            Controls.Add(showAllContacts);
            Controls.Add(searchError);
            Controls.Add(export);
            Controls.Add(dataGridView1);
            Controls.Add(import);
            Controls.Add(register);
            Controls.Add(search);
            Controls.Add(searchBox);
            Name = "InquiryScreen";
            Text = "連絡先管理アプリ";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)contactBindingSource).EndInit();
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
        private Button showAllContacts;
        private Label window1;
        private BindingSource contactBindingSource;
    }
}
