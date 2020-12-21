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
   
    public partial class OrderReceive_1 : Form
    {
        #region 定义变量
        ///
        public Label label5 = new Label();              //WPO标签
        public TextBox textbox3 = new TextBox();        //WPO数量输入
        public static int WPANum = 0;
        public static int WPONum = 0;
        public static DateTime Intime;
        public static DateTime Outtime;
        public static int ProType = 0;
        #endregion
        public OrderReceive_1()
        {
            InitializeComponent(); 
        }

        #region 窗体标签
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)   //订单数量输入
        {
            if(comboBox1.Text =="WPA & WPO")
            {
                label2.Text = "WPA数量：";
                label2.Location = new Point(50, 130);
                textBox1.Location = new Point(150, 128);
                ShowCon ();
                ProType = 3;
                
            }
            if (comboBox1 .Text =="WPA")
            {
                label2.Text = "WPA数量：";
                label2.Location = new Point(132, 130);
                textBox1.Location = new Point(232, 128);
                HideCon();
                ProType = 1;
                
            }
            if(comboBox1 .Text =="WPO")
            {
                label2.Text = "WPO数量：";
                label2.Location = new Point(132, 130);
                textBox1.Location = new Point(232, 128);
                HideCon();
                ProType = 2;
                
            }
        }
        
        private void OrderReceive_Load(object sender, EventArgs e)
        {
            CPS_Logistics.Program.nextform_1 = 0;
        }
        public void ShowCon()  //showcon方法
        {
            label5.Show();
            label5 .Parent=this;
            label5.Location = new Point(300, 130);
            label5.Text = "WPO数量：";
            label5.Font = new Font("微软雅黑", 12);
            this.Controls.Add(label5);
            textbox3.Show();
            textbox3.Parent = this;
            textbox3.Location = new Point(400, 128);
            textbox3.Font = new Font("微软雅黑", 12);
            this.Controls.Add(textbox3);
            this.textbox3.KeyPress += new KeyPressEventHandler  (textbox3_KeyPress);
        }
        public void HideCon()  // Hide方法
        {
            label5.Hide();
            textbox3.Hide();
        }
        private void textbox3_KeyPress(object sender,KeyPressEventArgs e)  //判断是否是整数输入
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 13 && e.KeyChar != 8)
            {
                MessageBox.Show("请输入非负整数", "Info", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                e.Handled = true;
                textbox3.Clear();
            }
            
        }
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  //判断输入是否为整数
        {
            if((e.KeyChar <48||e.KeyChar >57)&&e.KeyChar!=13&&e.KeyChar !=8)
            {
                MessageBox.Show("请输入非负整数", "Info", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                e.Handled = true;
                textBox1.Clear();
            }
            
        }
        private void button2_Click(object sender, EventArgs e)  // cancel方法
        {
            this.Close();
            this.Dispose();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)  //判断时间输入是否正确
        {
            if (DateTime.Compare(dateTimePicker1.Value, dateTimePicker2.Value) > 0)
            {
                MessageBox.Show("时间输入错误", "Info", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Intime = dateTimePicker1.Value;
            Outtime = dateTimePicker2.Value;
            if (ProType == 2)
            {
                if (textBox1.Text == "" || textBox1.Text == "0")
                {
                    MessageBox.Show("请输入WPO产品数量", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WPONum = Convert.ToInt32(textBox1.Text);
                    WPANum = 0;
                }
            }
            if(ProType ==1)
            {
                if (textBox1.Text == "" || textBox1.Text == "0")
                {
                    MessageBox.Show("请输入WPA产品数量", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WPANum = Convert.ToInt32(textBox1.Text);
                    WPONum = 0;
                }
            }
            else if (ProType ==3)
            {
                if (textBox1.Text =="")
                {
                    WPANum = 0;
                }
                else
                {
                    WPANum = Convert.ToInt32(textBox1.Text);
                }
                if (textbox3.Text =="")
                {
                    WPONum = 0;
                }
                else
                {
                    WPONum = Convert.ToInt32(textbox3.Text);
                }
                if (WPONum == 0 || WPANum == 0 || textBox1.Text == "" || textbox3.Text == "")
                {
                    MessageBox.Show("请输入产品数量", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WPANum = Convert.ToInt32(textBox1.Text);
                    WPONum = Convert.ToInt32(textbox3.Text);
                }
            }
            else if (ProType ==0)
            {
                MessageBox.Show("请选择产品类型", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (WPANum != 0 || WPONum != 0)
            {
                CPS_Logistics.Program.nextform_1 = 1;
                this.Close();
                this.Dispose();
            }
        }
    }
}
