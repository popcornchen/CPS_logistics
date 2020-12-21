using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CPS_Logistics.Scheduling
{
    public partial class AGVdetails : Form
    {
        public static int[] PIC = new int[15];
        public AGVdetails()
        {
            InitializeComponent();
            timer1.Start();   // update the status every 15s
        }
        private void AGVdetails_Load(object sender, EventArgs e)
        {
            receive(Scheduling.agv); // initial the information
            updatestatus(Scheduling.strrecred, Scheduling.agv);
            updatesysteminfo();
            updatevehicleinfo();


            this.Tag = Size.Width + ":" + Size.Height;


            foreach (Control ctrl in this.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel1.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel2.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
        }
        private void button2_Click(object sender, EventArgs e)  //Close the form
        {
            CPS_Logistics.Scheduling.Scheduling.agv = 0;
            this.Close();
            this.Dispose();
        }
        public static void receive(int e)  // search for the state of AGVe
        {
            string strread = "800002000" + e + "0000640000010182003F000010";
            byte[] comm = Method.CommuAGV.hexstrtobyte(strread);
            byte[] rece = CPS_Logistics.Method.CommuAGV.Command(comm, e);
            Scheduling.strrecred = Method.CommuAGV.bytetohexstr(rece);
        }
        public static void updatestatus(string strrevceive,int e)//更新状态
        {
            if (strrevceive==""||strrevceive.Substring(15,1)!=e.ToString())
            {
                return;
            }
            if(strrevceive .Substring(20,4)!="0101")
            {
                MessageBox.Show("Data received was wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (strrevceive.Substring(24,4)!="0000")
            {
                MessageBox.Show("Wrong Received", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for(int i=0;i<15;i++)
            {
                PIC[i] = Convert.ToInt32(strrevceive.Substring(28 + 4 * i, 4), 16);
            }
        }  //split the string back
        private void updatesysteminfo()
        {
            if(yesno(PIC[2],0)==0)
            {
                this.label3.Text = "系统正常";
            }
            else
            {
                this.label3.Text="系统异常";
                this.label3.BackColor = Color.Red;
            }
            if(yesno(PIC[2],1)==0)
            {
                this.label5.Text = "急停正常";
            }
            else
            {
                this.label5.Text = "急停故障";
                this.label5.BackColor = Color.Red;
            }
            if(yesno(PIC[2],2)==0)
            {
                this.label4.Text = "无障碍物";
            }
            else
            {
                this.label4.Text = "有障碍物";
                this.label4.BackColor = Color.Red;
            }
            if(yesno(PIC[2],3)==0)
            {
                this.label6.Text = "传感器正常";
            }
            else
            {
                this.label6.Text = "传感器异常";
                this.label6.BackColor = Color.Red;
            }
            if (yesno(PIC[2], 4) == 0)
                this.label7.Text = "遥控器正常";
            else
            {
                this.label7.Text = "遥控器异常";
                this.label7.BackColor = Color.Red;
            }
            if (yesno(PIC[2], 5) == 0)
                this.label10.Text = "正向行驶";
            else
            {
                this.label10.Text = "逆向行驶";
                this.label10.BackColor = Color.Red;
            }
            if (yesno(PIC[2], 6) == 0)
                this.label12.Text = "驱动器正常";
            else
            {
                this.label12.Text = "驱动器异常";
                this.label12.BackColor = Color.Red;
            }
            if(yesno(PIC[2],7)==0)
                this.label10.Text = "定位正常";
            else
            {
                this.label10.Text = "定位失败";
                this.label10.BackColor = Color.Red;
            }
            if (yesno(PIC[2], 8) == 0)
                this.label9.Text = "暂无碰撞";
            else
            {
                this.label9.Text = "出现碰撞";
                this.label9.BackColor = Color.Red;
            }
            if (yesno(PIC[2], 9) == 0)
                this.label11.Text = "引导带正常";
            else
            {
                label11.Text= "引导带丢失";
                label11.BackColor = Color.Red;
            }
        }       //encode system information
        private void updatevehicleinfo()
        {
            this.label17.Text = PIC[4] / 100 + "V";
            if (yesno(PIC[3], 6) == 0)
                label18.Text = "正常";
            else
            {
                label18.Text = "不足";
                label18.BackColor = Color.Red;
            }
            if (yesno(PIC[3], 2) == 1)
                label19.Text = "正在充电";
            else
            {
                if (yesno(PIC[3], 4) == 1)
                    label19.Text = "需要充电";
                else 
                {
                    if (yesno(PIC[3], 3) == 1)
                        label19.Text = "充电完成";
                    else
                    {
                        label19.Text = "电量正常";
                    }
                }
            }
            if (yesno(PIC[3], 3) == 1)
                label19.Text = "充电完成";
            if (yesno(PIC[3], 13) == 1)
            {
                label22.Text = "静止";
                label22.BackColor = Color.Red;
            }
            else
                label22.Text = "正常";
            if (yesno(PIC[3], 14) == 1)
            {
                label21.Text = "遮挡";
                label21.BackColor = Color.Red;
            }
            else
                label21.Text = "正常";
            if (yesno(PIC[3], 15) == 1)
            {
                label20.Text = "遮挡";
                label20.BackColor = Color.Red;
            }
            else
                label20.Text = "正常";
        }       //encode vehicle info
        public static int yesno (int e, int bit)
        {
            if((e & 1<<bit)>0)
            {
                int i = 1;
                return i;
            }
            else
            {
                int i = 0;
                return i;
            }
        }   //comprise
        private void button1_Click(object sender, EventArgs e)
        {
            receive(Scheduling.agv);
            updatestatus(Scheduling.strrecred, Scheduling.agv);
            updatesysteminfo();
            updatevehicleinfo();
        }       //Update button
        private void timer1_Tick(object sender, EventArgs e)
        {
            receive(Scheduling.agv);
            updatestatus(Scheduling.strrecred, Scheduling.agv);
            updatesysteminfo();
            updatevehicleinfo();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void AGVdetails_Resize(object sender, EventArgs e)
        {
            string[] scale = this.Tag.ToString().Split(new char[] { ':' });

            float newx = (float)(Size.Width / (float)Convert.ToInt16(scale[0]));
            float newy = (float)(Size.Height / (float)Convert.ToInt16(scale[1]));

            foreach (Control ctrl in this.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel1.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel2.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
        }       // Update the status every 15second since the form is shown
    }
}
