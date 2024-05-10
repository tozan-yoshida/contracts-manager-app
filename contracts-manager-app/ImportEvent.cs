using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{
    public class ImportEvent
    {
        private InquiryScreen InquiryScreen;
        private string connectionString;
      
        public ImportEvent(InquiryScreen InquiryScreen, string connectionString)
        {
            this.InquiryScreen = InquiryScreen;
            this.connectionString = connectionString;
        }

        public void ImportEventOccur()
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

        private void ImportPushOK(string fileName, System.Text.Encoding encoding)
        {
            try
            {
                // 読み込みたいCSVファイルをダイアログより選択して開く
                using (StreamReader sr = new StreamReader(fileName, encoding, false))
                {
                    // 1行目ではないかどうか
                    // 1行目はヘッダーになっているため読み込んではいけない
                    bool notFirst = false;
                    // 末尾まで繰り返す
                    ReadCsv(sr, notFirst);
                    // 画面の更新
                    InquiryScreen.ScreenDisplay();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ReadCsv(StreamReader sr, bool notFirst)
        {
            while (!sr.EndOfStream)
            {
                // CSVファイルの1行を読み込む
                string line = sr.ReadLine();

                // 2行目以降の場合
                if (notFirst)
                {
                    ReadCsvNotFirst(line);
                }
                notFirst = true;
            }
        }

        private void ReadCsvNotFirst(string line)
        {
            // 読み込んだ1行をカンマ事に分けて配列に格納する
            string[] values = line.Split(',');

            // 配列からリストに格納する
            List<string> lists = new List<string>();
            lists.AddRange(values);

            // リストからDBにインポート
            importDB(lists);
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
