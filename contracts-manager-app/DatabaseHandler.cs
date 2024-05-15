using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{
    /// <summary>
    /// クエリ文実行のためのハンドラクラス
    /// </summary>
    public class DatabaseHandler
    {
        // SQLサーバーに接続するために必要な情報
        private string connectionString;

        public DatabaseHandler(string connectionString) {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// DBにMERGE INTOするときのクエリ文作成、実行
        /// </summary>
        /// <param name="contact">DBに加えたい連絡先</param>
        public void MergeIntoContact(Contact contact, bool isRegist)
        {
            // 連絡先のサニタイジング
            Contact sanitaizingContact = SanitizingContact(contact);
            // クエリ文作成
            string cmdTxt = RegistOrImportQueryStatement(isRegist);
            // クエリ文実行
            DatabaseHandleExecuteNonQuery2(sanitaizingContact, cmdTxt);
        }

        /// <summary>
        /// 連絡先のサニタイジング
        /// </summary>
        /// <param name="contact">サニタイジングしたい連絡先</param>
        /// <returns>サニタイジングされた連絡先</returns>
        private Contact SanitizingContact(Contact contact)
        {
            string id = contact.id.Replace("'", "''");
            string name = contact.name.Replace("'", "''");
            string tel = contact.tel.Replace("'", "''");
            string address = contact.address.Replace("'", "''");
            string remark = contact.remark.Replace("'", "''");
            string imagePass = contact.imagePass.Replace("'", "''");
            Contact aContact = new Contact(id, name, tel, address, remark, imagePass);
            return aContact;
        }

        /// <summary>
        /// 基本的なクエリ文を実行する
        /// </summary>
        /// <param name="cmdTxt">クエリ文内容</param>
        private void DatabaseHandleExecuteNonQuery2(Contact aContact, string cmdTxt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = cmdTxt;
                        Parameterization(aContact, cmd);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
            }
        }

        /// <summary>
        /// 変数のパラメータ化
        /// </summary>
        /// <param name="aContact"></param>
        /// <param name="cmd"></param>
        private void Parameterization(Contact aContact, SqlCommand cmd)
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = aContact.id;
            cmd.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = aContact.name;
            cmd.Parameters.Add("@TEL", SqlDbType.NVarChar).Value = aContact.tel;
            cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = aContact.address;
            cmd.Parameters.Add("@REMARK", SqlDbType.NVarChar).Value = aContact.remark;
            cmd.Parameters.Add("@IMAGE_PASS", SqlDbType.NVarChar).Value = aContact.imagePass;
        }

        /// <summary>
        ///  MERGE INTO のクエリ文
        /// </summary>
        /// <returns></returns>
        private string MergeIntoDBQueryStatement()
        {
            string cmdTxt =              $@"MERGE INTO contacts AS target
                                            USING
                                                (VALUES 
                                                    (@ID,@NAME,@TEL,@ADDRESS,@REMARK,@IMAGE_PASS)
                                                ) AS source(id, name, tel, address, remark, imagePass)
                                            ON target.id = source.id 
                                            WHEN MATCHED THEN 
                                                UPDATE SET target.name = source.name, 
                                                target.tel = source.tel, 
                                                target.address = source.address, 
                                                target.remark = source.remark,
                                                target.imagePass = source.imagePass
                                            WHEN NOT MATCHED THEN";
            return cmdTxt;
        }

        /// <summary>
        /// 新規登録かインポートかで生成するクエリ文をハンドルする
        /// </summary>
        /// <param name="isRegist">新規登録か否か</param>
        /// <returns></returns>
        private string RegistOrImportQueryStatement(bool isRegist)
        {
            if (isRegist)
            {
                return   @$"{MergeIntoDBQueryStatement()}
                            INSERT (name, tel, address, remark, imagePass)
                            VALUES (source.name, source.tel, source.address, source.remark, source.imagePass);";
            }
            else
            {
                return   $@"SET IDENTITY_INSERT contacts ON;
                            {MergeIntoDBQueryStatement()}
                            INSERT (id, name, tel, address, remark, imagePass)
                            VALUES (source.id, source.name, source.tel, source.address, source.remark, source.imagePass);
                            SET IDENTITY_INSERT contacts OFF;";
            }
        }

        /// <summary>
        /// 連絡先の削除のクエリ文作成、実行
        /// </summary>
        /// <param name="id">削除したい連絡先のid</param>
        public void DeleteContact(Contact contact)
        {
            // idのサニタイジング
            Contact sanitizingContact = SanitizingContact(contact);
            string cmdTxt = $@"DELETE FROM contacts WHERE id = @ID";
            DatabaseHandleExecuteNonQuery2(sanitizingContact, cmdTxt);
        }

        /// <summary>
        /// データテーブルにクエリ文で抽出したデータをそのまま書き込む
        /// </summary>
        /// <param name="cmdTxt">クエリ文内容</param>
        /// <param name="dt">書き込みたいデータテーブル</param>
        public void DataAdaptDataTable(string cmdTxt, DataTable dt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = cmdTxt;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
            }
        }
    }
}
