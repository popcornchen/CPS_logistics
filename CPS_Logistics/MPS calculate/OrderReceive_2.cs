using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPS_Logistics
{
    
    public partial class OrderReceive_2 : Form
    {
        public static string Company;
        public static string CEO;
        public static string Address;
        public static string Tele;
        

        public OrderReceive_2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 13 && e.KeyChar != 8&&e.KeyChar!=43&&e.KeyChar!=45)
            {
                MessageBox.Show("请输入正常联系方式", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            /*if(textBox1.Text !=""&&textBox2.Text !=""&&textBox3.Text !=""&&textBox4.Text !="")
            {
                Company = textBox1.Text;
                CEO = textBox2.Text;
                Address = textBox3.Text;
                Tele = textBox4.Text;
                StringBuilder SQLstr = new StringBuilder();
                SQLstr.Append("INSERT into tb_Order");
                SQLstr.Append("(ProductType,StartTime,EndTime,WPANumber,WPONumber,Company,CEO,Address,Tele)");
                SQLstr .Append ("VALUES('" + OrderReceive_1.ProType + "','" + OrderReceive_1.Intime + "','" + OrderReceive_1.Outtime + "',");
                SQLstr.Append(" '" + OrderReceive_1.WPANum + "','" + OrderReceive_1.WPONum + "','" + Company + "','" + CEO + "','" + Address + "','" + Tele + "');");
                string SQLs = Convert.ToString(SQLstr);
                CPS_Logistics.DataClass.DataClass.DB_Change(SQLs );
                CPS_Logistics.Program.nextform_2 = 1;
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("填写必要信息", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
        }

        private void OrderReceive_2_Load(object sender, EventArgs e)
        {
            CPS_Logistics.Program.nextform_2 = 0;
        }

    }
}
