using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSOUpdate;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

namespace CPS_Logistics.Scheduling
{
    public partial class AutoControl : Form
    {
        public static int [] jobsequence;   // 任务序列，用来储存从数据库中访问到的任务或用户输入的任务
        public static int [] emergency;     // 紧急任务序列
        public static int [] autopath1;     // agv1获得的任务
        public static int [] autopath2;     // agv2获得的任务
        public static bool done;
        public static Thread appointment;   // 任务分配新线程
        public static Thread agv1go;        // agv1运行新线程，应该被写到AGV类里，慢慢进行改进
        public static Thread agv2go;        // agv2运行新线程，同上
        public static Thread mapupdate2;    // 更新agv1地图线程
        public static Thread mapupdate1;    // 更新agv2地图线程
        public static bool agv1busy;        // AGV1工作状态
        public static bool agv2busy;        // AGV2工作状态
        public static System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();        // 为picturebox控件设置边框形状
        public static Region reg;
        public static List<Control> picturebox=new List<Control> ();            //picturebox控件集合

        public static bool Chargeoccup;             // 判断AGV充电状态


        public static int AGV1now;
        public static int AGV1next;
        public static int AGV2now;
        public static int AGV2next;

        public static bool AGV1Back;
        public static bool AGV2Back;

        public static Thread AvoidCollision;

        public static Thread totalauto;

        

        public static int toolstatus = 2; //这个像一个运行状态监视器
        
