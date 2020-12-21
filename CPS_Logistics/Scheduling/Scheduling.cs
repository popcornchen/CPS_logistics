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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace CPS_Logistics.Scheduling
{
    public partial class Scheduling : Form
    {
        public static string strRead_1 = "80000200010000630000010182003F000010";       //Read Command for AGV_1
        public static string strRead_2 = "80000200020000630000010182003F000010";        //Read Command for AGV_2
        public static string strWrista_1 = "80000200010000630000010282004D000002";      //Part of station  Command for AGV_1
        public static string strWrista_2 = "80000200020000630000010282004D000002";      //Part of station  Command for AGV_2
        public static string strWristu_1 = "8000020001000063000001023100";                      //Part of status  Command for AGV_1
        public static string strWristu_2 = "8000020002000063000001023100";                      //Part of status  Command for AGV_2
        public static string strmid = "0001";
        public static string strrecred = "";
        public static string strrecwri_1 = "";
        public static string strrecwri_2 = "";
        public static string[] analy = new string[16];
        public static int agv = 0;
        public static int[] PIC_1 = new int[16];
        public static int[] PIC_2 = new int[16];
        public static string path_1;
        public static string path_2;
        public static bool busy_1 = false;
        public static bool busy_2 = false;
        public static bool Chargeoccup;    //充电站是否可用

        public static Thread agvgofor1 = new Thread(delegate() { });
        public static Thread agvgofor2 = new Thread(delegate() { });



        public static System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
        public static Region reg;
        public static List<Control> picturebox = new List<Control>();
        public int position2 = 0;   //作为储存原始PIC_2[6]信息的全局变量，与变化的位置信息进行比较
        public int position1 = 0;

        public static int AGV1now;
        public static int AGV1next;
        public static int AGV2now;
        public static int AGV2next;

        public Scheduling()
        {
            InitializeComponent();
            label10.Text = trackBar1.Value.ToString();
            label12.Text = trackBar2.Value.ToString();
            //AGV1maunal
            AGV1auto_onoff(false);

            //agv1auto
            AGV1manual_onoff(false);

            //AGC2 auto
            AGV2auto_onoff(false);

            //agv2 manual
            AGV2maunal_onoff(false);
            busy_1 = false;
            busy_2 = false;



            this.Tag = Size.Width + ":" + Size.Height;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel1.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel2.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel3.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.groupBox1.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.groupBox2.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.groupBox3.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.groupBox4.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }

        }

        #region onoff for manual or auto
        public void AGV1auto_onoff(bool e) //Function: control the state of the AGV1 auto buttons 
        {
            label7.Enabled = e;
            label6.Enabled = e;
            label10.Enabled = e;
            trackBar1.Enabled = e;
            button14.Enabled = e;
            button15.Enabled = e;
            label8.Enabled = e;
            label9.Enabled = e;
            comboBox1.Enabled = e;
            button3.Enabled = e;
        }

        public void AGV1manual_onoff(bool e)  //Function: control the state of the AGV1 manual buttons 
        {
            button4.Enabled = e;
            button5.Enabled = e;
            button6.Enabled = e;
            button7.Enabled = e;
            button8.Enabled = e;
            checkBox1.Enabled = e;
            checkBox2.Enabled = e;
            checkBox3.Enabled = e;
            checkBox4.Enabled = e;
        }

        public void AGV2auto_onoff(bool e)  //Function: control the state of the AGV2 auto buttons
        {
            label15.Enabled = e;
            label12.Enabled = e;
            label16.Enabled = e;
            label13.Enabled = e;
            label14.Enabled = e;
            trackBar2.Enabled = e;
            button27.Enabled = e;
            button29.Enabled = e;
            comboBox2.Enabled = e;
            button10.Enabled = e;
        }

        public void AGV2maunal_onoff(bool e)  //Function: control the state of the AGV2 manual buttons
        {
            button22.Enabled =e;
            button23.Enabled = e;
            button24.Enabled =e;
            button25.Enabled = e;
            button26.Enabled = e;
            checkBox5.Enabled = e;
            checkBox6.Enabled = e;
            checkBox7.Enabled = e;
            checkBox8.Enabled = e;

        }
        #endregion

        #region judge the connection
        private void button2_Click(object sender, EventArgs e)      // judge the state of connection of AGV1
        {
            MessageBox.Show("Ping 192.168.1.91(AGV1) or 192.168.1.92(AGV2)","Tip",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Process.Start("CMD.EXE");
        }

        #endregion

        #region switch between manual and auto
        private void checkBox1_CheckedChanged(object sender, EventArgs e) //agv1auto
        {
            if(AGV1Auto.Checked==true)
            {
                manualswitch(1, false);
                label4.Text = "ON ";
                AGV1Manual.Checked =false;
                label5.Text = "OFF";
                AGV1manual_onoff(false);
                AGV1auto_onoff(true);
            }
            if(AGV1Auto.Checked==false)
            {
                autoswitch(1, false);
                label4.Text = "OFF";
                AGV1auto_onoff(false);
            }
        }

        private void AGV1Manual_CheckedChanged(object sender, EventArgs e) //agv1 manual
        {
            if(AGV1Manual.Checked==true)
            {
                manualswitch(1, true);
                autoswitch(1, false);
                label5.Text = "ON ";
                AGV1Auto.Checked = false;
                label4.Text = "OFF";
                AGV1manual_onoff(true);
                AGV1auto_onoff(false);
            }
            if(AGV1Manual.Checked==false)
            {
                manualswitch(1, false);
                label5.Text = "OFF";
                AGV1manual_onoff(false);
            }
        }

        private void button9_Click(object sender, EventArgs e) // new form to show more details when the AGV1 is running 
        {
            agv = 1;
            AGVdetails agvdetails1 = new AGVdetails();
            agvdetails1.Show();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)       // synchronize the bar and label to show distance for AGV1
        {
            label10.Text = trackBar1.Value.ToString();
        }

        private void AGV2Auto_CheckedChanged(object sender, EventArgs e)        //  agv2 auto 
        {
            if (AGV2Auto.Checked == true)
            {
                manualswitch(2, false);
                label17.Text = "ON ";
                AGV2Manual.Checked = false;
                label11.Text = "OFF";
                AGV2maunal_onoff(false);

                AGV2auto_onoff(true);
            }
            if (AGV2Auto.Checked == false)
            {
                autoswitch(2, false);
                AGV2auto_onoff(false);
            }
        }

        private void button17_Click(object sender, EventArgs e)    // show the details of AGV2
        {
            agv = 2;
            AGVdetails agvdetails2 = new AGVdetails();
            agvdetails2.Show();
        }

        private void AGV2Manual_CheckedChanged(object sender, EventArgs e)      //agv2 manual 
        {
            if (AGV2Manual.Checked == true)
            {
                manualswitch(2, true);
                autoswitch(2, false);
                label11.Text = "ON ";
                AGV1Auto.Checked = false;
                label17.Text = "OFF";
                AGV2maunal_onoff(true);
                AGV2auto_onoff(false);
            }
            if (AGV2Manual.Checked == false)
            {
                manualswitch(2, false);
                label11.Text = "OFF";
                AGV2maunal_onoff(false);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)  // synchronize the bar and label to show distance for AGV1
        {
            label12.Text = trackBar2.Value.ToString();
        }
        
        public void Scheduling_Load(object sender, EventArgs e)
        {
            cycletime = 0;
            
            try
            {
                rece(1);
                update(strrecred, 1);
            }
            catch(Exception err)
            {
                MessageBox.Show("Fail to connect the AGV1", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
            try
            {
                rece(2);
                update(strrecred, 2);
            }
            catch(Exception err)
            {
                MessageBox.Show("Fail to connect the AGV2", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }


            try
            {
                if ((CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_2[3], 2) == 1)
                || (CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_2[3], 3) == 1)
                || (CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_1[3], 2) == 1)
                || (CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_1[3], 3) == 1))
                {
                    Chargeoccup = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Fail to Initial the Charge information", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
            
            foreach (Control control in this.panel2.Controls)       //遍历所有picturebox控件
            {
                if (control is PictureBox)
                {
                    picturebox.Add(control);
                }
            }
            path.AddEllipse(this.pictureBox1.ClientRectangle);      //设置图形大小
            reg = new System.Drawing.Region(path);                  //设置图形区域

            foreach (PictureBox pic in picturebox)                  //设置picturebox边界为圆
            {
                pic.Hide();
                pic.Region = reg;
            }


            newagv1thread(0,0,0);
            newagv2thread(0,0,0);
        }

        public void autoswitch(int e,bool b)    // switvch the station for agv control 
        {
            string ipnum;
            if (e == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if(b==false)
            {
                string autooff="800002"+"00"+ipnum+"00"+"006300000102B100030000010000";         //在内部专用中
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(autooff), e);
            }
            if(b==true)
            {
                string autooff = "800002" + "00" + ipnum + "00" + "006300000102B100030000010001";           //在内部专用中
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(autooff), e);
            }
        }

        public void manualswitch(int e,bool b)      //switch the station for agv control
        {
            string ipnum;
            if (e == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if (b==true)
            {
                string autooff = "800002" + "00" + ipnum + "00" + "006300000102B100030000010000";         //在内部专用中
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(autooff), e);

                agvstop(e);
                string manualon = "800002" + "00" + ipnum + "00" + "00630000" + "010231000400000101";
                agvstop(e);
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(manualon),e);
            }
            if(b==false)
            {
                agvstop(e);
                string manualoff = "800002" + "00" + ipnum + "00" + "00630000" + "010231000400000100";
                agvstop(e);
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(manualoff), e);

                string autooff = "800002" + "00" + ipnum + "00" + "006300000102B100030000010001";           //在内部专用中
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(autooff), e);
            }
        }
        #endregion

        #region manual 
        private void button6_Click(object sender, EventArgs e)      //agv1 foreward
        {
            agvstop(1);
            foreswitch(1, true);
        }

        private void button8_Click(object sender, EventArgs e)      //agv1 stop
        {
            agvstop(1);
        }

        private void button4_Click(object sender, EventArgs e)      //agv1 left
        {
            agvstop(1);
            leftswitch(1, true);
        }

        private void button7_Click(object sender, EventArgs e)      //agv1 back
        {
            agvstop(1);
            backswitch(1, true);
        }

        private void button5_Click(object sender, EventArgs e)      //agv1 right
        {
            agvstop(1);
            rightswitch(1, true);
        }

        private void foreswitch(int a, bool b)              //switch 0/1 in W4.01
        {
            string ipnum;
            if (a == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if(b==true)
            {
                string forward ="800002" + "00"+ipnum + "00" + "00630000" + "01023100"+ "0401000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
            if(b==false)
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0401000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
        }           //methord fore

        private void backswitch(int a ,bool b)          // switch 0/1 in 4.02
        {
            string ipnum;
            if (a == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if(b==true)
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0402000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
            else
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0402000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
        }           //methord back

        private void leftswitch(int a,bool b)           // switch 0/1 in 4.03
        {
            string ipnum;
            if (a == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if (b == true)
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0403000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
            else
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0403000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
        }               //methord left

        private void rightswitch(int a,bool b)      //switch 0/1 in 4.04
        {
            string ipnum;
            if (a == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if (b == true)
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0404000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
            else
            {
                string forward = "800002" + "00"+ipnum + "00" + "00630000" + "01023100" + "0404000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(forward), a);
            }
        }              // methord right

        private void agvstop(int e)
        {
            foreswitch(e, false);
            backswitch(e, false);
            leftswitch(e, false);
            rightswitch(e, false);
        }                               // methord stop

        public void branchchoose(int a ,int b)
        {
            string ipnum;
            if (a == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            string branchon = "800002" + "00"+ipnum + "00" + "00630000" + "010231000405000101";
            Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(branchon), a);
            if(b==1)
            {
                string left = "800002" + "00"+ipnum + "00" + "00630000" + "010231000406000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(left), a);
            }
            if(b==2)
            {
                string right = "800002" + "00"+ipnum + "00" + "00630000" + "010231000406000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(right), a);
            }
        }           // branch methord 

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                branchchoose(1, 1);
                checkBox2.Checked = false;
            }
            if((checkBox2.Checked ==false)&&(checkBox1.Checked ==false))
            {
                string branchoff = "800002" + "00"+"01" + "00" + "00630000" + "010231000405000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(branchoff), 1);
            }
        }       //agv1 branch left

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                checkBox1.Checked = false;
                branchchoose(1, 2);
            }
            if ((checkBox2.Checked == false) && (checkBox1.Checked == false))
            {
                string branchoff = "800002" + "00"+"01" + "00" + "00630000" + "010231000405000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(branchoff), 1);
            }
        }       //agvq branch right 

        private void button24_Click(object sender, EventArgs e)
        {
            agvstop(2);
            foreswitch(2, true);
        }       //agv2 fore

        private void button26_Click(object sender, EventArgs e)
        {
            agvstop(2);
            leftswitch(2, true);
        }       //agv2 left

        private void button23_Click(object sender, EventArgs e)
        {
            agvstop(2);
            backswitch(2, true);
        }       //agv2 back

        private void button25_Click(object sender, EventArgs e)
        {
            agvstop(2);
            rightswitch(2, true);
        }       //agv2 right

        private void button22_Click(object sender, EventArgs e)
        {
            agvstop(2);
        }       //agv2 stop

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox8.Checked==true)
            {
                branchchoose(2, 1);
                checkBox7.Checked = false;
            }
            if((checkBox7.Checked ==false)&&(checkBox8.Checked ==false))
            {
                string branchoff = "800002" + "000" + 2 + "00" + "00630000" + "010231000405000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(branchoff), 2);
            }
        }       //agv2 branch left

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                branchchoose(2, 2);
                checkBox8.Checked = false;
            }
            if ((checkBox7.Checked == false) && (checkBox8.Checked == false))
            {
                string branchoff = "800002" + "000" + 2 + "00" + "00630000" + "010231000405000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(branchoff), 2);
            }
        }       //agv2 branch right

        private void rollerswitch(int a, bool b,int c)
        {
            if((b==true)&&(c==1))
            {
                string rollerleft = "800002" + "000" + a + "00" + "00630000" + "01023100040A000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(rollerleft), a);
            }
            if((b==true)&&(c==2))
            {
                string rollerright = "800002" + "000" + a + "00" + "00630000" + "01023100040B000101";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(rollerright), a);
            }
            if((b==false)&&(c==1))
            {
                string rollerleft = "800002" + "000" + a + "00" + "00630000" + "01023100040A000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(rollerleft), a);
            }
            if((b==false)&&(c==2))
            {
                string rollerright = "800002" + "000" + a + "00" + "00630000" + "01023100040B000100";
                Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(rollerright), a);
            }
        }       //roller control

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked==true)
            {
                checkBox4.Checked = false;
                rollerswitch(1, true, 1);
            }
            if(checkBox3.Checked==false)
            {
                rollerswitch(1, false, 1);
            }
        }       //agv1roller left

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox3.Checked = false;
                rollerswitch(1, true, 2);
            }
            if (checkBox4.Checked == false)
            {
                rollerswitch(1, false, 2);
            }
        }       //agv1 roller right 
            
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox5.Checked = false;
                rollerswitch(2, true, 1);
            }
            if (checkBox6.Checked == false)
            {
                rollerswitch(2, false, 1);
            }
        }       //agv2 roller left

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox6.Checked = false;
                rollerswitch(2, true, 2);
            }
            if (checkBox5.Checked == false)
            {
                rollerswitch(2, false, 2);
            }
        }      //agv2 roller right

        #endregion

        #region    auto
        /// <summary>
        /// PIC_1[3] --1=1 work station is done  D66.01
        /// PIC_1[3]--2=1 the AGV is charging now  D66.02
        /// PIC_1[3]--3=1 the AGV has complete   D66.03
        /// PIC_1[6]  the station just passed or is passing D69
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)             //AGV1前往自定义站
        {
            button14.Enabled = false;
            // await 怎么用
            if (busy_1 == false)
            {
                rece(1);
                update(strrecred, 1);
                busy_1 = true;
                if(CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_1[3], 2) == 1)     // the agv is charging but not filled 
                {
                    MessageBox.Show("the agv is charging now,please wait", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button14.Enabled = true;
                    return;
                }
                else if (CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_1[3], 3) == 1)       // the agv is charged
                {
                    rece(1);
                    update(strrecred, 1);
                    Chargeoccup = false;
                    newagv1thread(28, Convert.ToInt16(comboBox1.Text), trackBar1.Value);
                    agvgofor1.Start();
                    //waycycle(28, Convert.ToInt16(comboBox1.Text), 1, trackBar1.Value);  //???????????????????????????????
                    //MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else            // the agv is not in the charge station
                {
                    rece(1);
                    update(strrecred, 1);
                    //MessageBox.Show("the route is " + Method.ALG.pathdot[PIC_1[6], Convert.ToInt32(comboBox1.Text)].ToString(), "Info", MessageBoxButtons.OK);
                    //waycycle(PIC_1[6], Convert.ToInt16(comboBox1.Text),1,trackBar1.Value);
                    newagv1thread(PIC_1[6], Convert.ToInt16(comboBox1.Text), trackBar1.Value);
                    agvgofor1.Start();
                    //MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //busy_1 = false;
                //button14.Enabled = true;
                //return;
            }
            else
            {
                MessageBox.Show("Do the former appointment", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button14.Enabled = true;
                return;
            }

        }

        private void button15_Click(object sender, EventArgs e)             //AGV1 前往充电站
        {
            button15.Enabled = false;
            if ((busy_1 == false)&&(Chargeoccup==false))
            {
                busy_1 = true;
                rece(1);
                update(strrecred, 1);
                //MessageBox.Show("the route is " + Method.ALG.pathdot[PIC_1[6], Convert.ToInt32(comboBox1.Text)].ToString(), "Info", MessageBoxButtons.OK);
                //waycycle(PIC_1[6], 28, 1, 1000);        //28 为充电站影子编号
                newagv1thread(PIC_1[6], 28, trackBar1.Value);
                agvgofor1.Start();
            }
            else if (busy_1 == true)
            {
                MessageBox.Show("Do the former appointment", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Chargeoccup == true)
            {
                MessageBox.Show("AGV is charging now", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //button15.Enabled = true;
            //return;
        }

        private void button29_Click(object sender, EventArgs e)             //AGV2 前往自定义站
        {
            button29.Enabled = false;
            if (busy_2 == false)
            {
                busy_2 = true;
                rece(2);
                update(strrecred, 2);
                if (CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_2[3], 2) == 1)
                {
                    MessageBox.Show("the agv is charging now,please wait", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button29.Enabled = true;
                    return;
                }
                else if (CPS_Logistics.Scheduling.AGVdetails.yesno(PIC_2[3], 3) == 1)
                {
                    rece(2);
                    update(strrecred, 2);
                    Chargeoccup = false;
                    //waycycle(28, Convert.ToInt16(comboBox2.Text), 2, trackBar2.Value);  //???????????????????????????????
                    //MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    newagv2thread(28, Convert.ToInt16(comboBox2.Text), trackBar2.Value);
                    agvgofor2.Start();
      
                }
                else
                {
                    rece(2);
                    update(strrecred, 2);
                    //MessageBox.Show("the route is " + Method.ALG.pathdot[PIC_1[6], Convert.ToInt32(comboBox1.Text)].ToString(), "Info", MessageBoxButtons.OK);
                    //waycycle(PIC_2[6], Convert.ToInt16(comboBox2.Text), 2, trackBar2.Value);
                    //MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    newagv2thread(PIC_2[6], Convert.ToInt16(comboBox2.Text), trackBar2.Value);
                    agvgofor2.Start();
                }
                //busy_2 = false;
                //button29.Enabled = true;
                //return;
            }
            else
            {
                MessageBox.Show("Do the former appointment", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button29.Enabled = true;
                return;
            }
        }

        private void button27_Click(object sender, EventArgs e)             //AGV2 前往充电站
        {
            button27.Enabled = false;
            if ((busy_2 == false) && (Chargeoccup == false))
            {
                busy_2 = true;
                Chargeoccup = true;
                rece(2);
                update(strrecred, 2);
                //MessageBox.Show("the route is " + Method.ALG.pathdot[PIC_1[6], Convert.ToInt32(comboBox1.Text)].ToString(), "Info", MessageBoxButtons.OK);
                //waycycle(PIC_2[6], 28, 2, 1000);        //28 为充电站影子编号
                //string strdir = "8000020002000063000001023100030f000101";           //前往充电站
                //byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
                //Method.CommuAGV.Command(comma, 2);
                //MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //busy_2 = false;
                newagv2thread(PIC_2[6], 28, trackBar2.Value);
                agvgofor2.Start();
            }
            else if (busy_2 == true)
            {
                MessageBox.Show("Do the former appointment", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Chargeoccup == true)
            {
                MessageBox.Show("AGV is charging now", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //button27.Enabled = true;
            //return;
        }
        #endregion

        #region  自动执行命令函数 
        /*
        public static void gonext(int a ,int  b,int c, int m,int des)  // 非可行方法
        {
            int direc = Method.ALG.distin[a - 1, b - 1];
            switch(direc)
            {
                case 1:
                    {
                        string strdir;
                        strdir = "800002000"+m+"000064000001023100" + "010231000406000101";
                        byte[] comm = Method.CommuAGV.hexstrtobyte(strdir);
                        Method.CommuAGV.Command(comm, m);
                        break;
                    }
                case 2:
                    {
                        string strdir;
                        strdir = "800002000" + m + "000064000001023100" + "010231000406000100";
                        byte[] comm = Method.CommuAGV.hexstrtobyte(strdir);
                        Method.CommuAGV.Command(comm, m);
                        break;
                    }
                case 3:
                        {
                            break;
                        }
                case 0:
                        {
                            MessageBox.Show("Can't find the path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
            }
            string hex;
            string strdirec;
            hex = c.ToString("X4");
            strdirec = "800002000" + m + "0000640000010282004D000002" + (des * 10).ToString("X4") + hex;
            byte[] command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command,m);
            string strdirect;
            strdirect = "800002000" + m + "000064000001023100" + "01023100030a000101";
            byte[] comma = Method.CommuAGV.hexstrtobyte(strdirect);
            Method.CommuAGV.Command(comma,m);
        }
        */
        
        public static int cycletime = 0;

        /// <summary>
        /// 从当前位置到目标位置的方法
        /// 嵌套函数
        /// 防止死循环
        /// </summary>
        /// <param name="a"></param> 起点
        /// <param name="b"></param> 目的地
        /// <param name="m"></param> AGV编号
        /// <param name="des"></param> 目的地位置
        public static void waycycle(int a,int b,int m,int des)  
        {
            string ipnum;
            if(m==1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            cycletime = cycletime + 1;
            if(cycletime>5)
            {
                MessageBox.Show("AGV"+m.ToString()+" can not recognize the route correctly", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Scheduling.agvautostop(m);
                cycletime = 0;
                return;
            }
            string path = Method.ALG.pathdot[a - 1, b - 1];  //获取路径字符串
            int[] dot = new int[path.Length / 2];       //初始化路径矩阵
            for (int i = 0; i < dot.Length; i++)
            {
                dot[i] = Convert.ToInt16(path.Substring(2 * i, 2));     //分配路径      
            }
            string hex;
            string strdirec;
            byte[] command;
            hex = b.ToString("X4");

            //虽然三段语句可以简化，但是为了调试以及我的C#水平的限制，就这样吧，也挺好看的
            strdirec = "80000200"+ipnum + "000063000001023100" + "0400000100";           // 手动控制无效
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);

            strdirec = "80000200" + ipnum + "0000630000010282004D000002" + (des * 10).ToString("X4") + hex;         //写入终点
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);

            strdirec = "80000200" + ipnum + "00" + "00630000" + "010231000405000101";             //上位机分支有效
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);

            strdirec = "80000200" + ipnum + "000063000001023100" + "030a000101";            // 前往目标
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);

            if (m == 1)
            {
                for (int i = 0; i < dot.Length - 1; i++)
                {
                    
                    //AGV1now = dot[i];
                    //AGV1next = dot[i + 1];


                    strdirec = "80000200" + ipnum + "000063000001023100" + "0400000100";           // 手动控制无效
                    command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, m);


                    strdirec = "80000200" + "2" + "000063000001023100" + "0400000100";           // 手动控制无效
                    command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, 2);





                    int direction = Method.ALG.distin[dot[i]-1, dot[i + 1]-1];
                    string Lastbit = "1";
                    if (direction == 1)
                    {
                        Lastbit = "0";
                    }
                    else if (direction == 2)
                    {
                        Lastbit = "1";
                    }
                    do
                    {
                        if(i>0)
                        {
                            if (PIC_1[6] != dot[i - 1])
                            {
                                waycycle(PIC_1[6], b, 1, des);
                                return;
                            }
                        }
                        else
                        {
                            if (PIC_1[6] != a)
                            {
                                waycycle(PIC_1[6], b, 1, des);
                                return;
                            }
                        }
                        Application.DoEvents();    //出让控制权
                        rece(1);
                        update(strrecred, 1);
                    }
                    while (PIC_1[6] != dot[i]);

                    AutoControl.AGV1now = dot[i];

                    AutoControl.AGV1next = dot[i + 1];

                    strdirec = "8000020001000063000001023100040600010" + Lastbit;        //上位机分支,默认向右
                    command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, 1);
                }

                
                do
                {
                    if (PIC_1[6] != dot[dot.Length-2])
                    {
                        waycycle(PIC_1[6], b, 1, des);
                        return;
                    }
                    Application.DoEvents();
                    rece(1);
                    update(strrecred, 1);
                }
                while (PIC_1[6] != b);
                AutoControl.AGV1now = b;
                AutoControl.AGV1next = 0;
            }

            else if (m == 2)
            {
                for (int i = 0; i < dot.Length - 1; i++)
                {
                    
                    //AGV2now = dot[i];
                    //AGV2next = dot[i + 1];
                    strdirec = "80000200" + ipnum + "000063000001023100" + "0400000100";           // 手动控制无效
                    command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, m);

                    int direction = Method.ALG.distin[dot[i]-1, dot[i + 1]-1];


                    string Lastbit = "1";
                    if (direction == 1)
                    {
                        Lastbit = "0";
                    }
                    else if (direction == 2)
                    {
                        Lastbit = "1";
                    }
                    do
                    {
                        if (i > 0)
                        {
                            if (PIC_2[6] != dot[i - 1])
                            {
                                waycycle(PIC_2[6], b,2, des);
                                return;
                            }
                        }
                        else
                        {
                            if (PIC_2[6] != a)
                            {
                                waycycle(PIC_2[6], b, 2, des);
                                return;
                            }
                        }
                        Application.DoEvents();
                        rece(2);
                        update(strrecred, 2);
                    }
                    while (PIC_2[6] != dot[i]);

                    AutoControl.AGV2now = dot[i];
                    AutoControl.AGV2next = dot[i + 1];

                    strdirec = "8000020002000063000001023100040600010" + Lastbit;        //上位机分支,默认向
                    command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, 2);
                }


                do
                {
                    if (PIC_2[6] != dot[dot.Length - 2])
                    {
                        waycycle(PIC_2[6], b, 2, des);
                        return;
                    }
                    Application.DoEvents();
                    rece(2);
                    update(strrecred, 2);
                }
                while (PIC_2[6] != b);
                AutoControl.AGV2now = b;
                AutoControl.AGV2next = 0;
            }



            cycletime = 0;



        }

        /// <summary>
        /// AGV自动停止，输入混乱的目标以及位置
        /// </summary>
        /// <param name="m"></param> AGV编号
        public static void agvautostop(int m)
        {
            string strdirec;
            byte[] command;
            strdirec = "800002000" + m.ToString() + "000063000001023100" + "0400000100";           // 手动控制无效
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);
            strdirec = "800002000" + m.ToString() + "0000630000010282004D000002" + (0 * 10).ToString("X4") + "0000";  //目的地错误信息
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);
            strdirec = "800002000" +m.ToString() + "000063000001023100" + "030a000101";           //前往自定义站无动作
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, m);
        }


        public void newagv1thread(int a,int b,int des)
        {
            agvgofor1 = new Thread(delegate()
                {
                    if(b==28)
                    {
                        waycycle(a, 28, 1, 1000);        //28 为充电站影子编号
                        AutoControl.AGV1next = 31;
                        string strdir = "8000020001000063000001023100030f000101";           //前往充电站    
                        byte[] command = Method.CommuAGV.hexstrtobyte(strdir);
                        Method.CommuAGV.Command(command, 1);
                        do
                        {
                            rece(1);
                            update(strrecred, 1);
                            Application.DoEvents();
                        }
                        while (AGVdetails.yesno(PIC_1[3], 0) != 1);
                        AutoControl.AGV1now = 31;
                        AutoControl.AGV1next = 0;
                        MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        busy_1 = false;
                        Chargeoccup = true;
                        button15.Enabled = true;
                        agvgofor1.Abort();
                    }
                    else
                    {
                        waycycle(a, b, 1, des);
                        MessageBox.Show("Appointment of AGV1 Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        busy_1 = false;
                        button14.Enabled = true;
                        agvgofor1.Abort();
                    }
                });
        }

        public void newagv2thread(int a,int b,int des)
        {
            agvgofor2 = new Thread(delegate()
                {
                    if(b==28)
                    {
                        waycycle(a, 28, 2, 1000);        //28 为充电站影子编号
                        AutoControl.AGV2next = 31;
                        string strdir = "8000020002000063000001023100030f000101";           //前往充电站    
                        byte[] command = Method.CommuAGV.hexstrtobyte(strdir);
                        Method.CommuAGV.Command(command, 2);
                        do
                        {
                            rece(2);
                            update(strrecred, 2);
                            Application.DoEvents();
                        }
                        while (AGVdetails.yesno(PIC_2[3], 0) != 1);
                        AutoControl.AGV2now = 31;
                        AutoControl.AGV2next = 0;
                        MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        busy_2 = false;
                        Chargeoccup = true;
                        button27.Enabled = true;
                        agvgofor2.Abort();
                    }
                    else
                    {
                        waycycle(a, b, 2, des);
                        MessageBox.Show("Appointment of AGV2 Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        busy_2 = false;
                        button29.Enabled = true;
                        agvgofor2.Abort();
                    }

                });
        }

        /*
        public static void goend(int a,int b,int m,int des)    //非可行方法
        {
            int direc = Method.ALG.distin[a - 1, b - 1];
            switch (direc)
            {
                case 1:
                    {
                        string strdir;
                        strdir = "800002000" + m + "000064000001023100" + "0406000101";
                        byte[] comm = Method.CommuAGV.hexstrtobyte(strdir);
                        Method.CommuAGV.Command(comm, m);
                        break;
                    }
                case 2:
                    {
                        string strdir;
                        strdir = "800002000" + m + "000064000001023100" + "0406000100";
                        byte[] comm = Method.CommuAGV.hexstrtobyte(strdir);
                        Method.CommuAGV.Command(comm, m);
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 0:
                    {
                        MessageBox.Show("Can't find the path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
            }
            string hex;
            string strdirec;
            if (b == 28)
            {
                byte[] comm;
                strdirec = "800002000"+m+"000064000001023100" + "030f000101";
                comm = Method.CommuAGV.hexstrtobyte(strdirec);
                Method.CommuAGV.Command(comm, m);
            }
            else
            {
                hex = b.ToString("X4");
                strdirec = "800002000" + m + "0000640000010282004D000002" + (des * 10).ToString("X4") + hex ;
                byte[] command = Method.CommuAGV.hexstrtobyte(strdirec);
                Method.CommuAGV.Command(command, m);
                if (b == 1)
                {
                    string strdir;
                    strdir = "800002000" + m + "000064000001023100" + "030e000101";
                    byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
                    Method.CommuAGV.Command(comma, m);
                }
                else if (b == 2)
                {
                    string strdir;
                    strdir = "800002000" + m + "000064000001023100" + "030c000101";
                    byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
                    Method.CommuAGV.Command(comma, m);
                }
                else if ((b < 15) && (b > 2))
                {
                    string strdir;
                    strdir = "800002000" + m + "000064000001023100" + "030b000101";
                    byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
                    Method.CommuAGV.Command(comma, m);
                }
                else
                {
                    string strdir;
                    strdir = "800002000" + m + "000064000001023100" + "030a000101";
                    byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
                    Method.CommuAGV.Command(comma, m);
                }
            }
        }
         */

        #endregion

        #region  更新AGV状态信息 now status   
        /// <summary>
        /// 更新AGV状态信息
        /// </summary>
        /// <param name="e"></param>
        public static void rece(int e)   // 发送查询命令并接受状态信息
        {
            string ipnum;
            if (e == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            string strread = "80000200"+ipnum+ "0000630000010182003F000010";
            byte[] comm = Method.CommuAGV.hexstrtobyte(strread);
            byte[] rece = CPS_Logistics.Method.CommuAGV.Command(comm,e);
            Scheduling.strrecred = Method.CommuAGV.bytetohexstr(rece);
            System.Console.WriteLine(Scheduling.strrecred);
        }  
        public static void update(string str,int e)  //对状态信息进行解码
        {
            string ipnum;
            if (e == 1)
            {
                ipnum = "01";
            }
            else
            {
                ipnum = "02";
            }
            if(str=="")
            {
                return;
            }
            Console.WriteLine(str);
            if (str.Substring(20, 4) != "0101")  //判断信息准确性
            {
                MessageBox.Show("Data received was wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (str.Substring(24, 4) != "0000")  //判断信息准确性
            {
                MessageBox.Show("Wrong Received", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(str.Substring(14,2)!=ipnum)
            {
                //Application.DoEvents();
                //rece(e);
                //update(str, e);
                return;
            }
            if(e==1)
            {
                for (int i = 0; i < 16; i++)
                {
                    PIC_1[i] = Convert.ToInt32(str.Substring(28 + 4 * i, 4), 16);
                }
            }
            if(e==2)
            {
                for (int i = 0; i < 16; i++)
                {
                    PIC_2[i] = Convert.ToInt32(str.Substring(28 + 4 * i, 4), 16);
                }
            }
            
        }
        public static int onoff(int e, int bit)  //读取bit信息
        {
            if (Convert.ToBoolean(e) && Convert.ToBoolean(1 << bit))
            {
                int i = 1;
                return i;
            }
            else
            {
                int i = 0;
                return i;
            }
        }


        #endregion // 更新AGV状态信息        

        #region  TImer
        private void timer1_Tick(object sender, EventArgs e)
        {
            //rece(1);
            //update(strrecred, 1);
            //rece(2);
            //update(strrecred, 2);
        }
        #endregion

        
        /*
        private void button10_Click(object sender, EventArgs e)  //测试用按钮  
        {
            string strdir;
            strdir = "80000200"+"02" + "000063000001023100" + "0400000100";  // 手动控制无效
            byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            strdir = "800002" + "00"+"02" + "00" + "00630000" + "010231000405000101";        //上位机分支有效
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            strdir = "800002" + "00"+"02 " + "00" + "00630000" + "010231000406000101";        //上位机分支左偏
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2); 
            int b =5;
            string hex;
            string strdirec;
            hex = b.ToString("X4");
            strdirec = "80000200"+"02" + "0000630000010282004D000002" +  (1000*10 ).ToString("X4")+hex;  //目的地数据写入
            byte[] command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, 2);
            strdir = "80000200"+"02 "+ "000063000001023100" + "030a000101";           //前往自定义站无动作
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            
            int i=0;
            do
            {
                rece(2);
                update(strrecred, 2);               //更新状态
                Application.DoEvents();
            }
            while(PIC_2[6] !=4);
             
            strdir = "800002" + "00"+"02 " + "00" + "00630000" + "010231000406000100";        //上位机分支右偏
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);

            
            
            
            do
            {
                rece(2);
                update(strrecred, 2);               //更新状态
                Application.DoEvents();
            }
            while (PIC_2[6] != 14);
            strdir = "800002" + "00"+"02 " + "00" + "00630000" + "010231000406000101";        //上位机分支右偏
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            do
            {
                rece(2);
                update(strrecred, 2);               //更新状态
                Application.DoEvents();
            }
            while (PIC_2[6] != 5);
             
            /*  
            strdirec = "800002000" + "1" + "0000640000010282004D000002" + (0 * 10).ToString("X4") + "0000";  //目的地
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, 1);
            strdir = "800002000" + "1 " + "000064000001023100" + "030a000101";           //前往自定义站无动作
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 1);
             
            MessageBox.Show(i.ToString());
             
        }
        */

   
        public static void showposition(int e,int hideposi,int showposi)
        {
            if(e==1)
            {
                picturebox[hideposi-1].Hide();
                picturebox[showposi-1].BackgroundImage = Properties.Resources.posi1;
                picturebox[showposi-1].Show();
            }
            else if(e==2)
            {
                picturebox[hideposi-1].Hide();
                picturebox[showposi-1].BackgroundImage = Properties.Resources.posi2;
                picturebox[showposi-1].Show();
            }
        }

        public static void showposition(int e, int showposi)
        {
            if (e == 1)
            {
                picturebox[showposi-1].BackgroundImage = Properties.Resources.posi1;
                picturebox[showposi-1].Show();
            }
            else if (e == 2)
            {
                picturebox[showposi-1].BackgroundImage = Properties.Resources.posi2;
                picturebox[showposi-1].Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            agvautostop(1);
            button14.Enabled = true;
            button15.Enabled = true;
            if(agvgofor1.IsAlive==true)
            {
                Chargeoccup = false;
                agvgofor1.Abort();
            }
            busy_1 = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            agvautostop(2);
            button29.Enabled = true;
            button27.Enabled = true;
            if (agvgofor2.IsAlive == true)
            {
                Chargeoccup = false;
                agvgofor2.Abort();
            }
            busy_2 = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Scheduling_Resize(object sender, EventArgs e)
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
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])*0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel1.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel2.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel3.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.groupBox1.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.groupBox2.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.groupBox3.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.groupBox4.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4]) * 0.9), ctrl.Font.Style, ctrl.Font.Unit);
            }

        }
   
       
    }
}
