using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace contracts_manager_app
{

    public partial class RegistOrUpdate : Form
    {


        public bool update { get; set; } = false;
        private bool error { get; set; } = false;
        private InquiryScreen inquiryScreen;
        private DatabaseHandler databaseHandler;
        private string imagePass;



        public RegistOrUpdate(InquiryScreen inquiryScreen)
        {
            this.inquiryScreen = inquiryScreen;
            databaseHandler = inquiryScreen.databaseHandler;
            InitializeComponent();
        }


        private void registOrUpdate_Click(object sender, EventArgs e)
        {
            // エラーの初期化
            ErrorInitialization();

            // エラー判定
            CheckError();

            // エラーが一つも起きていないときに限り下の処理を行う
            // 新規登録、更新処理
            if (!error)
            {
                // 画像ファイルが選択されているか
                // それによって生成する連絡先オブジェクトの内容を変更する
                bool addImageFile = CheckCanAddImageFile();
                Contact contact = toContact(addImageFile);

                // DBに連絡先をマージする
                RegistOrUpdateToDB(contact);
            }
        }

        /// <summary>
        /// エラー判定とエラー用テキストボックスの初期化
        /// </summary>
        private void ErrorInitialization()
        {
            // 諸々初期化
            error = false;
            nameError.Text = string.Empty;
            telError.Text = string.Empty;
            addressError.Text = string.Empty;
            remarkError.Text = string.Empty;
        }

        /// <summary>
        /// 各種エラー判定を行う
        /// </summary>
        private void CheckError()
        {
            // 名称エラー判定
            CheckNameError();

            // 電話番号エラー判定
            CheckTelError();

            // アドレスエラー判定
            CheckAddressError();

            // 備考エラー判定
            CheckRemarkError();
        }

        /// <summary>
        /// 名称エラー判定
        /// </summary>
        private void CheckNameError()
        {
            // 名前が入力されているか
            if (nameBox.Text.Length >= 1)
            {
                CheckNameExist();
            }
            else
            {
                error = true;
                nameError.Text = "名前を入力してください";
            }
        }

        /// <summary>
        /// nameBox に文字列が存在する場合の処理
        /// </summary>
        private void CheckNameExist()
        {
            // 同一の名前をもつテーブルのidを格納するリスト
            List<string> idList = new List<string>();
            // 名前が同一の行を連絡先テーブルから抽出、idをリスト化
            SameNameList(idList);

            // 更新・新規登録の重複エラーチェック
            CheckNameUploadOrRegist(idList);
        }

        /// <summary>
        /// 名前が同一の行を連絡先テーブルから抽出、idをリスト化
        /// </summary>
        /// <param name="idList"></param>
        private void SameNameList(List<string> idList)
        {
            foreach (DataRow dr in inquiryScreen.contacts.Rows)
            {
                if (dr["name"].Equals(nameBox.Text))
                {
                    idList.Add(dr["id"].ToString());
                }
            }
        }

        /// <summary>
        /// 更新か登録かによって処理をハンドルさせる
        /// </summary>
        /// <param name="idList"></param>
        private void CheckNameUploadOrRegist(List<string> idList)
        {
            // 更新の場合
            if (update)
            {
                CheckUpdateNameError(idList);
            }
            // 新規登録の場合
            else
            {
                // 同じ名前のオブジェクトが存在している場合、重複エラー
                if (idList.Any())
                {
                    DuplicationError();
                }
            }
        }

        /// <summary>
        /// 更新の場合の重複エラーの判断処理
        /// </summary>
        /// <param name="idList"></param>
        private void CheckUpdateNameError(List<string> idList)
        {
            // idが別で同じ名前のオブジェクトが存在しているか
            foreach (var item in idList)
            {
                // している場合重複エラー
                if (!item.Equals(inquiryScreen.contact1.id))
                {
                    DuplicationError();
                    break;
                }
            }
        }

        /// <summary>
        /// 重複エラーの処理
        /// </summary>
        private void DuplicationError()
        {
            error = true;
            nameError.Text = "この名前は既に登録されています";
        }

        private void CheckTelError()
        {
            if (telBox.Text.Length == 0)
            {
                error = true;
                telError.Text = "数字を15字以内で入力してください";
            }
        }

        /// <summary>
        /// アドレスのエラーの処理
        /// アドレスには@が含まれている必要がある
        /// </summary>
        private void CheckAddressError()
        {
            if (addressBox.Text.Length == 0)
            {
                error = true;
                addressError.Text = "メールアドレスを30字以内で入力してください";
            }
            else if (!(addressBox.Text.Contains("@")))
            {
                error = true;
                addressError.Text = "メールアドレスは@が含まれる必要があります";
            }
        }

        /// <summary>
        /// 備考のエラーの処理
        /// </summary>
        private void CheckRemarkError()
        {
            if (remarkBox.Text.Length > 30)
            {
                error = true;
                remarkError.Text = "備考は30字以内で入力してください";
            }
        }

        /// <summary>
        ///  画像を設定する選択がされているか
        /// </summary>
        /// <returns></returns>
        private bool CheckCanAddImageFile()
        {
            if (imagePass != null && !deleteImage.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 登録・編集の処理
        /// </summary>
        private void RegistOrUpdateToDB(Contact contact)
        {
            // 入力情報をDBにMERGE INTO する
            databaseHandler.MergeIntoContact(contact);
            // dataGridViewを再表示
            inquiryScreen.ScreenDisplay();
            // このフォームを閉じる
            this.Close();
        }

        /// <summary>
        /// 入力情報をContactクラスに格納する
        /// </summary>
        /// <returns>入力情報を格納したContactクラス</returns>
        private Contact toContact(bool isImage)
        {
            Contact contact;
            string id = inquiryScreen.contact1.id;
            string name = nameBox.Text;
            string tel = telBox.Text;
            string address = addressBox.Text;
            string remark = remarkBox.Text;

            if (isImage)
            {
                contact = new Contact(id, name, tel, address, remark, imagePass);
            }
            else
            {
                contact = new Contact(id, name, tel, address, remark);
            }
            return contact;
        }

        /// <summary>
        /// ボタンとラベルの表示名を変更する
        /// </summary>
        /// <param name="buttonName">変更するボタンのnameプロパティ</param>
        /// <param name="formName">変更するラベルのnameプロパティ</param>
        public void LabelChanger(string buttonName, string formName, string imagePass)
        {
            enter.Text = buttonName;
            formInfo.Text = formName;
            this.imagePass = imagePass;
        }

        /// <summary>
        /// テキストボックスを事前に入力状態にしておくためのメソッド
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tel"></param>
        /// <param name="address"></param>
        /// <param name="remark"></param>
        public void TextBoxRegister(Contact contact)
        {
            nameBox.Text = contact.name;
            telBox.Text = contact.tel;
            addressBox.Text = contact.address;
            remarkBox.Text = contact.remark;
            
        }

        /// <summary>
        /// 電話番号のテキストボックスの入力制限設定
        /// </summary>
        private void telBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースが押された時は有効(Deleteキーも有効)
            if (e.KeyChar == '\b') return;

            // 数値0～9以外が押されたときはイベントをキャンセルする
            if (e.KeyChar < '0' || '9' < e.KeyChar) e.Handled = true;
        }

        /// <summary>
        /// 登録・編集画面を初期化して表示
        /// </summary>
        public void ShowDialogPlus()
        {
            // エラーの初期化
            ErrorInitialization();

            // 新規登録の場合、テキストボックスも初期化
            if (!update)
            {
                TextBoxInitialization();
            }
            ShowDialog();
        }

        /// <summary>
        /// テキストボックスの初期化
        /// </summary>
        private void TextBoxInitialization()
        {
            nameBox.Text = string.Empty;
            telBox.Text = string.Empty;
            addressBox.Text = string.Empty;
            remarkBox.Text = string.Empty;
        }

        /// <summary>
        /// ファイル選択ボタン押下時の処理
        /// </summary>
        private void fileChoice_Click(object sender, EventArgs e)
        {
            // ファイルダイアログを使用する際のインスタンス
            FileDialogUse fileDialogUse = new FileDialogUse(new OpenFileDialog(), "picture");

            // ダイアログを表示、OKボタンが押されたなら画像表示の処理を行う
            if (fileDialogUse.DialogUse())
            {
                FileChoicePushOK(fileDialogUse.fileDialog.FileName);
            }
        }

        /// <summary>
        /// ファイル選択ボタン押下時のファイルダイアログにて
        /// OKボタン押下時の処理
        /// </summary>
        /// <param name="fileName"></param>
        private void FileChoicePushOK(string fileName)
        {
            imagePass = fileName;
            pictureBox1.Image = Image.FromFile(fileName);
        }

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistOrUpdate_Load(object sender, EventArgs e)
        {
            // チェックボックスの初期化
            deleteImage.Checked = false;

            // 表示する画像の分岐
            // 編集かつすでに画像が設定されている場合のみ、その画像が表示される
            if (imagePass != "")
            {
                pictureBox1.Image = new Bitmap(imagePass);
            }
            else
            {
                pictureBox1.Image = Properties.Resources.kkrn_icon_user_1;
            }
        }
    }
}
