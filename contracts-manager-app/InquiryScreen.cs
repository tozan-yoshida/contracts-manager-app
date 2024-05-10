using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace contracts_manager_app
{
    public partial class InquiryScreen : Form
    {
        public DataTable contacts { get; set; }

        // データベースとの接続文字列作成
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

        private RegistOrUpdate registOrUpdateScreen;

        public Contact contact1 { get; set; }

        // それぞれの情報が何列目にあるか
        private int updateIndex;    // 編集ボタン
        private int deleteIndex;    // 削除ボタン
        private int idIndex;        // id
        private int nameIndex;      // 名前
        private int telIndex;       // 電話番号
        private int addressIndex;   // メールアドレス
        private int remarkIndex;    // 備考

        public InquiryScreen()
        {
            InitializeComponent();

            // DataTableの初期化
            contacts = new DataTable();

            // DataGridViewの初期化
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

            // ボタンの背景色変更
            updateButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatStyle = FlatStyle.Flat;
            updateButton.DefaultCellStyle.BackColor = Color.LightGreen;
            deleteButton.DefaultCellStyle.BackColor = Color.Coral;

            // DataGridViewに追加する
            dataGridView1.Columns.Add(updateButton);
            dataGridView1.Columns.Add(deleteButton);            
            contacts.Columns.Add("id", typeof(int));
            contacts.Columns.Add("name", typeof(string));
            contacts.Columns.Add("tel", typeof(string));
            contacts.Columns.Add("address", typeof(string));
            contacts.Columns.Add("remark", typeof(string));

            // 情報がそれぞれ dataGridView の何列目にあるか
            updateIndex = dataGridView1.Columns["編集"].Index;
            deleteIndex = dataGridView1.Columns["削除"].Index;
            idIndex = dataGridView1.Columns["id"].Index;
            nameIndex = dataGridView1.Columns["name"].Index;
            telIndex = dataGridView1.Columns["tel"].Index;
            addressIndex = dataGridView1.Columns["address"].Index;
            remarkIndex = dataGridView1.Columns["remark"].Index;

            // dataGridViewのレイアウト用
            dataGridView1.Columns[updateIndex].FillWeight = 1.0f;
            dataGridView1.Columns[deleteIndex].FillWeight = 1.0f;
            dataGridView1.Columns[idIndex].FillWeight = 1.0f;
            dataGridView1.Columns[nameIndex].FillWeight = 4.0f;
            dataGridView1.Columns[telIndex].FillWeight = 2.6f;
            dataGridView1.Columns[addressIndex].FillWeight = 5.0f;
            dataGridView1.Columns[remarkIndex].FillWeight = 7.5f;

            // カラム名を指定
            dataGridView1.Columns[nameIndex].HeaderText = "名前";
            dataGridView1.Columns[telIndex].HeaderText = "電話番号";
            dataGridView1.Columns[addressIndex].HeaderText = "メールアドレス";
            dataGridView1.Columns[remarkIndex].HeaderText = "備考";

            // idの列を非表示にする
            dataGridView1.Columns[idIndex].Visible = false;

            dataGridView1.Columns[updateIndex].HeaderText = "";
            dataGridView1.Columns[deleteIndex].HeaderText = "";

            // 登録・編集画面のインスタンス作成
            registOrUpdateScreen = new RegistOrUpdate(this);

            contact1 = new Contact("", "", "", "", "");
        }

        /// <summary>
        /// ロード時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // DatagridViewの表示
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

                    // 行フィルターをオフにする
                    contacts.DefaultView.RowFilter = null;

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

            // dataGridViewの初期表示でセルを選択させない
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();

        }

        /// <summary>
        /// 新規登録ボタン押下時のイベント
        /// </summary>
        private void register_Click(object sender, EventArgs e)
        {
            contact1.id = "0";
            registOrUpdateScreen.update = false;
            // ボタンの表示を"登録"に変更
            registOrUpdateScreen.LabelChanger("登録", "新規追加画面");
            // 登録画面の表示
            registOrUpdateScreen.ShowDialogPlus();
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
                UpdateClick(e);
            }

            // "削除"ボタンを押したときの処理
            else if (dgv.Columns[e.ColumnIndex].Name == "削除")
            {
                DeleteClick(e);                
            }
        }

        /// <summary>
        /// 編集ボタン押下時のイベントの処理内容
        /// </summary>
        private void UpdateClick(DataGridViewCellEventArgs e)
        {
            // 編集、更新画面のボタンの表記を"更新"に変更
            registOrUpdateScreen.LabelChanger("更新", "編集画面");
            registOrUpdateScreen.update = true;
            // 押された"編集"ボタンの行の情報取得、格納
            RowInfoStore(e);

            // 遷移先の画面のテキストボックスに自動的に入力
            registOrUpdateScreen.TextBoxRegister(contact1);

            // 画面遷移
            registOrUpdateScreen.ShowDialogPlus();
        }

        /// <summary>
        /// 押された行の情報取得、格納の処理内容
        /// </summary>
        private void RowInfoStore(DataGridViewCellEventArgs e)
        {
            // 押された行の情報取得、格納
            contact1.id = dataGridView1.Rows[e.RowIndex].Cells[idIndex].Value.ToString();
            contact1.name = dataGridView1.Rows[e.RowIndex].Cells[nameIndex].Value.ToString();
            contact1.tel = dataGridView1.Rows[e.RowIndex].Cells[telIndex].Value.ToString();
            contact1.address = dataGridView1.Rows[e.RowIndex].Cells[addressIndex].Value.ToString();
            // 備考は何も入力されていない場合があるためif文で判断
            if (dataGridView1.Rows[e.RowIndex].Cells[remarkIndex].Value != null) 
                contact1.remark = dataGridView1.Rows[e.RowIndex].Cells[remarkIndex].Value.ToString();
        }

        /// <summary>
        /// 削除ボタン押下時のイベントの処理内容
        /// </summary>
        /// <param name="e"></param>
        private void DeleteClick(DataGridViewCellEventArgs e)
        {
            // 削除するかの確認メッセージボックスを表示
            DialogResult result = MessageBox.Show("連絡先を削除しますか？", "確認",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            // OKを押したときの処理
            if (result == DialogResult.OK)
            {
               PushDeleteOk(e);
            }
        }


        /// <summary>
        /// 削除許可を出したときの処理
        /// </summary>
        private void PushDeleteOk(DataGridViewCellEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // 削除したい行のidを取得
                    contact1.id = dataGridView1.Rows[e.RowIndex].Cells[idIndex].Value.ToString();
                    // クエリ文作成
                    string cmdtest = "DELETE FROM contacts WHERE id = " + contact1.id;

                    using (var cmd = new SqlCommand(cmdtest, conn))
                    {
                        // db接続
                        conn.Open();
                        // クエリ文実行
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // 画面の再表示
            ScreenDisplay();
        }

        /// <summary>
        /// 検索ボタン押下時のイベント
        /// </summary>
        private void search_Click(object sender, EventArgs e)
        {
            // 文字列が入力されている場合
            if (searchBox.Text.Length > 0)
            {
                // データにフィルターをかける
                // 条件はテキストボックスの文字列が名前、電話番号、メールアドレス、備考のいずれかの一部もしくは全部に一致
                contacts.DefaultView.RowFilter = @$"name LIKE '%{searchBox.Text}%'
                                                    OR remark LIKE'%{searchBox.Text}%' ";
            }
        }

        /// <summary>
        /// エクスポートボタン押下時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void export_Click(object sender, EventArgs e)
        {
            // エクスポートするデータが存在するかどうか
            if (contacts.Rows.Count > 0)
            {
                // エクスポートするデータ存在時の処理
                ExportEvent exportEvent = new ExportEvent(contacts);
                exportEvent.ExportEventOccur();
            }
            else
            {
                MessageBox.Show("エクスポートするデータが存在しません");
            }
        }

        /// <summary>
        /// インポートボタン押下時のイベント
        /// </summary>
        private void import_Click(object sender, EventArgs e)
        {
            ImportEvent importEvent = new ImportEvent(this, connectionString);
            importEvent.ImportEventOccur();
        }

        /// <summary>
        /// 全連絡先表示ボタン押下時のイベント
        /// </summary>
        private void showAllContacts_Click(object sender, EventArgs e)
        {
            ScreenDisplay();
        }
    }
}
