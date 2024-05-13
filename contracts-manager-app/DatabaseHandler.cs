using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        /// 基本的なクエリ文を実行する
        /// </summary>
        /// <param name="cmdTxt">クエリ文内容</param>
        public void DatabaseHandleExecuteNonQuery(string cmdTxt)
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
                Debug.WriteLine(ex.Message);
            }
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
                Debug.WriteLine("ｴﾗｰ" + ex.Message);
            }
        }
    }
}
