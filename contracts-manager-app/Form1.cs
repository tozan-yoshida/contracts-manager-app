using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contracts;
        static  // データベースとの接続
                // 接続文字列作成
            string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = test2; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

        public Form1()
        {
            InitializeComponent();

            // DataTableの初期化
            contracts = new DataTable();
            contracts.Columns.Add("id", typeof(int));
            contracts.Columns.Add("name", typeof(string));
            contracts.Columns.Add("tel", typeof(string));
            contracts.Columns.Add("address",  typeof(string));
            contracts.Columns.Add("remark", typeof(string));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DataGridViewの初期化
            // カラム数を指定
            dataGridView1.ColumnCount = 4;

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
            dataGridView1.Columns[0].HeaderText = "名前";
            dataGridView1.Columns[1].HeaderText = "電話番号";
            dataGridView1.Columns[2].HeaderText = "メールアドレス";
            dataGridView1.Columns[3].HeaderText = "備考";

            // データの追加テスト
            dataGridView1.Rows.Add("名前", "電話番号", "メールアドレス", "備考");

        }

        /// <summary>
        /// 画面を（再）表示する
        /// </summary>
        private void ScreenDisplay()
        {
           

        }
    }
}
