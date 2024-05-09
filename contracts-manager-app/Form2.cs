using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

        
        public bool update { get; set; } = false;
        private bool error { get; set; } = false;
        private Form1 f1;
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";



        public Form2(Form1 f1)
        {
            this.f1 = f1;
            InitializeComponent();
        }


        private void registOrUpdate_Click(object sender, EventArgs e)
        {
            // 諸々初期化
            error = false;
            nameError.Text = string.Empty;
            telError.Text = string.Empty;
            addressError.Text = string.Empty;
            remarkError.Text = string.Empty;


            // エラー判定

            // 名称エラー判定
            // 名前が入力されているか
            if (nameBox.Text.Length >= 1)
            {
                List<string> idList = new List<string>();
                // 名前が同一の行を連絡先テーブルから抽出、リスト化
                foreach (DataRow dr in f1.contacts.Rows)
                {
                    if (dr["name"].Equals(nameBox.Text))
                    {
                        idList.Add(dr["id"].ToString());
                    }
                }

                // 更新の場合
                if (update)
                {
                    // idが別で同じ名前のオブジェクトが存在しているか
                    foreach (var item in idList)
                    {
                        // している場合重複エラー
                        if(!item.Equals(f1.id))
                        {
                            error = true;
                            nameError.Text = "この名前は既に登録されています";
                            break;
                        }
                    }
                }
                // 新規登録の場合
                else
                {
                    // 同じ名前のオブジェクトが存在している場合、重複エラー
                    if (idList.Any())
                    {
                        error = true;
                        nameError.Text = "この名前は既に登録されています";
                    }
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

            // エラーが一つも起きていないときに限り下の処理を行う
            // 新規登録、更新処理
            if (!error)            
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using(SqlCommand cmd = conn.CreateCommand())
                        {
                            // 入力情報をdbにmerge intoするためのクエリ文
                            cmd.CommandText =   "MERGE INTO contacts AS target " +
                                                "USING " +
                                                    "(VALUES " +
                                                        "(" + f1.id + ",'" + nameBox.Text + "','" + telBox.Text + "','" + addressBox.Text + "','" + remarkBox.Text + "') " +
                                                    ") AS source(id, name, tel, address, remark) " +
                                                "ON target.id = source.id " +
                                                "WHEN MATCHED THEN " +
                                                    "UPDATE SET target.name = source.name, " +
                                                    "target.tel = source.tel, " +
                                                    "target.address = source.address, " +
                                                    "target.remark = source.remark " +
                                                "WHEN NOT MATCHED THEN " +
                                                    "INSERT (name, tel, address, remark) " +
                                                    "VALUES (source.name, source.tel, source.address, source.remark); ";
                            // db接続
                            conn.Open();
                            // クエリ文実行
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                // dataGridViewを再表示
                f1.ScreenDisplay();

                // このフォームを閉じる
                this.Close();
            }
        }

        public void LabelChanger(string buttonName, string formName)
        {
            registOrUpdate.Text = buttonName;
            formInfo.Text = formName;
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
        private void telBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースが押された時は有効(Deleteキーも有効)
            if (e.KeyChar == '\b') return;

            // 数値0～9以外が押されたときはイベントをキャンセルする
            if(e.KeyChar < '0' || '9' < e.KeyChar) e.Handled  = true;
        }

        /// <summary>
        /// 登録・編集画面を初期化して表示
        /// </summary>
        public void ShowDialogPlus()
        {
            // 諸々初期化
            error = false;
            nameError.Text = string.Empty;
            telError.Text = string.Empty;
            addressError.Text = string.Empty;
            remarkError.Text = string.Empty;

            // 新規登録の場合、テキストボックスも初期化
            if (!update)
            {
                nameBox.Text = string.Empty;
                telBox.Text = string.Empty;
                addressBox.Text = string.Empty;
                remarkBox.Text = string.Empty;
            }
            ShowDialog();
        }
    }
}
