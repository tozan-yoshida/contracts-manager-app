using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{

    /// <summary>
    /// FileDialogを使う際に呼び出すクラス
    /// </summary>
    public class FileDialogUse
    {
        // 開きたいファイルダイアログ
        public FileDialog fileDialog { get; set; }

        public FileDialogUse(FileDialog fileDialog)
        {
            this.fileDialog = fileDialog;
        }

        /// <summary>
        /// ファイルダイアログを開き、OKボタンが押されたかどうかを返す
        /// </summary>
        /// <returns>OKボタンが押されたかどうか</returns>
        public bool DialogUse()
        {
            // タイトルとはじめのファイル名を設定
            DialogTitleAndName();
            // [ファイルの種類]に表示される選択肢を指定する
            fileDialog.Filter = "csvファイル(*.csv)|*.csv";
            // [ファイルの種類]ではじめに選択されるものを指定する
            fileDialog.FilterIndex = 0;
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            fileDialog.RestoreDirectory = true;
            // ダイアログのOKボタンが押されたかどうかの判定
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// フィールド fileDialog のクラスによってタイトルや初期のファイル名を変更する
        /// </summary>
        private void DialogTitleAndName()
        {
            // fileDialog が OpenFileDialog クラスの場合
            if(fileDialog is OpenFileDialog)
            {
                fileDialog.Title = "開くファイルを選択してください";
            }
            // fileDialog が SaveFileDialog クラスの場合
            else if(fileDialog is SaveFileDialog){
                DateTime dateTime = DateTime.Now;
                fileDialog.FileName = @$"連絡先_{dateTime.ToString("yyMMddhhmm")}";
                fileDialog.Title = "保存先のファイルを選択してください";
            }
        }
    }
}
