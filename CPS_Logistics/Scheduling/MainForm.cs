using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPS_Logistics.Scheduling
{
    public partial class MainForm : Form
    {
        public CPS_Logistics.Scheduling.AutoControl formauto_1 ;
        public CPS_Logistics.Scheduling.Scheduling formsche_1 ;
        public CPS_Logistics.MPS formmps_1;
        public MainForm()
        {
            InitializeComponent();

            this.Tag = Size.Width + ":" + Size.Height;

            foreach (Control ctrl in this.Controls)
            {

                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Location.X + ":" + ctrl.Location.Y + ":" + ctrl.Font.Size;
            }

            foreach (Control ctrl in this.panel2.Controls)
            {

                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Location.X + ":" + ctrl.Location.Y + ":" + ctrl.Font.Size;
            }
            foreach (ToolStripStatusLabel toollabel in this.statusStrip1.Items)
            {
                toollabel.Tag = toollabel.Size.Width + ":" + toollabel.Size.Height + ":" + ":" + toollabel.Font.Size;
            }

            DataClass.MysqlClass.DB_con();
            
            
             

            
             
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            formauto_1 = new AutoControl();
            formsche_1 = new Scheduling();
            formmps_1 = new MPS();
            formauto_1.TopLevel = false;
            formauto_1.Dock = DockStyle.Fill;
            formauto_1.Parent = this.panel3;
            formauto_1.Show();
            formsche_1.Show();
            formsche_1.Hide();
            toolStripStatusLabel1.Text = "NOW Auto-Control Mode";
            toolStripStatusLabel1.BackColor = Color.Gray;
            button1.BackgroundImage = Properties.Resources.A_1;
            button2.BackgroundImage = Properties.Resources.B_2;
            button3.BackgroundImage = Properties.Resources.C_2;
            button4.BackgroundImage = Properties.Resources.D_2;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Enabled = false;

            toolupdate_Elapsed(null,null);
            toolUpdate();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.A_1;
            button2.BackgroundImage = Properties.Resources.B_2;
            button3.BackgroundImage = Properties.Resources.C_2;
            button4.BackgroundImage = Properties.Resources.D_2;

            toolStripStatusLabel1.Text = "NOW Auto-Control Mode";
            toolStripStatusLabel1.BackColor = Color.Gray;
            AutoControl.toolstatus = 2;
            
            button1.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;

            //formauto_1 = new AutoControl();
            formmps_1.Dispose();
            formsche_1.Hide();
            //formsche_1.Close();

            //formsche_1.Dispose();
            formauto_1.TopLevel = false;
            formauto_1.Dock = DockStyle.Fill;
            formauto_1.Parent = this.panel3;
            formauto_1.Show();

            //终止scheduling的两个可能正在执行的线程

            if(Scheduling.agvgofor1.IsAlive==true)
            {
                
                Scheduling.agvautostop(1);
                Scheduling.agvautostop(2);
                Scheduling.agvgofor1.Abort();
            }
            if(Scheduling.agvgofor2.IsAlive==true)
            {
                Scheduling.agvautostop(1);
                Scheduling.agvautostop(2);
                Scheduling.agvgofor2.Abort();

            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.A_2;
            button2.BackgroundImage = Properties.Resources.B_1;
            button3.BackgroundImage = Properties.Resources.C_2;
            button4.BackgroundImage = Properties.Resources.D_2;

            toolStripStatusLabel1.Text = "NOW Manual-Control Mode";
            toolStripStatusLabel1.BackColor = Color.Gray;
            AutoControl.toolstatus = 0;
            
            button1.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = true;
            button3.Enabled = true;

            formmps_1.Dispose();
            formauto_1.Hide();
            //formauto_1.Close();
            //formauto_1.Dispose();
            //formsche_1 = new Scheduling();
            formsche_1.TopLevel = false;
            formsche_1.Dock = DockStyle.Fill;
            formsche_1.Parent = this.panel3;
            formsche_1.Show();

            if(AutoControl.appointment.IsAlive==true)
            {
                AutoControl.appointment.Abort();
            }
            if(AutoControl.agv1go.IsAlive==true)
            {
                Scheduling.agvautostop(1);
                Scheduling.agvautostop(2);
                AutoControl.agv1go.Abort();
            }
            if (AutoControl.agv2go.IsAlive == true)
            {
                Scheduling.agvautostop(1);
                Scheduling.agvautostop(2);
                AutoControl.agv2go.Abort();
            }
            try
            {
                AutoControl.count.Stop();
            }
            catch
            {
            }
            try
            {
                AutoControl.autotime.Stop();
            }
            catch
            {
            }
            if(AutoControl.totalauto.IsAlive==true)
            {
                AutoControl.totalauto.Abort();
            }
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.A_2;
            button3.BackgroundImage = Properties.Resources.C_1;
            button2.BackgroundImage = Properties.Resources.B_2;
            button4.BackgroundImage = Properties.Resources.D_2;

            toolStripStatusLabel1.Text = "NOW Material Calcualte Mode";
            toolStripStatusLabel1.BackColor = Color.Gray;
            AutoControl.toolstatus = 0;
            
            button1.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = true;
            button2.Enabled = true;

            formauto_1.Hide();
            formsche_1.Hide();
            formmps_1 = new MPS();
            formmps_1.Show();
            formmps_1.TopLevel = false;
            formmps_1.Dock = DockStyle.Fill;
            formmps_1.Parent = this.panel3;
            formmps_1.Show();
            //formsche_1.Close();
            //formsche_1.Dispose();
            //formauto_1.Close();
            //formauto_1.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.A_2;
            button4.BackgroundImage = Properties.Resources.D_1;
            button3.BackgroundImage = Properties.Resources.C_2;
            button2.BackgroundImage = Properties.Resources.B_2;
            
            button1.Enabled = true;
            button4.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;

            formauto_1.Hide();
            formsche_1.Hide();
            formmps_1.Dispose();
            //formauto_1.Close();
            //formauto_1.Dispose();
            //formsche_1.Close();
            //formsche_1.Dispose();
        }


        public System.Timers.Timer toolupdate;
        public void toolUpdate()
        {
            toolupdate = new System.Timers.Timer();
            toolupdate.Interval = 1000;
            toolupdate.Elapsed +=toolupdate_Elapsed;
            toolupdate.Start();
        }

        private void toolupdate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (AutoControl.toolstatus)
            {
                case 0:
                    toolStripStatusLabel2.Text = "";
                    break;
                case 1:
                    toolStripStatusLabel2.Text = "自动获取任务模式~";
                    break;
                case 2:
                    toolStripStatusLabel2.Text = "手动输入任务模式~";
                    break;
                case 3:
                    toolStripStatusLabel2.Text = "获取任务中~";
                    break;
                case 4:
                    toolStripStatusLabel2.Text = "分配任务中~";
                    break;
                case 5:
                    toolStripStatusLabel2.Text = "执行任务中~";
                    break;
                case 6:
                    toolStripStatusLabel2.Text = "数据读取中~";
                    break;
                case 9:
                    toolStripStatusLabel2.Text = "任务完成！！";
                    break;
                case 10:
                    toolStripStatusLabel2.Text = "等待上一个任务完成~";
                    break;

            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            
            string  [] scale = this.Tag.ToString().Split(new char[]{':'});
            
            float newx = (float)(this.Size.Width / (float)Convert.ToInt16(scale[0]));
            float newy = (float)(this.Size.Height /(float) Convert.ToInt16(scale[1]));

            
            foreach (Control ctrl in this.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                
                ctrl.Left = (int)(newx * Convert.ToInt16(tagsplit[2]));

                
                ctrl.Top = (int)(newy * Convert.ToInt16(tagsplit[3]));
                
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx,newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
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
            foreach(ToolStripStatusLabel toollabel in this.statusStrip1.Items)
            {
                string[] tagsplit = toollabel.Tag.ToString().Split(new char[] { ':' });
                toollabel.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                toollabel.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                //toollabel.Font = new Font(toollabel.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[2])), toollabel.Font.Style, toollabel.Font.Unit);
            }
             
        }
  
    }
}
