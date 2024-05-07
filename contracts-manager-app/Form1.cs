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

        public bool update; // 編集か新規登録かの分岐
        public bool error;
        private Form2 f2;

        public int id { get; }
        public string name { get; }
        public string tel { get; }
        public string address {  get; }
        public string remark {  get; }

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
            update = false;

            f2 = new Form2();

            id = 0;
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
            update = false;
            f2.LabelChanger("登録");
            f2.ShowDialog();
        }

        private void RegistOrUpdate(bool update)
        {
            error = false;
            if(update)
            {
                f2.LabelChanger("更新");
            }
            else
            {
                f2.LabelChanger("登録");
            }

            f2.Show();

        }
    }
}