        public AutoControl()
        {
            /*-----------------窗体初始化--------------------*/
            InitializeComponent();


            this.Tag = Size.Width + ":" + Size.Height;

            
            foreach(Control ctrl in this.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" + ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel1.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel2.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel3.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel4.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel5.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
            foreach (Control ctrl in this.panel6.Controls)
            {
                ctrl.Tag = ctrl.Size.Width + ":" + ctrl.Size.Height + ":" +  ctrl.Left + ":" + ctrl.Top + ":" + ctrl.Font.Size;
            }
             
        }




        public void AutoControl_Load(object sender, EventArgs e)
        {

            /*-----------------连接两台AGV, 数据库更新状态--------------------*/
            try
            {
                Scheduling.rece(1);
                Scheduling.update(Scheduling.strrecred, 1);               //更新状态
                position1 = Scheduling.PIC_1[6];
                Method.CommuAGV.Updatedatabase("on", 1);
            }

            catch (Exception err)
            {
                MessageBox.Show("Fail to connect the AGV1", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }



            try
            {
                Scheduling.rece(2);
                Scheduling.update(Scheduling.strrecred, 2);               //更新状态
                position2 = Scheduling.PIC_2[6];
                Method.CommuAGV.Updatedatabase("on", 2);
                
            }

            catch(Exception err)
            {
                MessageBox.Show("Fail to connect the AGV2", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }




            /*-----------------充电站状态初始化，可能要上传--------------------*/
            try
            {
                if ((CPS_Logistics.Scheduling.AGVdetails.yesno(Scheduling.PIC_2[3], 2) == 1)
                || (CPS_Logistics.Scheduling.AGVdetails.yesno(Scheduling.PIC_2[3], 3) == 1)
                || (CPS_Logistics.Scheduling.AGVdetails.yesno(Scheduling.PIC_1[3], 2) == 1)
                || (CPS_Logistics.Scheduling.AGVdetails.yesno(Scheduling.PIC_1[3], 3) == 1))
                {
                    Chargeoccup = true;
                }
            }
            
            catch
            {
                MessageBox.Show("Fail to initial the Charge station","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }



            /*-----------------窗体内容初始化--------------------*/
            foreach (Control control in this.panel3.Controls)       //遍历所有picturebox控件
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





            try
            {
                
                picturebox[position1 - 1].BackgroundImage = Properties.Resources.posi1;         //在地图上显示AGV1位置
                picturebox[position1 - 1].Show();
                AGV1now = position1;
            }
            catch(Exception str)
            {
                AGV1now = 0;
                MessageBox.Show("Fail to initial the map for AGV1", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }




            try
            {

                picturebox[position2 - 1].BackgroundImage = Properties.Resources.posi2;         //在地图上显示AGV2位置
                picturebox[position2 - 1].Show();
                AGV2now = position2;
            }
            catch (Exception str)
            {
                AGV2now = 0;
                MessageBox.Show("Fail to initial the map for AGV2", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }



            



            
            textBox1.Text = "Please use  " + "," + "to  seperate ";         //设置提示信息
            textBox1.ForeColor = Color.LightGray;
            textbox1hastext = false;
            textBox2.Text = "Please use  " + "," + "to  seperate ";         //设置提示信息
            textBox2.ForeColor = Color.LightGray;
            textbox2hastext = false;
            label3.Text = "";
            label4.Text = "";
            panel6.Hide();

            /*-----------------所需线程初始化，部分线程启动--------------------*/
            //访问Win窗体控件本质上不是线程安全的
            CheckForIllegalCrossThreadCalls = false;            //让线程的命令可以访问主线程form控件

            AGV1Back = false;
            AGV2Back = false;
            AGV1next = 0;
            AGV2next = 0;

            mapdata();          //更新地图线程开启
            mapupdate2.Start();  //线程使用逻辑：mapupdate2是个实例，先用上面那个函数创建它，再调用
            mapagv1();
            mapupdate1.Start();
            Avoid();

            creatnewthread();
            agv1way();
            agv2way();
            autocontrol();


            jobsequence = null;
            emergency = null;


            UpdateDB.Start(); //这个线程挺重要，隔时上传数据库

            
        }

        /*-----------------任务分配，执行命令，手动模式--------------------*/
        private void button2_Click(object sender, EventArgs e) 
        {
            toolstatus = 3;
            //重启一下地图更新的线程
            if (mapupdate2.IsAlive==true)
            {
                mapupdate2.Abort();
                mapupdate2.Start();
            }
            else
            {
                mapdata(); 
                mapupdate2.Start();
            }
            if (mapupdate1.IsAlive == true)
            {
                mapupdate1.Abort();
                mapupdate1.Start();
            }
            else
            {
                mapagv1();
                mapupdate1.Start();
            }
            button2.Enabled = false;       //禁用Go按钮
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            done = false;                       // 程序判断
            string path = textBox1.Text;
            string supr = textBox2.Text;

            //对输入的序列进行分析，auto模式下可以跳过这里，但序列检查思路可写在sql传入的函数里
            if((path.Length==0)||(supr.Length==0))
            {
                MessageBox.Show("The sequence is empty", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                return;
            }
            string[] array = path.Split(',');
            string[] arrspu = supr.Split(','); 
            if((array.Length==0)||(arrspu.Length==0))
            {
                MessageBox.Show("The sequence is empty", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                return;
            }
            bool sameelement = (array.Distinct<string>().Count() != array.Length);
            if(sameelement==true)
            {
                MessageBox.Show("The same emelents are not allowed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                return;
            }
            if(array.Length!=arrspu.Length)
            {
                MessageBox.Show("Wrong job sequence or emergency station", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                return;
            }

            toolstatus =4;
            //分析合格后进行任务分配
            jobsequence =new int [array.Length]; //任务序列
            emergency=new int [arrspu.Length];  //紧急序列
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == "0")
                {
                    MessageBox.Show("Wrong Input 0", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    return;
                }
                else
                {
                    jobsequence[i] = Convert.ToInt32(array[i]);
                    emergency[i] = Convert.ToInt32(arrspu[i]);
                }
            }
            if ((jobsequence.Max() > 30) || (jobsequence.Min() < 1))
            {
                MessageBox.Show("the stations are not in the map", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                return;
            }
            
            if(jobsequence.Length==1)
            {
                autopath1 = new int[1];
                autopath1[0] = jobsequence[0];
                label3.Text = "";
                label3.Text = autopath1[0].ToString() ;
                toolstatus = 5;
                agv1way();
                agv1go.Start();

            }
            else
            {
                creatnewthread();  //创建一个新线程 进行matlab计算

                //在进行计算前，需要设置计算失败调控机制，将返回参数的长度作为评价参数，若输出任务序列长度与输入任务序列长度不同，则重复计算；
                //若连续计算错误超过5次，抛出异常
                int cycletime = 0;
                do
                {
                    if (cycletime > 5)
                    {
                        MessageBox.Show("Failed to disappach jobs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    cycletime++;
                    //启用新线程进行matlab调用计算
                    appointment.Start();
                    //判断计算结果是否输出成功
                    do
                    {
                        Application.DoEvents(); //实现进程同步，交出CPU控制权
                    }
                    while (done != true);
                    Application.DoEvents();
                }
                while (autopath1.Length + autopath2.Length < jobsequence.Length);  //防止matlab调度回传数据出问题


                //两个path的展示
                if (autopath1[0] == 0)
                {
                    label3.Text = "";
                    label3.Text = "No commission";
                }
                else
                {
                    label3.Text = "";
                    for (int i = 0; i < (autopath1.Length - 1); i++)
                    {
                        label3.Text += autopath1[i] + "-";
                    }
                    label3.Text += autopath1[autopath1.Length - 1];
                }
                if (autopath2[0] == 0)
                {
                    label4.Text = "";
                    label4.Text = "No commission";
                }
                else
                {
                    label4.Text = "";
                    for (int i = 0; i < (autopath2.Length - 1); i++)
                    {
                        label4.Text += autopath2[i] + "-";
                    }
                    label4.Text += autopath2[autopath2.Length - 1];
                }

                jobsequence = null;
                emergency = null;

                toolstatus = 5;
                Avoid();
                AvoidCollision.Start();

                //agv1与agv2 分别在不同线程执行任务 ，应该将AGV1与AGV2写在一个类里，这样保证AGV1与AGV2能分开而不干扰

                agv1way();
                agv2way();
                agv1go.Start();
                agv2go.Start();
            }
            

            do
            {
                Application.DoEvents();
            }
            while (agv1busy != false || agv2busy != false);




            
            button2.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled =true;
            toolstatus = 9;
        }


        /// <summary>
        /// 新线程进行调用matlab计算
        /// </summary>
        /// 
        public void creatnewthread()    //创建新线程函数。虽然不懂为什么
        {
            appointment = new Thread(delegate()
                {
                    done = false;
                    try
                    {
                        Method.Autosequence.GetPath(jobsequence, emergency);        //调用Matlab计算任务分配
                        autopath1 = Method.Autosequence.get1route();                 // agv1的任务序列
                        autopath2 = Method.Autosequence.get2route();                  // agv2的任务序列
                    }
                    catch(Exception str)
                    {
                        MessageBox.Show("Error!!" + str.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    done = true;        //判断是否让 线程终止状态信息
                    appointment.Abort();
                });
        }

        public void agv1way()       //创建AGV1 新线程执行任务
        {
            agv1go = new Thread(delegate()
                {
                    agv1busy = true;        //设置AGV1的工作状态
                    label12.Text = "~Working!~";
                    if (autopath1[0] == 0)
                    {
                        agv1busy = false;
                        agv1go.Abort();     //退出此线程
                    }
                    for (int i = 0; i < autopath1.Length; i++)
                    {
                        AGV1Back = false;
                        Scheduling.rece(1);
                        Scheduling.update(Scheduling.strrecred, 1);

                        Scheduling.cycletime = 0;
                        Scheduling.waycycle(Scheduling.PIC_1[6], autopath1[i], 1, 1000);
                        if (Scheduling.cycletime != 0)
                        {
                            MessageBox.Show("Error! Please Check!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            agv1busy = false;
                            Scheduling.agvautostop(1);
                            agv1go.Abort();
                        }



                        //此时应该!!!!!!!!!!!!暂停程序10s；
                        Scheduling.agvautostop(1);
                        System.Threading.Thread.Sleep(10000);
                    }
                    AGV1Back = true;
                    //回到17站点
                    Scheduling.rece(1);
                    Scheduling.update(Scheduling.strrecred, 1);
                    Scheduling.cycletime = 0;
                    
                    Scheduling.waycycle(Scheduling.PIC_1[6], 17, 1, 1500);
                    if (Scheduling.cycletime != 0)
                    {
                        MessageBox.Show("Error! Please Check!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Scheduling.agvautostop(1);
                        agv1busy = false;
                        agv1go.Abort();
                    }
                    agv1busy = false;
                    label12.Text = "~Completed!~";
                    MessageBox.Show("Jobs for agv1 complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    agv1go.Abort();

                });
        }

        public void agv2way()       // 创建AGV2 新线程执行任务
        {
            agv2go = new Thread(delegate()
                {
                    agv2busy = true;
                    label14.Text = "~Working!~";
                    if (autopath2[0] == 0)
                    {
                        agv2busy = false;
                        agv2go.Abort();
                        return;
                    }
                    for (int i = 0; i < autopath2.Length; i++)
                    {
                        AGV2Back = false;
                        Scheduling.rece(2);
                        Scheduling.update(Scheduling.strrecred, 2);
                        Scheduling.cycletime = 0;
                        Scheduling.waycycle(Scheduling.PIC_2[6], autopath2[i], 2, 1000);
                        if (Scheduling.cycletime != 0)
                        {
                            MessageBox.Show("Error! Please Check!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            agv2busy = false;
                            agv2go.Abort();
                            return;
                        }


                        Scheduling.agvautostop(2);
                        System.Threading.Thread.Sleep(10000);
                        //此时应该!!!!!!!!!!!!暂停程序10s；
                    }

                    AGV2Back = true;
                    //回到18站点
                    Scheduling.rece(2);
                    Scheduling.update(Scheduling.strrecred, 2);
                    Scheduling.cycletime = 0;
                    Scheduling.waycycle(Scheduling.PIC_2[6], 18, 2, 1500);
                    if (Scheduling.cycletime != 0)
                    {
                        MessageBox.Show("Error! Please Check!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        agv2busy = false;
                        agv2go.Abort();
                        return;
                    }
                    label14.Text = "~Completed!~";
                    agv2busy = false;
                    MessageBox.Show("Jobs for agv2 complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                });
        }

        public void mapdata()
        {
            mapupdate2 = new Thread(delegate()
                {
                    newthreadmapupdate2();
                });
        }
        public void mapagv1()
        {
            mapupdate1=new Thread(delegate()
                {
                    newthreadmapupdate1();
                });
        }

        /*-----------------自动模式切换--------------------*/
        private void checkBox1_CheckedChanged(object sender, EventArgs e)           //手动输入任务与自动接收任务切换
        {
            if(checkBox1.Checked==true)
            {
                try
                {
                    appointment.Abort();
                }
                catch
                {

                }
                try
                {
                    Scheduling.agvautostop(1);
                    Scheduling.agvautostop(2);
                    agv1go.Abort();
                }
                catch
                {

                }
                try
                {
                    agv2go.Abort();
                }
                catch
                {

                }

                toolstatus = 1;
                agv1busy = false;
                agv2busy = false;
                panel4.Hide();
                textBox1.Clear();
                textBox2.Clear();
                panel6.Show();
                autocontrol();
                totalauto.Start();

                time = 300;
                counttime();
            }
            else if (checkBox1.Checked==false)
            {

                try
                {
                    appointment.Abort();
                }
                catch
                {

                }
                try
                {
                    Scheduling.agvautostop(1);
                    Scheduling.agvautostop(2);
                    agv1go.Abort();
                }
                catch
                {

                }
                try
                {
                    agv2go.Abort();
                }
                catch
                {

                }

                toolstatus = 2;

                count.Stop();
                autotime.Stop();


                textBox1.Text = "Please use  " + "," + "to  seperate ";
                textBox1.ForeColor = Color.LightGray;
                textbox1hastext = false;
                textBox2.Text = "Please use  " + "," + "to  seperate ";
                textBox2.ForeColor = Color.LightGray;
                textbox2hastext = false;
                panel4.Show();
                panel6.Hide();
            }
        }

        #region TIps
        private bool textbox1hastext = true;
        private bool textbox2hastext = true;
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textbox1hastext==false)
            {
                textBox1.Text = "";
            }
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                textBox1.Text="Please use  "+"," +"to  seperate ";
                textBox1.ForeColor = Color.LightGray;
                textbox1hastext = false;
            }
            else
            {
                textbox1hastext = true;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textbox2hastext == false)
            {
                textBox2.Text = "";
            }
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Please use  " + "," + "to  seperate ";
                textBox2.ForeColor = Color.LightGray;
                textbox2hastext = false;
            }
            else
            {
                textbox2hastext = true;
            }
        }
        #endregion

        #region Exam the Input 
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //ascii码表对照，限制按键范围
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57 ) && e.KeyChar != 13 && e.KeyChar != 8&& e.KeyChar!=44)
            {
                MessageBox.Show("输入参数不合理", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                textBox1.Clear();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 50) && e.KeyChar != 13 && e.KeyChar != 8 && e.KeyChar != 44)
            {
                MessageBox.Show("输入参数不合理", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                textBox2.Clear();
            }
        }
        #endregion
        /*-----------------AGV状态检查与更新（手动）--------------------*/
        private void button1_Click(object sender, EventArgs e)          //检查AGV连接状态
        {
            MessageBox.Show("Ping 192.168.1.91(AGV1) or 192.168.1.92(AGV2)", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("CMD.EXE");               //using system.diagnotics
        }


        private void button3_Click(object sender, EventArgs e)          // 手动更新AGV1状态，这边更新的同时可以把数据上传
        {
            try
            {
                Scheduling.rece(1);
                Scheduling.update(Scheduling.strrecred, 1);
            }
            
            catch
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)          //AGV1工作状态信息显示，工作状态得上传
        {
            Scheduling.agv = 1;
            AGVdetails agvdetails1 = new AGVdetails();
            try
            {
                agvdetails1.Show();
            }
            catch
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)          //AGV1工作状态信息显示
        {
            Scheduling.agv = 2;
            AGVdetails agvdetails2 = new AGVdetails();
            agvdetails2.Show();
        }

        private void button6_Click(object sender, EventArgs e)             // 手动更新AGV1状态
        {
            try
            {
                Scheduling.rece(2);
                Scheduling.update(Scheduling.strrecred, 2);
                picturebox[position2 - 1].Show();
            }
            catch (Exception wrong)
            {
                MessageBox.Show(wrong.ToString());
                return;
            }
        }


        /*
        private void button7_Click(object sender, EventArgs e)          //测试用按钮代码
        {




            
            //timer1.Start();
            string strdir;
            strdir = "800002000" + "2 " + "000063000001023100" + "0400000100";  // 手动控制无效
            byte[] comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            strdir = "800002" + "000" + "2 " + "00" + "00630000" + "010231000405000101";        //上位机分支有效
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            strdir = "800002" + "000" + "2 " + "00" + "00630000" + "010231000406000101";        //上位机分支左偏
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            int b = 5;
            string hex;
            string strdirec;
            hex = b.ToString("X4");
            strdirec = "800002000" + "2" + "0000630000010282004D000002" + (1000 * 10).ToString("X4") + hex;  //目的地数据写入
            byte[] command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, 2);
            strdir = "800002000" + "2 " + "000063000001023100" + "030a000101";           //前往自定义站无动作
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);

            int i = 0;
            do
            {
                Scheduling.rece(2);
                Scheduling.update(Scheduling.strrecred, 2);               //更新状态
                Application.DoEvents();
            }
            while (Scheduling.PIC_2[6] != 4);

            strdir = "800002" + "000" + "2 " + "00" + "00630000" + "010231000406000100";        //上位机分支右偏
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);




            do
            {
                Scheduling.rece(2);

                Scheduling.update(Scheduling.strrecred, 2);               //更新状态
                Application.DoEvents();
            }
            while (Scheduling.PIC_2[6] != 14);
            strdir = "800002" + "000" + "2 " + "00" + "00630000" + "010231000406000101";        //上位机分支右偏
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 2);
            do
            {
                Scheduling.rece(2);
                Scheduling.update(Scheduling.strrecred, 2);               //更新状态
                Application.DoEvents();
            }
            while (Scheduling.PIC_2[6] != 5);


            mapupdate2.Abort();      //终止此线程
            mapupdate1.Abort();
            /*  
            strdirec = "800002000" + "1" + "0000640000010282004D000002" + (0 * 10).ToString("X4") + "0000";  //目的地
            command = Method.CommuAGV.hexstrtobyte(strdirec);
            Method.CommuAGV.Command(command, 1);
            strdir = "800002000" + "1 " + "000064000001023100" + "030a000101";           //前往自定义站无动作
            comma = Method.CommuAGV.hexstrtobyte(strdir);
            Method.CommuAGV.Command(comma, 1);
             


        
        DataSet commission = new DataSet();
        Console.WriteLine("1");
        string SQLstr1 = "SELECT ID AS ID  FROM tb_CommandForAGV20170511 WHERE Needornot ='1';";
        string SQLstr2 = "SELECT ID AS ID  FROM tb_CommandForAGV20170511 WHERE Needornot ='2';";

        DataTable tb_Need1 = new DataTable("tb_Need1");
        DataTable tb_Need2 = new DataTable("tb_Need2");
        commission = CPS_Logistics.DataClass.DataClass.DB_Set(SQLstr1, tb_Need1.TableName);
        tb_Need1 = commission.Tables[0];
        commission = CPS_Logistics.DataClass.DataClass.DB_Set(SQLstr2, tb_Need2.TableName);
        tb_Need2 = commission.Tables[0];
        MessageBox.Show(tb_Need1.Rows.Count.ToString());
        MessageBox.Show(tb_Need2.Rows.Count.ToString());
        Console.WriteLine("3");
        if (tb_Need1 != null && tb_Need2 != null)
        {
            jobsequence = new int[tb_Need1.Rows.Count + tb_Need2.Rows.Count];
            emergency = new int[tb_Need1.Rows.Count + tb_Need2.Rows.Count];
            for (int i = 0; i < tb_Need1.Rows.Count; i++)
            {
                try
                {
                    jobsequence[i] = Convert.ToInt16(tb_Need1.Rows[i]["ID"]);
                    emergency[i] = 0;
                }
                catch
                {
                    continue;
                    MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            for (int i = 0; i < tb_Need2.Rows.Count; i++)
            {
                try
                {
                    jobsequence[i + tb_Need1.Rows.Count] = Convert.ToInt16(tb_Need2.Rows[i]["ID"]);
                    emergency[i + tb_Need1.Rows.Count] = 1;
                }
                catch
                {
                    continue;
                    MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        else if (tb_Need1 == null && tb_Need2 != null)
        {
            jobsequence = new int[tb_Need2.Rows.Count];
            emergency = new int[tb_Need2.Rows.Count];
            for (int i = 0; i < tb_Need2.Rows.Count; i++)
            {
                try
                {
                    jobsequence[i] = Convert.ToInt16(tb_Need2.Rows[i]["ID"]);
                    emergency[i] = 1;
                }
                catch
                {
                    continue;
                    MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        else if (tb_Need1 != null && tb_Need2 == null)
        {
            jobsequence = new int[tb_Need1.Rows.Count];
            emergency = new int[tb_Need1.Rows.Count];
            for (int i = 0; i < tb_Need1.Rows.Count; i++)
            {
                try
                {
                    jobsequence[i] = Convert.ToInt16(tb_Need1.Rows[i]["ID"]);
                    emergency[i] = 0;
                }
                catch
                {
                    continue;
                    MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        else
        {
            label9.Text = "No Commission";
            Console.WriteLine("7");
        }
        MessageBox.Show("Done2!!");
        label9.Text = "";
        string m="";
        for (int i = 0; i < jobsequence.Length - 1; i++)
        {
            m += jobsequence[i] + "-";

        }
        MessageBox.Show(m);
            


        counttime();
    }
    */
        /*-----------------AGV状态检查与更新函数（自动）--------------------*/
        public int position2 = 0;   //作为储存原始PIC_2[6]信息的全局变量，与变化的位置信息进行比较
        public int position1 = 0;
        /// <summary>
        /// winform中的timer控件对AGV地图进行更新，在其他线程中不能执行该控件的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void timer1_Tick(object sender, EventArgs e)   
        {
            Scheduling.rece(2);
            Scheduling.update(Scheduling.strrecred, 2);               //更新状态
            if(position2!=Scheduling.PIC_2[6])
            {
                picturebox[position2-1].Hide();
                position2 = Scheduling.PIC_2[6];
                picturebox[position2 - 1].Show();
            }
            
        }
        

        private void newthreadmapupdate2()
        {
            System.Timers.Timer threadtimer = new System.Timers.Timer();
            threadtimer.Interval = 1000;
            threadtimer.Elapsed += threadtimer_Elapsed;
            threadtimer.Start();
        }
        
        private void newthreadmapupdate1()
        {
            System.Timers.Timer threadtimer1 = new System.Timers.Timer();
            threadtimer1.Interval = 1000;
            threadtimer1.Elapsed += threadtimer1_Elapsed;
            threadtimer1.Start();
        }

        void threadtimer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(position1!=28)
            {
                Scheduling.showposition(1, position1);
            }
            try
            {
                Scheduling.rece(1);
                Scheduling.update(Scheduling.strrecred, 1);               //AGV1的更新状态，上传状态信息，这个在后台，不用管
            }
            catch(Exception str)
            {
                MessageBox.Show("Fail to Update the status of AGV1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((this.position1 != Scheduling.PIC_1[6]))
            {
                picturebox[position1 - 1].Hide();
                Scheduling.showposition(1, position1,Scheduling.PIC_1[6]);
                this.position1 = Scheduling.PIC_1[6];
                picturebox[position1 - 1].BackgroundImage = Properties.Resources.posi1;
                picturebox[position1 - 1].Show();

            }
            if (position1 == 28 && (AGVdetails.yesno(Scheduling.PIC_1[3], 0) == 1 
                || AGVdetails.yesno(Scheduling.PIC_1[3], 2) == 1 
                || AGVdetails.yesno(Scheduling.PIC_1[3], 3) == 1))
            {
                Scheduling.showposition(1, position1,31);
                picturebox[position1 - 1].Hide();
                picturebox[30].BackgroundImage = Properties.Resources.posi1;
                picturebox[30].Show();
            }
        }


        private void threadtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)       //AGV2的更新状态
        {
            if(position2!=28)
            {
                Scheduling.showposition(2, position2);
            }

            try
            {
                Scheduling.rece(2);
                Scheduling.update(Scheduling.strrecred, 2);               
            }
            catch (Exception str)
            {
                MessageBox.Show("Fail to Update the status of AGV2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((this.position2 != Scheduling.PIC_2[6]))
            {
                Scheduling.showposition(2, position2,Scheduling.PIC_2[6]);
                picturebox[position2 - 1].Hide();
                this.position2 = Scheduling.PIC_2[6];
                picturebox[position2 - 1].BackgroundImage = Properties.Resources.posi2;
                picturebox[position2 - 1].Show();
            }
            if(position2==28&&(AGVdetails.yesno(Scheduling.PIC_2[3],0)==1
                ||AGVdetails.yesno(Scheduling.PIC_2[3],2)==1
                ||AGVdetails.yesno(Scheduling.PIC_2[3],3)==1))
            {
                Scheduling.showposition(2, position2,31);
                picturebox[position2 - 1].Hide();
                picturebox[30].BackgroundImage = Properties.Resources.posi2;
                picturebox[30].Show();
            }


            /*
            Scheduling.rece(1);
            Scheduling.update(Scheduling.strrecred, 1);               //更新状态
            Console.WriteLine(Scheduling.PIC_1[6].ToString());
            if (this.position1 != Scheduling.PIC_1[6])
            {
                picturebox[position1 - 1].Hide();
                this.position1 = Scheduling.PIC_1[6];
                picturebox[position1 - 1].BackgroundImage = Properties.Resources.posi1;
                picturebox[position1 - 1].Show();
            }
            */
        }

        

        public void Avoid()
        {
            AvoidCollision = new Thread(delegate()
                {
                    avoidcollision();
                });
        }

        private void avoidcollision()
        {
            System.Timers.Timer agvavoid = new System.Timers.Timer();
            agvavoid.Interval = 3000;
            agvavoid.Elapsed += agvavoid_Elapsed;
            agvavoid.Start();
        }

        void agvavoid_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //AGV1now = Scheduling.AGV1now;
            //AGV1next = Scheduling.AGV1next;
            //AGV2now = Scheduling.AGV2now;
            //AGV2next = Scheduling.AGV2next;
            //Console.WriteLine(AGV1next.ToString() + "+" + AGV1now.ToString() + "+" + AGV2next.ToString() + "+" + AGV2now.ToString());

            if (AGV1next == 0 || AGV2next == 0 || AGV1now == 0 || AGV2now == 0)
            {
                return;
            }

            if((AGV1next==AGV2now)||(AGV1now==AGV2next)||(AGV1next==AGV2next))
            {
                if(AGV1Back==true)
                {
                    string autooff = "800002" + "00" + "01" + "00" + "006300000102B100030000010000";         //在内部专用中
                    Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(autooff), 1);


                    string manualon = "800002" + "00" + "01" + "00" + "00630000" + "010231000400000101";
                    Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(manualon), 1);
                    System.Threading.Thread.Sleep(3000);
                    manualon = "800002" + "00" + "01" + "00" + "00630000" + "010231000400000100";
                    Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(manualon), 1);
                    string strdirec = "80000200" + "01" + "000063000001023100" + "030a000101";            // 前往目标
                    byte[] command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, 1);
                    //Console.WriteLine("1");
                }
                else
                {
                    string autooff = "800002" + "00" + "02" + "00" + "006300000102B100030000010000";         //在内部专用中
                    Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(autooff), 2);

                    string manualon = "800002" + "00" + "02" + "00" + "00630000" + "010231000400000101";
                    Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(manualon), 2);
                    System.Threading.Thread.Sleep(3000);
                    manualon = "800002" + "00" + "02" + "00" + "00630000" + "010231000400000100";
                    Method.CommuAGV.Command(Method.CommuAGV.hexstrtobyte(manualon), 2);

                    string strdirec = "80000200" + "02" + "000063000001023100" + "030a000101";            // 前往目标
                    byte[] command = Method.CommuAGV.hexstrtobyte(strdirec);
                    Method.CommuAGV.Command(command, 2);
                    //Console.WriteLine("2");
                }



            }
        }
      

        public void autocontrol()
        {
            totalauto = new Thread(delegate()
                {
                    autotimer();
                });
        }

        public static System.Timers.Timer autotime ;
        private void autotimer()
        {
            autotime = new System.Timers.Timer();
            autotime.Interval = 5000;         //5s更新一次
            
            autotime.Elapsed += autotime_Elapsed;
            autotime_Elapsed(null,null);
            autotime.Start();

        }



        public static System.Timers.Timer count;
        public int time;

        public void counttime()
        {
            count = new System.Timers.Timer();
            count.Stop();
            count.Interval = 1000;
            count.Elapsed += count_Elapsed;
            label16.Text = "5min";
            count.Start();
        }
        void count_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            time=time-1;
            if(time==0)
            {
                count.Stop();
                time = 300;
                GoOk();
            }
            int min = time / 60;
            int sec = (time % 60);
            label16.Text = min.ToString() + " min " + sec.ToString() + " s ";
            
        }


        DataTable tb_Need1 = new DataTable("tb_Need1");
        DataTable tb_Need2 = new DataTable("tb_Need2");
        //DataTable tb_monitor = new DataTable("tb_monitor");

        string[] route1_split;
        string[] route2_split;
        static int id_1;
        static int id_2;

        /*-----------------读取命令序列：两遍->读出展示+内部check--------------------*/
        void read_seq()
        {
            //提示进入下次工作循环，读取命令序列，这里改成mysql,调试决定是否放进线程

            DataSet commission = new DataSet();
            DataSet GetTag = new DataSet();
            //string SQLstr1 = "SELECT ID AS ID  FROM tb_CommandForAGV20170511 WHERE Needornot ='1'";
            //string SQLstr2 = "SELECT ID AS ID  FROM tb_CommandForAGV20170511 WHERE Needornot ='2'";
            //string startTag = "Select StartPosition from agv_monitor order by id DESC limit 1";

            //GetTag = CPS_Logistics.DataClass.MysqlClass.DB_set(startTag, tb_monitor.TableName);
            //tb_monitor = GetTag.Tables[0];
            //sequence = Convert.ToInt16(tb_monitor.Rows[0]["StartPosition"]);

            string SQLstr1 = "SELECT sub_task from agv_optimize WHERE id_AGV='1' AND checked='0' order by id DESC limit 1";
            string SQLstr2 = "SELECT sub_task from agv_optimize WHERE id_AGV='2' AND checked='0' order by id DESC limit 1";

            commission = CPS_Logistics.DataClass.MysqlClass.DB_set(SQLstr1, tb_Need1.TableName);
            tb_Need1 = commission.Tables[0];  //commision是dataset, tb_need1是datatable
            commission = CPS_Logistics.DataClass.MysqlClass.DB_set(SQLstr2, tb_Need2.TableName);
            tb_Need2 = commission.Tables[0];

            //这里开始是我写的
            string route1;
            string route2;

            label9.Text = "";
            route1 = tb_Need1.Rows[0]["sub_task"].ToString();
            route2 = tb_Need2.Rows[0]["sub_task"].ToString();
            id_1 = Convert.ToInt16(tb_Need1.Rows[0]["id"]); //主键读出来,后面update要用
            id_2 = Convert.ToInt16(tb_Need2.Rows[0]["id"]);
            route1_split = route1.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            route2_split = route2.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (route1_split.Length != 0 && route2_split.Length != 0)
            {
                int[] jobse = new int[route1_split.Length + route2_split.Length];
                for (int i = 0; i < route1_split.Length; i++)
                {
                    try
                    {
                        jobse[i] = Convert.ToInt16(route1_split[i]);
                        label9.Text += jobse[i].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                for (int i = 0; i < route2_split.Length; i++)
                {
                    try
                    {
                        jobse[i + route1_split.Length] = Convert.ToInt16(route2_split[i]);
                        label9.Text += jobse[i + route1_split.Length].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (route1_split.Length == 0 && route2_split.Length != 0)
            {
                int[] jobse = new int[route2_split.Length];
                for (int i = 0; i < route2_split.Length; i++)
                {
                    try
                    {
                        jobse[i] = Convert.ToInt16(route2_split[i]);
                        label9.Text += jobse[i].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (route1_split.Length != 0 && route2_split.Length == 0)
            {
                int[] jobse = new int[route1_split.Length];
                for (int i = 0; i < route1_split.Length; i++)
                {
                    try
                    {
                        jobse[i] = Convert.ToInt16(route1_split[i]);
                        label9.Text += jobse[i].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                label9.Text = "No Commission";
                return;
            }
        }


        void autotime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {


            //toolstatus = 6;

            /*
            if (mapupdate2.IsAlive == true)
            {
                mapupdate2.Abort();
                mapupdate2.Start();
            }
            else
            {
                mapdata(); 
                mapupdate2.Start();
            }
            if (mapupdate1.IsAlive == true)
            {
                mapupdate1.Abort();
                mapupdate1.Start();
            }
            else
            {
                mapagv1();
                mapupdate1.Start();
            }
             */

            read_seq();
            if (route2_split.Length != 0)
            {

                if (agv1busy == true || agv2busy == true)
                {
                    toolstatus = 10;
                    return;
                }
                origin_GoOk();
                GoOk(); //感觉里面的内容跟上面一样，但是多了个emergency系列
            }


            //这是师兄原来写的
            /*label9.Text = "";
            if (tb_Need1.Rows.Count!=0 && tb_Need2.Rows.Count!= 0)
            {
                int [] jobse = new int[tb_Need1.Rows.Count + tb_Need2.Rows.Count];
                for (int i = 0; i < tb_Need1.Rows.Count; i++)
                {
                    try
                    {
                        jobse[i] = Convert.ToInt16(tb_Need1.Rows[i]["ID"]);  //这个数据库里的station序列是处理过的。。吧？
                        label9.Text+=jobse[i].ToString()+"-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                for (int i = 0; i < tb_Need2.Rows.Count; i++)
                {
                    try
                    {
                        jobse[i + tb_Need1.Rows.Count] = Convert.ToInt16(tb_Need2.Rows[i]["ID"]);
                        label9.Text += jobse[i + tb_Need1.Rows.Count].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (tb_Need1.Rows.Count == 0 && tb_Need2.Rows.Count != 0)
            {
                int [] jobse = new int[tb_Need2.Rows.Count];
                for (int i = 0; i < tb_Need2.Rows.Count; i++)
                {
                    try
                    {
                        jobse[i] = Convert.ToInt16(tb_Need2.Rows[i]["ID"]);
                        label9.Text += jobse[i].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (tb_Need1.Rows.Count != 0 && tb_Need2.Rows.Count == 0)
            {
                int[] jobse = new int[tb_Need1.Rows.Count];
                for (int i = 0; i < tb_Need1.Rows.Count; i++)
                {
                    try
                    {
                        jobse[i] = Convert.ToInt16(tb_Need1.Rows[i]["ID"]);
                        label9.Text += jobse[i].ToString() + "-";
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                label9.Text = "No Commission";
                return;
            }
            
            
            if(tb_Need2.Rows.Count!=0)
            {
                
                if (agv1busy == true || agv2busy == true)
                {
                    toolstatus = 10;
                    return;
                }
                GoOk(); //感觉里面的内容跟上面一样，但是多了个emergency系列
            }*/
        }

        void GoOk()
        {
            count.Stop();
            time = 300;
            counttime();


            label3.Text = "";
            label4.Text = "";

            
            //这里是我改的
            if (route1_split.Length != 0 && route2_split.Length != 0)
            {
                jobsequence = new int[route1_split.Length + route2_split.Length];
                emergency = new int[route1_split.Length + route2_split.Length];
                for (int i = 0; i < route1_split.Length; i++)
                {
                    try
                    {
                        jobsequence[i] = Convert.ToInt16(route1_split[i]);
                        emergency[i] = 0;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                for (int i = 0; i < route2_split.Length; i++)
                {
                    try
                    {
                        jobsequence[i + route1_split.Length] = Convert.ToInt16(route2_split[i]);
                        emergency[i + route1_split.Length] = 1;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (route1_split.Length == 0 && route2_split.Length != 0)
            {
                jobsequence = new int[route2_split.Length];
                emergency = new int[route2_split.Length];
                for (int i = 0; i < route2_split.Length; i++)
                {
                    try
                    {
                        jobsequence[i] = Convert.ToInt16(route2_split[i]);
                        emergency[i] = 1;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (route1_split.Length != 0 && route2_split.Length == 0)
            {
                jobsequence = new int[route1_split.Length];
                emergency = new int[route1_split.Length];
                for (int i = 0; i < route1_split.Length; i++)
                {
                    try
                    {
                        jobsequence[i] = Convert.ToInt16(route1_split[i]);
                        emergency[i] = 0;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                label9.Text = "No Commission";
                return;
            }
            
            label9.Text = "";
            try
            {
                for (int i = 0; i < jobsequence.Length - 1; i++)
                {
                    label9.Text += jobsequence[i] + "-";

                }
                label9.Text += jobsequence[jobsequence.Length - 1];
            }
            catch
            {
                label9.Text = "No Commission";
                return;
            }

            //sequence += 1;
            //string SQLstr = "UPDATE agv_monitor set StartPosition=" + sequence + " where line=0"; //这里可以改成monitor的startposition+1
            //DataClass.MysqlClass.DB_Change(SQLstr);


            toolstatus = 4;
            
            label3.Text = "";
            label4.Text = "";

            //这是师兄原来写的
            /*if (tb_Need1.Rows.Count != 0 && tb_Need2.Rows.Count != 0)
            {
                jobsequence = new int[tb_Need1.Rows.Count + tb_Need2.Rows.Count];
                emergency = new int[tb_Need1.Rows.Count + tb_Need2.Rows.Count];
                for (int i = 0; i < tb_Need1.Rows.Count; i++)
                {
                    try
                    {
                        jobsequence[i] = Convert.ToInt16(tb_Need1.Rows[i]["ID"]);
                        emergency[i] = 0;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                for (int i = 0; i < tb_Need2.Rows.Count; i++)
                {
                    try
                    {
                        jobsequence[i + tb_Need1.Rows.Count] = Convert.ToInt16(tb_Need2.Rows[i]["ID"]);
                        emergency[i + tb_Need1.Rows.Count] = 1;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (tb_Need1.Rows.Count == 0 && tb_Need2.Rows.Count != 0)
            {
                jobsequence = new int[tb_Need2.Rows.Count];
                emergency = new int[tb_Need2.Rows.Count];
                for (int i = 0; i < tb_Need2.Rows.Count; i++)
                {
                    try
                    {
                        jobsequence[i] = Convert.ToInt16(tb_Need2.Rows[i]["ID"]);
                        emergency[i] = 1;  //这个emergency..?
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (tb_Need1.Rows.Count != 0 && tb_Need2.Rows.Count == 0)
            {
                jobsequence = new int[tb_Need1.Rows.Count];
                emergency = new int[tb_Need1.Rows.Count];
                for (int i = 0; i < tb_Need1.Rows.Count; i++)
                {
                    try
                    {
                        jobsequence[i] = Convert.ToInt16(tb_Need1.Rows[i]["ID"]);
                        emergency[i] = 0;
                    }
                    catch
                    {
                        continue;
                        MessageBox.Show("Errors happened when converting the data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                label9.Text = "No Commission";
                return;
            }

            label9.Text = "";
            try
            {
                for (int i = 0; i < jobsequence.Length - 1; i++)
                {
                    label9.Text += jobsequence[i] + "-";

                }
                label9.Text += jobsequence[jobsequence.Length - 1];
            }
            catch
            {
                label9.Text = "No Commission";
                return;
            }

            string SQLstr = "UPDATE tb_CommandForAGV20170511 SET Needornot='0' WHERE Needornot !='0';"; //这里可以改成monitor的startposition+1
            DataClass.DataClass.DB_Change(SQLstr);


            toolstatus = 4;

            label3.Text = "";
            label4.Text = "";*/


            /*-----------------调用算法，与手动模式一致--------------------*/
            if (jobsequence.Length == 1)
            {
                autopath1[0] = jobsequence[0];
                label3.Text = "";
                label3.Text = autopath1[0].ToString();
                toolstatus = 5;
                agv1way();
                agv1go.Start();

            }
            else
            {
                creatnewthread();  //创建一个新线程 进行matlab计算



                //在进行计算前，需要设置计算失败调控机制，将返回参数的长度作为评价参数，若输出任务序列长度与输入任务序列长度不同，则重复计算；
                //若连续计算错误超过5次，抛出异常
                int cycletime = 0;
                do
                {
                    if (cycletime > 5)
                    {
                        MessageBox.Show("Failed to disappach jobs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    toolstatus = 4;
                    cycletime++;
                    //启用新线程进行matlab调用计算
                    creatnewthread();  //创建一个新线程 进行matlab计算
                    appointment.Start();

                    System.Threading.Thread.Sleep(5000);
                    //判断计算结果是否输出成功
                    do
                    {

                    }
                    while (done != true);
                }
                while (autopath1.Length + autopath2.Length < jobsequence.Length);  //防止matlab调度回传数据出问题


                if (autopath1[0] == 0)
                {
                    label3.Text = "";
                    label3.Text = "No commission";
                }
                else
                {
                    label3.Text = "";
                    for (int i = 0; i < (autopath1.Length - 1); i++)
                    {
                        label3.Text += autopath1[i] + "-";
                    }
                    label3.Text += autopath1[autopath1.Length - 1];
                }
                if (autopath2[0] == 0)
                {
                    label4.Text = "";
                    label4.Text = "No commission";
                }
                else
                {
                    label4.Text = "";
                    for (int i = 0; i < (autopath2.Length - 1); i++)
                    {
                        label4.Text += autopath2[i] + "-";
                    }
                    label4.Text += autopath2[autopath2.Length - 1];
                }

                jobsequence = null;
                emergency = null;

                Avoid();
                AvoidCollision.Start();

                toolstatus = 5;
                agv1way();
                agv2way();
                agv1go.Start();
                agv2go.Start();
            }
            



            System.Threading.Thread.Sleep(5000);

            


            do
            {
                Application.DoEvents();
            }
            while (agv1busy == true || agv2busy == true);

            
            
            MessageBox.Show("Commission in this period has done!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolstatus = 9;

            //先跑通，再做whiletrue循环，前面把线程abort了，前面elapsed好像本来就是自动循环了？
        }

        void origin_GoOk()
        {
            autopath1[0] = 0;
            autopath2[0] = 0;

            if (route1_split.Length != 0)
            {
                for (int i = 0; i < route1_split.Length; i++) autopath1[i] = Convert.ToInt16(route1_split[i]); 
            }
            if (route2_split.Length != 0)
            {
                for (int i = 0; i < route2_split.Length; i++) autopath2[i] = Convert.ToInt16(route2_split[i]);
            }

            if (autopath1[0] == 0)
            {
                label3.Text = "";
                label3.Text = "No commission";
            }
            else
            {
                label3.Text = "";
                for (int i = 0; i < (autopath1.Length - 1); i++)
                {
                    label3.Text += autopath1[i] + "-";
                }
                label3.Text += autopath1[autopath1.Length - 1];
            }
            if (autopath2[0] == 0)
            {
                label4.Text = "";
                label4.Text = "No commission";
            }
            else
            {
                label4.Text = "";
                for (int i = 0; i < (autopath2.Length - 1); i++)
                {
                    label4.Text += autopath2[i] + "-";
                }
                label4.Text += autopath2[autopath2.Length - 1];
            }

            jobsequence = null;
            emergency = null;

            Avoid();
            AvoidCollision.Start();

            toolstatus = 5;
            agv1way();
            agv2way();
            agv1go.Start();
            agv2go.Start();

            System.Threading.Thread.Sleep(5000);

            do
            {
                Application.DoEvents();
            }
            while (agv1busy == true || agv2busy == true);

            MessageBox.Show("Commission in this period has done!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolstatus = 9;  
        }

        /*-----------------展示窗体相关编辑--------------------*/
        private void AutoControl_Resize(object sender, EventArgs e)
        {

            string[] scale = this.Tag.ToString().Split(new char[] { ':' });

            float newx = (float)(Size.Width / (float)Convert.ToInt16(scale[0]));
            float newy = (float)(Size.Height / (float)Convert.ToInt16(scale[1]));

            foreach(Control ctrl in this.Controls)
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
            foreach (Control ctrl in this.panel3.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel4.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel5.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
            foreach (Control ctrl in this.panel6.Controls)
            {
                string[] tagsplit = ctrl.Tag.ToString().Split(new char[] { ':' });
                ctrl.Left = (int)(newx * Convert.ToSingle(tagsplit[2]));
                ctrl.Top = (int)(newy * Convert.ToSingle(tagsplit[3]));
                ctrl.Width = (int)(newx * Convert.ToSingle(tagsplit[0]));
                ctrl.Height = (int)(newy * Convert.ToSingle(tagsplit[1]));
                ctrl.Font = new Font(ctrl.Font.Name, (Single)(Math.Min(newx, newy) * Convert.ToSingle(tagsplit[4])), ctrl.Font.Style, ctrl.Font.Unit);
            }
             
        }




        static Thread UpdateDB=new Thread (delegate()
            {
                System.Timers.Timer Updastation = new System.Timers.Timer();
                Updastation.Interval = 4000;
                Updastation.Elapsed += Updastation_Elapsed;
                Updastation.Start();
            });

        /*-----------------状态更新模块，后台->数据库--------------------*/
        public static void Updastation_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string SQLstr;
            try
            {
                SQLstr = "Update agv1_status set station_now=" + AGV1now + ", station_next=" + AGV1next;
                //SQLstr = "UPDATE tb_AGVDetail SET AGVStationnow='" + AGV1now + "',AGVStationnext='" + AGV1next + "' WHERE ID = '1';";
                DataClass.MysqlClass.DB_Change(SQLstr);
                SQLstr = "Update agv2_status set station_now=" + AGV2now + ", station_next=" + AGV2next;
                //SQLstr = "UPDATE tb_AGVDetail SET AGVStationnow='" + AGV2now + "',AGVStationnext='" + AGV2next + "' WHERE ID = '2';";
                DataClass.MysqlClass.DB_Change(SQLstr);






            }
            catch(Exception str)
            {
                
            }

            try
            {
                AGVdetails.receive(1);
                AGVdetails.updatestatus(Scheduling.strrecred, 1);
                Updatedetail(1);
                optimized_upload(1);
                AGVdetails.receive(2);
                AGVdetails.updatestatus(Scheduling.strrecred, 2);
                Updatedetail(2);
                optimized_upload(2);
            }
            catch
            {

            }
            
            

            

            
        }


        static void  Updatedetail(int e)  //状态需要累计吗？
        {
            string SQLstr;
            if (AGVdetails.yesno(AGVdetails.PIC[2], 2) == 0)
            {
                SQLstr = "Update agv" + e + "_status set block = '无障碍物'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVBlocked='无障碍物' WHERE ID = '"+e+"';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            else
            {
                SQLstr = "Update agv" + e + "_status set block = '有障碍物'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVBlocked='有障碍物' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            if (AGVdetails.yesno(AGVdetails.PIC[2], 3) == 0)
            {
                SQLstr = "Update agv" + e + "_status set sensor = '传感器正常'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVSensor='传感器正常' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            else
            {
                SQLstr = "Update agv" + e + "_status set sensor = '传感器异常'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVSensor='传感器异常' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }

            if (AGVdetails.yesno(AGVdetails.PIC[2], 5) == 0)
            {
                SQLstr = "Update agv" + e + "_status set direction = '正向行驶'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVDirection='正向行驶' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            else
            {
                SQLstr = "Update agv" + e + "_status set direction = '逆向行驶'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVDirection='逆向行驶' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }

            if (AGVdetails.yesno(AGVdetails.PIC[2], 8) == 0)
            {
                SQLstr = "Update agv" + e + "_status set collsion = '暂无碰撞'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVCollsion='暂无碰撞' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            else
            {
                SQLstr = "Update agv" + e + "_status set collsion = '出现碰撞'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVCollsion='出现碰撞' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            if (AGVdetails.yesno(AGVdetails.PIC[2], 9) == 0)
            {
                SQLstr = "Update agv" + e + "_status set guide = '引导正常'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVGuide='引导正常' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }

            else
            {
                SQLstr = "Update agv" + e + "_status set guide = '引导丢失'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVGuide='引导丢失' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }

            SQLstr = "Upeate agv" + e + "_status set voltage=" + AGVdetails.PIC[4] / 100 + "V";
            //SQLstr = "UPDATE tb_AGVDetail SET AGVBattery='" + AGVdetails.PIC[4] / 100 + "V' WHERE ID = '" + e + "';";
            DataClass.MysqlClass.DB_Change(SQLstr);


            if (AGVdetails.yesno(AGVdetails.PIC[3], 2) == 1)
            {
                SQLstr = "Update agv" + e + "_status set charge = '正在充电'";
                //SQLstr = "UPDATE tb_AGVDetail SET AGVCharge='正在充电' WHERE ID = '" + e + "';";
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            else
            {
                if (AGVdetails.yesno(AGVdetails.PIC[3], 4) == 1)
                {
                    SQLstr = "Update agv" + e + "_status set charge = '需要充电'";
                    //SQLstr = "UPDATE tb_AGVDetail SET AGVCharge='需要充电' WHERE ID = '" + e + "';";
                    DataClass.MysqlClass.DB_Change(SQLstr);
                }

                else
                {
                    if (AGVdetails.yesno(AGVdetails.PIC[3], 3) == 1)
                    {
                        SQLstr = "Update agv" + e + "_status set charge = '充电完成'";
                        //SQLstr = "UPDATE tb_AGVDetail SET AGVCharge='充电完成' WHERE ID = '" + e + "';";
                        DataClass.MysqlClass.DB_Change(SQLstr);
                    }
                    else
                    {
                        SQLstr = "Update agv" + e + "_status set charge = '电量正常'";
                        //SQLstr = "UPDATE tb_AGVDetail SET AGVCharge='电量正常' WHERE ID = '" + e + "';";
                        DataClass.MysqlClass.DB_Change(SQLstr);
                    }
                }
            }
        }

        /*-----------------优化后的数据push函数：path, distance, efficiency--------------------*/
        static void optimized_upload(int e)
        {
            string path1_op = "";
            string path2_op = "";
            string SQLstr;

            if (e == 1)
            {
                for (int i = 0; i < autopath1.Length; i++) path1_op += (autopath1[i] + "-");
                SQLstr = "Update agv" + e + "_optimize set route_optimize=" + path1_op.ToString() + " where sequence=" + sequence;
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
            else
            {
                for (int i = 0; i < autopath2.Length; i++) path2_op += (autopath2[i] + "-");
                SQLstr = "Update agv" + e + "_optimize set route_optimize=" + path2_op.ToString() + " where sequence=" + sequence;
                DataClass.MysqlClass.DB_Change(SQLstr);
            }
        }

        private void button1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
     

       
        

        
    }
}
