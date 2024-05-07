using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contracts_manager_app
{

    public partial class Form2 : Form
    {

        private bool error { get; set; } = false;
        public bool update { get; set; } = false;
        private Form1 f1;

        public Form2(Form1 f1)
        {
            this.f1 = f1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void registOrUpdate_Click(object sender, EventArgs e)
        {
            // 諸々初期化
            error = false;
            nameError.Text = string.Empty;
            telError.Text = string.Empty;
            addressError.Text = string.Empty;
            remarkError.Text = string.Empty;


            // 名称エラー判定
            // 名前が入力されているか
            if (nameBox.Text.Length >= 1)
            {
                // 名前が同一の行を連絡先テーブルから抽出、リスト化

                if (update)
                {

                }
            }
            else
            {
                error = true;
                nameError.Text = "名前を入力してください";
            }

            // 番号エラー判定
            // 1字以上15字以下か(数字のみになるようにテキストボックスの設定をしている)
            if (!(telBox.Text.Length >= 1 && telBox.Text.Length <= 15))
            {
                error = true;
                telError.Text = "電話番号を15字以内で入力してください";
            }

            // アドレスエラー判定
            // 文字数判定
            if(!(addressBox.Text.Length >= 1 &&  addressBox.Text.Length <= 30))
            {
                error = true;
                addressError.Text = "メールアドレスを30字以内で入力してください";
            }
            // @がアドレス内に存在しているか
            else if (!(addressBox.Text.Contains("@")))
            {
                error = true;
                addressError.Text = "メールアドレスは@が含まれる必要があります";
            }

            if(!(remarkBox.Text.Length <= 30))
            {
                error = true;
                remarkError.Text = "備考は30字以内である必要があります";
            }

            if (error) return;
            else
            {
                // 入力情報をdbにmerge into

                // dataGridViewを再表示

                // このページを閉じる
                this.Close();
            }
        }

        public void LabelChanger(string buttonName)
        {
            registOrUpdate.Text = buttonName;
        }

        /// <summary>
        /// テキストボックスを事前に入力状態にしておくためのメソッド
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tel"></param>
        /// <param name="address"></param>
        /// <param name="remark"></param>
        public void TextBoxRegester(string name, string tel, string address, string remark)
        {
            nameBox.Text = name;
            telBox.Text = tel;
            addressBox.Text = address;
            remarkBox.Text = remark;
        }

        /// <summary>
        /// 電話番号のテキストボックスの入力制限設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void telBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースが押された時は有効(Deleteキーも有効)
            if (e.KeyChar == '\b') return;

            // 数値0～9以外が押されたときはイベントをキャンセルする
            if(e.KeyChar < '0' || '9' < e.KeyChar) e.Handled  = true;
        }
    }
}
