using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{
    public class ExportEvent
    {
        private DataTable contacts;

        public ExportEvent(DataTable contacts)
        {
            this.contacts = contacts;
        }


        /// <summary>
        /// エクスポートするデータ存在時の処理
        /// </summary>
        public void ExportEventOccur()
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
        /// フォルダ選択時OKを押下時の処理
        /// </summary>
        /// <param name="fileName">保存するファイル名のフルパス</param>
        /// <param name="encoding">CSVファイルに書き込むときに使うEncoding</param>
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

    }
}
