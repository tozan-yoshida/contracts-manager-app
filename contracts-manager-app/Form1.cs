using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contacts { get; set; }
        // データベースとの接続文字列作成
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

        private Form2 f2;

        public string id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string remark { get; set; }

        public Form1()
        {
            InitializeComponent();

            // DataTableの初期化
            contacts = new DataTable();
            contacts.Columns.Add("id", typeof(int));
            contacts.Columns.Add("name", typeof(string));
            contacts.Columns.Add("tel", typeof(string));
            contacts.Columns.Add("address", typeof(string));
            contacts.Columns.Add("remark", typeof(string));

            // 初期状態は新規登録

            f2 = new Form2(this);

            id = "";
            name = "";
            tel = "";
            address = "";
            remark = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DataGridViewの初期化
            // カラム数を指定
            dataGridView1.DataSource = contacts;

            // DataGridViewButtonColumnの作成
            DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();

            // 列の名前を設定
            updateButton.Name = "編集";
            deleteButton.Name = "削除";

            // すべてのボタンに"編集"、"削除"と表示する
            updateButton.UseColumnTextForButtonValue = true;
            deleteButton.UseColumnTextForButtonValue = true;
            updateButton.Text = "編集";
            deleteButton.Text = "削除";

            // DataGridViewに追加する
            dataGridView1.Columns.Add(updateButton);
            dataGridView1.Columns.Add(deleteButton);

            // カラム名を指定
            dataGridView1.Columns[1].HeaderText = "名前";
            dataGridView1.Columns[2].HeaderText = "電話番号";
            dataGridView1.Columns[3].HeaderText = "メールアドレス";
            dataGridView1.Columns[4].HeaderText = "備考";

            // はじめの列を非表示にする
            dataGridView1.Columns[0].Visible = false;


            // データの追加テスト
            contacts.Rows.Add(1, "田中", "123456789", "nanikasira", "ビコー");

            ScreenDisplay();

        }

        /// <summary>
        /// DataGridViewの表を(再)取得する
        /// </summary>
        public void ScreenDisplay()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // 表示の初期化
                    contacts.Clear();

                    // テーブルの全要素取得コマンドの生成
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM contacts";

                    // db接続
                    connection.Open();
                    Debug.WriteLine("接続成功");

                    using (var sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                contacts.Rows.Add(sdr["id"], sdr["name"].ToString(), sdr["tel"].ToString(), sdr["address"].ToString(), sdr["remark"].ToString());
                            }
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("ｴﾗｰ" + ex.Message);
            }

        }

        /// <summary>
        /// 新規登録ボタンを押した時の処理
        /// </summary>
        private void register_Click(object sender, EventArgs e)
        {
            f2.update = false;
            f2.LabelChanger("登録");
            f2.ShowDialog();
        }


        /// <summary>
        /// 編集、削除ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            // "編集"ボタンを押したときの処理
            if (dgv.Columns[e.ColumnIndex].Name == "編集")
            {
                // 編集、更新画面のボタンの表記を"更新"に変更
                f2.LabelChanger("更新");
                f2.update = true;
                // 押された"編集"ボタンの行の情報取得、格納
                id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                tel = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                address = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[4].Value != null) remark = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                // 正しく取得できているかのテスト
                // MessageBox.Show( id + name + tel + address + remark);

                // 遷移先の画面のテキストボックスに自動的に入力
                f2.TextBoxRegester(name, tel, address, remark);

                // 画面遷移
                f2.ShowDialog();
            }

            // "削除"ボタンを押したときの処理
            else if(dgv.Columns[e.ColumnIndex].Name == "削除")
            {
                DialogResult result = MessageBox.Show("連絡先を削除しますか？", "質問", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                // OKを押したときの処理
                if (result == DialogResult.OK)
                {
                    try
                    {
                        using(SqlConnection conn = new SqlConnection(connectionString))
                        {
                            // 削除したい行のidを取得
                            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                            // クエリ文作成
                            string cmdtest = "DELETE FROM contacts WHERE id = " + id;

                            using (var cmd =  new SqlCommand(cmdtest, conn)) {
                                // db接続
                                conn.Open();
                                // クエリ文実行
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        // 画面の再表示
                        ScreenDisplay();
                    }
                }
            }
        }
    }
}
