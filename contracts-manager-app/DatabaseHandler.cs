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
        public void MergeIntoContact(Contact contact)
        {
            // クエリ文作成
            string cmdTxt = MergeIntoDBQueryStatement(SanitizingContact(contact));
            // クエリ文実行
            DatabaseHandleExecuteNonQuery(cmdTxt);
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
            Contact aContact = new Contact(id, name, tel, address, remark);
            return aContact;
        }

        /// <summary>
        /// 入力情報よりMERGE INTOのクエリ文を作成する
        /// </summary>
        /// <param name="aContact">登録・更新するためのデータ</param>
        /// <returns>クエリ文</returns>
        private string MergeIntoDBQueryStatement(Contact aContact)
        {
            string cmdTxt = $@"MERGE INTO contacts AS target
                                            USING
                                                (VALUES 
                                                    ({aContact.id},'{aContact.name}','{aContact.tel}','{aContact.address}','{aContact.remark}')
                                                ) AS source(id, name, tel, address, remark)
                                            ON target.id = source.id 
                                            WHEN MATCHED THEN 
                                                UPDATE SET target.name = source.name, 
                                                target.tel = source.tel, 
                                                target.address = source.address, 
                                                target.remark = source.remark 
                                            WHEN NOT MATCHED THEN 
                                                INSERT (name, tel, address, remark)
                                                VALUES (source.name, source.tel, source.address, source.remark); ";
            return cmdTxt;
        }

        /// <summary>
        /// 基本的なクエリ文を実行する
        /// </summary>
        /// <param name="cmdTxt">クエリ文内容</param>
        private void DatabaseHandleExecuteNonQuery(string cmdTxt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = cmdTxt;
                        cmd.ExecuteNonQuery();
                    }
                }
            }catch (SqlException ex)
            {
                MessageBox.Show("エラー: "+ ex.Message);
            }
        }

        /// <summary>
        /// 連絡先の削除のクエリ文作成、実行
        /// </summary>
        /// <param name="id">削除したい連絡先のid</param>
        public void DeleteContact(string id)
        {
            // idのサニタイジング
            id = id.Replace("'", "''");
            string cmdTxt = $@"DELETE FROM contacts WHERE id = {id}";
            DatabaseHandleExecuteNonQuery(cmdTxt);
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
