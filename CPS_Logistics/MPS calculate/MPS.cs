using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CPS_Logistics
{
    public partial class MPS : Form
    {
        public static int WPANum = 0;
        public static int WPONum = 0;
        public static int ProType = 0;
        public MPS()
        {
            InitializeComponent();

            label4.Hide();
            textBox2.Hide();
            label3.Location = new Point(784, 109);
            textBox1.Location = new Point(925, 108);


            this.Tag = Size.Width + ":" + Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MPS_Load(object sender, EventArgs e)
        {
            this.Tag = Size.Width + ":" + Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
        }

        private void GETIT_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] scale = this.Tag.ToString().Split(new char[] { ':' });

            float newx = (float)(this.Size.Width / (float)Convert.ToInt16(scale[0]));
            float newy = (float)(this.Size.Height / (float)Convert.ToInt16(scale[1]));

            if (comboBox2.Text == "WPA & WPO")
            {
                label3.Text = "WPA数量：";
                label3.Location = new Point((int)(653 * newx), (int)(109 * newy));
                textBox1.Location = new Point((int)(793*newx), (int)(108*newy));
                ShowCon();
                ProType = 3;

            }
            if (comboBox2.Text == "WPA")
            {
                label3.Text = "WPA数量：";
                label3.Location = new Point((int)(784*newx), (int)(109*newy));
                textBox1.Location = new Point((int)(925*newx), (int)(108*newy)); 
                HideCon();
                ProType = 1;

            }
            if (comboBox2.Text == "WPO")
            {
                label3.Text = "WPO数量：";
                label3.Location = new Point((int)(784*newx), (int)(109*newy));
                textBox1.Location = new Point((int)(925*newx), (int)(108*newy)); 
                HideCon();
                ProType = 2;

            }

        }
        public void ShowCon()  //showcon方法
        {
            label4.Show();
            label4.Parent = this;
            label4.Text = "WPO数量：";
            textBox2.Show();
            textBox2.Parent = this;
            this.textBox2.KeyPress += new KeyPressEventHandler(textbox2_KeyPress);
        }
        public void HideCon()  // Hide方法
        {
            label4.Hide();
            textBox2.Hide();
        }

        private void textbox2_KeyPress(object sender, KeyPressEventArgs e)  //判断是否是整数输入
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 13 && e.KeyChar != 8)
            {
                MessageBox.Show("请输入非负整数", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                textBox2.Clear();
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 13 && e.KeyChar != 8)
            {
                MessageBox.Show("请输入非负整数", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*if (ProType == 2)
            {
                if (textBox1.Text == "" || textBox1.Text == "0")
                {
                    MessageBox.Show("请输入WPO产品数量", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WPONum = Convert.ToInt32(textBox1.Text);
                    WPANum = 0;
                    DataSet Data_Set_2 = new DataSet();
                    string SQLstr = "SELECT ActualID AS ID,PartName,WPO*" +
                        WPONum + "AS WPO ,WPA*" +
                        WPANum + "+WPO*" +
                        WPONum +
                        "AS Total FROM tb_MaterialCalculate20170513 WHERE WPA*" +
                        WPANum + "+WPO*" +
                        WPONum +
                        "!=0; ";
                    Data_Set_2 = CPS_Logistics.DataClass.DataClass.DB_Set(SQLstr, "tb_Part");
                    dataGridView1.DataSource = Data_Set_2.Tables[0];
                }
            }
            if (ProType == 1)
            {
                if (textBox1.Text == "" || textBox1.Text == "0")
                {
                    MessageBox.Show("请输入WPA产品数量", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WPANum = Convert.ToInt32(textBox1.Text);
                    WPONum = 0;
                    DataSet Data_Set_2 = new DataSet();
                    string SQLstr = "SELECT ActualID AS ID,PartName,WPA*"
                        + WPANum + " AS WPA,WPA*" +
                        WPANum + "+WPO*" +
                        WPONum +
                        "AS Total FROM tb_MaterialCalculate20170513 WHERE WPA*" +
                        WPANum + "+WPO*" +
                        WPONum +
                        "!=0; ";
                    Data_Set_2 = CPS_Logistics.DataClass.DataClass.DB_Set(SQLstr, "tb_Part");
                    dataGridView1.DataSource = Data_Set_2.Tables[0];

                }
            }
            else if (ProType == 3)
            {
                if (textBox1.Text == "")
                {
                    WPANum = 0;
                }
                else
                {
                    WPANum = Convert.ToInt32(textBox1.Text);
                }
                if (textBox2.Text == "")
                {
                    WPONum = 0;
                }
                else
                {
                    WPONum = Convert.ToInt32(textBox2.Text);
                }
                if (WPONum == 0 || WPANum == 0 || textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("请输入产品数量", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WPANum = Convert.ToInt32(textBox1.Text);
                    WPONum = Convert.ToInt32(textBox2.Text);
                    DataSet Data_Set_2 = new DataSet();
                    string SQLstr = "SELECT ActualID AS ID,PartName,WPA*"
                        + WPANum + " AS WPA,WPO*" +
                        WPONum + "AS WPO ,WPA*" +
                        WPANum + "+WPO*" +
                        WPONum +
                        "AS Total FROM tb_MaterialCalculate20170513 WHERE WPA*" +
                        WPANum + "+WPO*" +
                        WPONum +
                        "!=0; ";
                    Data_Set_2 = CPS_Logistics.DataClass.DataClass.DB_Set(SQLstr, "tb_Part");
                    dataGridView1.DataSource = Data_Set_2.Tables[0];

                }
            }
            else if (ProType == 0)
            {
                MessageBox.Show("请选择产品类型", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
        }

        private void MPS_Resize(object sender, EventArgs e)
        {
            string[] scale = this.Tag.ToString().Split(new char[] { ':' });

            float newx = (float)(this.Size.Width / (float)Convert.ToInt16(scale[0]));
            float newy = (float)(this.Size.Height / (float)Convert.ToInt16(scale[1]));


            foreach (Control ctrl in this.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });

                ctrl.Left = (int)(newx * Convert.ToInt16(tagsplit[2]));

                
                ctrl.Top = (int)(newy * Convert.ToInt16(tagsplit[3]));
                
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
            this.dataGridView1.Font = new Font(dataGridView1.Font.Name, (Single)(Math.Min(newx, newy) * 12));
            this.dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font.Name, (Single)(Math.Min(newx, newy) * 12));
            this.dataGridView1.RowHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font.Name, (Single)(Math.Min(newx, newy) * 12));
            int x = dataGridView1.Rows.Count;
            int y = dataGridView1.Columns.Count;
            for (int i = 0; i < x;i++ )
            {
                for (int j = 0; j < y; j++)
                {
                    this.dataGridView1.Rows[i].Cells[j].Style.Font = new Font(dataGridView1.Font.Name, (Single)(Math.Min(newx, newy) * 14.25));
                }
            }
            
        }
    }
}
