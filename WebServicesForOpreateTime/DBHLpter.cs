using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebServicesForOpreateTime
{
    public class DBHLpter
    {

        public static string connstr = "Data Source=10.124.149.29;Initial Catalog=Semi-finished_Warehouse;Persist Security Info=True;User ID=sa;Password=1qaz!QAZ";



        /// <summary>
        /// 受影响行数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static int GetExecuteNonQuery(string sqlstr)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connstr;
            conn.Open();
            SqlCommand com = new SqlCommand(sqlstr, conn);
            int num = com.ExecuteNonQuery();
            conn.Close();
            return num;

        }
        /// <summary>
        /// 得到表
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sqlstr)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connstr;
            conn.Open();
            SqlCommand com = new SqlCommand(sqlstr, conn);
            SqlDataAdapter da = new SqlDataAdapter(com);
            conn.Close();
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }




    }
}