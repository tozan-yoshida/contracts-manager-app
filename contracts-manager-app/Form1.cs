using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.IO;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contacts { get; set; }

        public DataTable searchedDT { get; set; }

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

            searchedDT = new DataTable();

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
        /// 新規登録ボタン押下時のイベント
        /// </summary>
        private void register_Click(object sender, EventArgs e)
        {
            id = "0";
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
            else if (dgv.Columns[e.ColumnIndex].Name == "削除")
            {
                DialogResult result = MessageBox.Show("連絡先を削除しますか？", "質問",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                // OKを押したときの処理
                if (result == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            // 削除したい行のidを取得
                            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                            // クエリ文作成
                            string cmdtest = "DELETE FROM contacts WHERE id = " + id;

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
                    finally
                    {
                        // 画面の再表示
                        ScreenDisplay();
                    }
                }
            }
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
                contacts.DefaultView.RowFilter = "name LIKE '%" + searchBox.Text + "%' " +
                                                 "OR tel LIKE'%" + searchBox.Text + "%' " +
                                                 "OR address LIKE'%" + searchBox.Text + "%' " +
                                                 "OR remark LIKE'%" + searchBox.Text + "%' ";
            }
        }

        /// <summary>
        /// エクスポートボタン押下時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void export_Click(object sender, EventArgs e)
        {
            if (contacts.Rows.Count > 0)
            {
                // 日時取得
                DateTime dt = DateTime.Now;

                // csvファイルのパス、ファイル名は連絡先_[年月日時間分]
                string csvPath = @"C:\Users\toru_yoshida\source\repos\contracts-manager-app\連絡先_"
                                    + dt.ToString("yyMMddHHmm") + ".csv";


                // CSVファイルに書き込むときに使うEncoding
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

                // 書き込むファイルを開く
                StreamWriter sr = new StreamWriter(csvPath, false, enc);

                int colCount = contacts.Columns.Count;
                int lastColIndex = colCount - 1;

                // ヘッダを書き込む
                for (int i = 0; i < colCount; i++)
                {
                    // ヘッダの取得
                    string field = contacts.Columns[i].Caption;
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    // フィールドを書き込む
                    sr.Write(field);
                    // カンマを書き込む
                    if (lastColIndex > i)
                    {
                        sr.Write(',');
                    }

                }
                // 改行する
                sr.Write("\r\n");

                // レコードを書き込む
                foreach (DataRow row in contacts.Rows)
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        // フィールドの取得
                        string field = row[i].ToString();
                        // "で囲む
                        field = EncloseDoubleQuotesIfNeed(field);
                        // フィールドを書き込む
                        sr.Write(field);
                        // カンマを書き込む
                        if (lastColIndex > i)
                        {
                            sr.Write(',');
                        }
                    }
                    // 改行する
                    sr.Write("\r\n");
                }
                sr.Close();

                MessageBox.Show("エクスポートしました");
            }
            else
            {
                MessageBox.Show("エクスポートするデータが存在しません");
            }
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf('"') > -1)
            {
                //"を""とする
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }

        /// <summary>
        /// インポートボタン押下時のイベント
        /// </summary>
        private void import_Click(object sender, EventArgs e)
        {
            // CSVファイルを読み取る時に使うEncoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 読み込みたいCSVファイルをダイアログより選択して開く
            using (StreamReader sr = new StreamReader(DialogOpen(), enc, false))
            {
                // 1行目ではないかどうか
                // 1行目はヘッダーになっているため読み込んではいけない
                bool notFirst = false;
                // 末尾まで繰り返す
                while(!sr.EndOfStream)
                {
                    // CSVファイルの1行を読み込む
                    string line = sr.ReadLine();

                    // 2行目以降の場合
                    if (notFirst)
                    {
                        // 読み込んだ1行をカンマ事に分けて配列に格納する
                        string[] values = line.Split(',');

                        // 配列からリストに格納する
                        List<string> lists = new List<string>();
                        lists.AddRange(values);

                        // リストからDBにインポート
                        importDB(lists);
                    }
                    notFirst = true;
                }
                // 画面の更新
                ScreenDisplay();
            }

        }

        private string DialogOpen()
        {
            // OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //ファイルの種類に表示される選択肢を指定する
            ofd.Filter = "csvファイル(*.csv)|*.csv";
            // ファイルの種類ではじめに選択されるものを指定する
            ofd.FilterIndex = 0;
            // タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            // ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
            }

            return ofd.FileName;
        }

        /// <summary>
        /// データベースにインポートする際のクエリの処理
        /// </summary>
        /// <param name="lists"></param>
        private void importDB(List<string> lists)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // クエリ文作成
                    string cmdtxt = "SET IDENTITY_INSERT contacts ON " +
                                    "MERGE INTO contacts AS target " +
                                    "USING " +
                                        "(VALUES (" + lists[0] + ",'" + lists[1] + "','" + lists[2] + "','" + lists[3] + "','" + lists[4] + "')" +
                                        ")AS source(id, name, tel, address, remark) " +
                                    "ON target.id = source.id " +
                                    "WHEN MATCHED THEN " +
                                        "UPDATE SET target.name = source.name, " +
                                        "target.tel = source.tel, " +
                                        "target.address = source.address, " +
                                        "target.remark = source.remark " +
                                    "WHEN NOT MATCHED THEN " +
                                        "INSERT(id, name, tel, address, remark) " +
                                        "VALUES(source.id, source.name, source.tel, source.address, source.remark); " +
                                  "SET IDENTITY_INSERT contacts OFF";

                    // MessageBox.Show(cmdtxt);
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = cmdtxt;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
