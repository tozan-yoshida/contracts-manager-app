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

        // 登録・更新画面のフォーム
        private RegistOrUpdate registOrUpdateScreen;

        // 連絡先の格納クラス
        public Contact contact1 { get; set; }

        // それぞれの情報が何列目にあるか
        private int updateIndex;    // 編集ボタン
        private int deleteIndex;    // 削除ボタン
        private int idIndex;        // id
        private int nameIndex;      // 名前
        private int telIndex;       // 電話番号
        private int addressIndex;   // メールアドレス
        private int remarkIndex;    // 備考

        // ボタン列
        private DataGridViewButtonColumn updateButtonColumn;
        private DataGridViewButtonColumn deleteButtonColumn;


        public DatabaseHandler databaseHandler { get; set; }

        public InquiryScreen()
        {
            InitializeComponent();

            // DataTableの初期化
            contacts = new DataTable();

            // DataGridViewの初期化
            dataGridView1.DataSource = contacts;

            // DataGridViewButtonColumnの作成
            updateButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn = new DataGridViewButtonColumn();

            // 列の名前を設定
            updateButtonColumn.Name = "編集";
            deleteButtonColumn.Name = "削除";

            // すべてのボタンに"編集"、"削除"と表示する
            updateButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            updateButtonColumn.Text = "編集";
            deleteButtonColumn.Text = "削除";

            // ボタンの背景色変更
            updateButtonColumn.FlatStyle = FlatStyle.Flat;
            deleteButtonColumn.FlatStyle = FlatStyle.Flat;
            updateButtonColumn.DefaultCellStyle.BackColor = Color.LightGreen;
            deleteButtonColumn.DefaultCellStyle.BackColor = Color.Coral;

            // DataGridViewに追加する
            dataGridView1.Columns.Add(updateButtonColumn);
            dataGridView1.Columns.Add(deleteButtonColumn);
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
            dataGridView1.Columns[telIndex].FillWeight = 2.8f;
            dataGridView1.Columns[addressIndex].FillWeight = 5.0f;
            dataGridView1.Columns[remarkIndex].FillWeight = 7.5f;

            // カラム名を指定
            dataGridView1.Columns[updateIndex].HeaderText = "";
            dataGridView1.Columns[deleteIndex].HeaderText = "";
            dataGridView1.Columns[nameIndex].HeaderText = "名前";
            dataGridView1.Columns[telIndex].HeaderText = "電話番号";
            dataGridView1.Columns[addressIndex].HeaderText = "メールアドレス";
            dataGridView1.Columns[remarkIndex].HeaderText = "備考";

            // idの列を非表示にする
            dataGridView1.Columns[idIndex].Visible = false;

            // データベースハンドラのインスタンス作成
            databaseHandler = new DatabaseHandler(connectionString);

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
        /// DataGridViewの表をDBより(再)取得する
        /// </summary>
        public void ScreenDisplay()
        {
            // 表示の初期化
            contacts.Clear();

            // 行フィルターをオフにする
            contacts.DefaultView.RowFilter = null;

            // DB上からデータテーブルにデータを渡す
            databaseHandler.DataAdaptDataTable("SELECT * FROM contacts", contacts);

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
            // 削除したい行のidを取得
            contact1.id = dataGridView1.Rows[e.RowIndex].Cells[idIndex].Value.ToString();

            // Deleteクエリ文作成、実行
            databaseHandler.DeleteContact(contact1.id);

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
                // 条件はテキストボックスの文字列が名前、備考のいずれかの一部もしくは全部に一致
                contacts.DefaultView.RowFilter = @$"name LIKE '%{searchBox.Text}%'
                                                    OR remark LIKE'%{searchBox.Text}%' ";
            }
            // dataGridViewの初期表示でセルを選択させない
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();
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
                ExportEvent();
            }
            else
            {
                MessageBox.Show("エクスポートするデータが存在しません");
            }
        }

        /// <summary>
        /// エクスポートするデータ存在時の処理
        /// </summary>
        private void ExportEvent()
        {
            // csvファイルのパスをフォルダを指定して取得
            FileDialogUse fileDialogUse = new FileDialogUse(new SaveFileDialog());

            // CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 書き込むフォルダの保存先と名前を指定する
            // 指定してOKボタンを押した場合、以下の処理を行う
            if (fileDialogUse.DialogUse())
            {
                ExportPushOK(fileDialogUse.fileDialog.FileName, encoding);
                MessageBox.Show($@"{fileDialogUse.fileDialog.FileName}にエクスポートしました");
            }
        }

        /// <summary>
        /// ダイアログでOK押下時の処理
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        private void ExportPushOK(string fileName, System.Text.Encoding encoding)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(fileName, false, encoding))
                {
                    int colCount = contacts.Columns.Count;  // 列の数
                    int lastColIndex = colCount - 1;        // 最後の列の列番号

                    // ヘッダを書き込む
                    WriteHeader(sr, colCount, lastColIndex);
                    // 改行する
                    sr.Write("\r\n");
                    // レコードを書き込む
                    WriteRecord(sr, colCount, lastColIndex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// ヘッダの書き込み処理
        /// </summary>
        /// <param name="colCount">列の数</param>
        /// <param name="lastColIndex">最後の列の列番号</param>
        private void WriteHeader(StreamWriter sr, int colCount, int lastColIndex)
        {
            // 列数だけ繰り返し
            for (int i = 0; i < colCount; i++)
            {
                // ヘッダの取得
                string field = contacts.Columns[i].Caption;
                // csvへの書き込み処理
                WriteCsv(sr, lastColIndex, i, field);
            }
        }

        /// <summary>
        /// レコードの書き込み処理
        /// </summary>
        /// <param name="colCount">列の数</param>
        /// <param name="lastColIndex">最後の列の列番号</param>
        private void WriteRecord(StreamWriter sr, int colCount, int lastColIndex)
        {
            foreach (DataRow row in contacts.Rows)
            {
                for (int i = 0; i < colCount; i++)
                {
                    // フィールドの取得
                    string field = row[i].ToString();
                    // csvへの書き込み処理
                    WriteCsv(sr, lastColIndex, i, field);
                }
                // 改行する
                sr.Write("\r\n");
            }
        }

        /// <summary>
        /// csvファイルへの書き込み処理
        /// </summary>
        /// <param name="lastColIndex">最後の列の列番号</param>
        /// <param name="field">csvに書きこむ元データ</param>
        private void WriteCsv(StreamWriter sr, int lastColIndex, int i, string field)
        {
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

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private String EncloseDoubleQuotesIfNeed(string field)
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
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            // ファイルダイアログを使用する際のインスタンス
            FileDialogUse fileDialogUse = new FileDialogUse(new OpenFileDialog());

            // ダイアログを表示、OKボタンが押されたならインポートの処理を行う
            if (fileDialogUse.DialogUse())
            {
                ImportPushOK(fileDialogUse.fileDialog.FileName, encoding);
            }
        }

        /// <summary>
        /// ダイアログでOK押下時の処理
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        private void ImportPushOK(string fileName, System.Text.Encoding encoding)
        {
            try
            {
                // 読み込みたいCSVファイルをダイアログより選択して開く
                using (StreamReader sr = new StreamReader(fileName, encoding, false))
                {
                    // 1行目ではないかどうか
                    // 1行目はヘッダーになっているため書き出してはいけない
                    bool notFirst = false;
                    // 末尾まで繰り返す
                    ReadCsv(sr, notFirst);
                    // 画面の更新
                    ScreenDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
            }
        }

        /// <summary>
        /// csvファイルの読み込みの繰り返し処理
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="notFirst">読み込んだ行が1行目か否か</param>
        private void ReadCsv(StreamReader sr, bool notFirst)
        {
            while (!sr.EndOfStream)
            {
                // CSVファイルを1行読み込む
                string line = sr.ReadLine();

                // 2行目以降の場合
                if (notFirst)
                {
                    ReadCsvNotFirst(line);
                }
                notFirst = true;
            }
        }

        /// <summary>
        /// csvファイルの2行目以降を読み込んだ時の処理
        /// </summary>
        /// <param name="line"></param>
        private void ReadCsvNotFirst(string line)
        {
            // 読み込んだ1行をカンマ事に分けて配列に格納する
            string[] values = line.Split(',');

            // 配列からコンタクトクラスに格納する
            Contact contact = new Contact(values[0], values[1], values[2], values[3], values[4]);
            // コンタクトクラスをDBにインポート
            databaseHandler.MergeIntoContact(contact);
        }

        /// <summary>
        /// 再読込ボタン押下時のイベント
        /// </summary>
        private void showAllContacts_Click(object sender, EventArgs e)
        {
            ScreenDisplay();
            searchBox.Text = "";
        }
    }
}
