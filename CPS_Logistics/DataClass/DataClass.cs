using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CPS_Logistics.DataClass
{
    class DataClass
    {
        
        #region Pubilc Variable 
        
        public static string Login_ID = "";                                                             // 记录当前登陆用户的编号
        public static string Login_Name = "";                                                       //记录当前登陆用户的姓名
        public static string Data_SQL = "";                                                          //记录SQL语句
        public static string Data_Table = "";                                                        //记录数据库表名
        public static string Data_Field = "";                                                         //记录要修改的字段名
        
        public static string Data_Sqlcon = "data source=192.168.1.220;database=tb_Fms;uid=sa;pwd=sa";
        public static SqlConnection Data_Con = new SqlConnection(Data_Sqlcon);                                                   // 创建数据库连接
        #endregion

        #region  Method
       
        public static void  DB_Con()
        {
            try
            {
                Data_Con = new SqlConnection(Data_Sqlcon);
                Data_Con.Open();
            }

            catch(Exception e)
            {
                MessageBox.Show("Failed to connect the DB" + "    " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return; 
            }
        }
        public static void DB_Close()
        {
            if(Data_Con.State ==ConnectionState .Open)
            {
                Data_Con.Close();
            }
        }
        public static SqlDataReader DB_Read (string SQLstr)
        {
            DB_Con();
            SqlCommand Data_Command = new SqlCommand(SQLstr, Data_Con);
            SqlDataReader Data_read = Data_Command.ExecuteReader();
            return Data_read; 
        }
        public static void  DB_Change(string SQLstr)
        {
            DB_Con();
            SqlCommand Data_Change =new SqlCommand (SQLstr ,Data_Con);
            Data_Change.ExecuteNonQuery();
            Data_Change .Dispose ();
            DB_Close();
        }
        public static DataSet DB_Set(string SQLstr, string tableName)
        {
            DB_Con();
            SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, Data_Con);
            DataSet Data_Set = new DataSet();
            try
            {
                SQLda.Fill(Data_Set, tableName);
            }
            catch(Exception str)
            {
                MessageBox.Show("Error!!" + str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DB_Close();
            return Data_Set;
        }
        #endregion
    }
}
