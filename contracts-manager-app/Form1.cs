using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contacts;
        // データベースとの接続文字列作成
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DataGridViewの初期化
            // カラム数を指定
            dataGridView1.DataSource = contacts;

            // DataGridViewButtonColumnの作成
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();

            // 列の名前を設定
            update.Name = "編集";
            delete.Name = "削除";

            // すべてのボタンに"編集"、"削除"と表示する
            update.UseColumnTextForButtonValue = true;
            delete.UseColumnTextForButtonValue = true;
            update.Text = "編集";
            delete.Text = "削除";

            // DataGridViewに追加する
            dataGridView1.Columns.Add(update);
            dataGridView1.Columns.Add(delete);

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
        private void ScreenDisplay()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

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

        private void register_Click(object sender, EventArgs e)
        {

        }
    }
}
