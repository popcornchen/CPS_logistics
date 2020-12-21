using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

/*-----------------此脚本存储所有与数据库操作相关函数--------------------*/
namespace CPS_Logistics.DataClass
{
    class MysqlClass
    {
        public static string constr = "user=root; database=test; port=8332; pwd=gotmNAOL6^NcKJ9$; server=115.236.52.123";
        public static MySqlConnection con = new MySqlConnection(constr);


        /*-----------------连接数据库--------------------*/
        public static void DB_con()
        {
            try
            {
                con = new MySqlConnection(constr);
                con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to connect the DB" + "    " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return; 
            }
        }

        /*-----------------关闭数据库--------------------*/
        public static void DB_Close()
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        /*-----------------更新数据库--------------------*/
        public static void DB_Change(string SQLstr)
        {
            if (con.State == ConnectionState.Closed) DB_con();
            MySqlCommand UpdateData = new MySqlCommand(SQLstr, con);
            UpdateData.ExecuteNonQuery();
            UpdateData.Dispose();
            //DB_Close();
        }

        /*-----------------读取数据库--------------------*/
        public static DataSet DB_set(string SQLstr, string tablename)
        {
            if (con.State == ConnectionState.Closed) DB_con();
            MySqlDataAdapter SQLdata = new MySqlDataAdapter(SQLstr, con);
            DataSet Data_Set = new DataSet();
            try
            {
                SQLdata.Fill(Data_Set, tablename);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error!!" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //DB_Close();
            return Data_Set;
        }

        /*-----------------执行插入命令--------------------*/
        public static void DB_insert(string SQLstr)
        {
            if (con.State == ConnectionState.Closed) DB_con();
            MySqlCommand InsertData = new MySqlCommand(SQLstr, con);
            InsertData.ExecuteNonQuery();
            InsertData.Dispose();
        }

    }
}
