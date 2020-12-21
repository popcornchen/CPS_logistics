using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Threading;



namespace CPS_Logistics.Method
{
    class CommuAGV
    {
        public static IPAddress IP_1 =IPAddress.Parse ("192.168.1.91");                //AGV1's IPAddress
        public static IPAddress IP_2 = IPAddress.Parse("192.168.1.92");                //AGV2's IPAddress
        public static IPAddress HostIP = IPAddress.Parse("192.168.1.99");        //local IPAddress
        public static int PortID = 9600;                                                                    // port of AGV
        Socket Commu = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);        // Creat a new socket 
        static IPEndPoint AGV_1=new IPEndPoint(IP_1 ,PortID);                                   //bind AGV1
        static IPEndPoint AGV_2 = new IPEndPoint(IP_2, PortID);                                 //bind AGV2
        IPEndPoint Host=new IPEndPoint(HostIP,8080);                                               // bind local PC




        public static void Connect(int e)   // analysis the status
        {
            switch (e)
            {
                case 1:
                    Socket Commu_1 = new Socket(IP_1.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    try
                    {
                        Commu_1.Connect(AGV_1);
                        MessageBox.Show("Connect to the AGV" + e + " succesfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Commu_1.Shutdown(SocketShutdown.Both);
                        Commu_1.Close();
                    }
                    catch(Exception wrong)
                    {
                        MessageBox.Show("Failed to connect the AGV"+"    "+wrong.Message,"Info",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case 2:
                     Socket Commu_2 = new Socket(IP_2.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    try
                    {
                        Commu_2.Connect(AGV_2);
                        MessageBox.Show("Connect to the AGV" + e + " succesfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Commu_2.Shutdown(SocketShutdown.Both);
                        Commu_2.Close();
                    }
                    catch(Exception wrong)
                    {
                        MessageBox.Show("Failed to connect the AGV"+"    "+wrong.Message,"Info",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        return;
                    }
                    break;
            }
        }
        public static byte[] Command(byte[] Sender,int e)       // send and receive command
        {
            switch (e)
            {
                case 1:
                    Socket Commu_1 = new Socket(IP_1.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    try
                    {
                        Commu_1.Connect(AGV_1);     //尝试连接
                    }
                    catch(Exception wrong)
                    {
                        MessageBox.Show("Failed to connect the AGV1"+wrong.Message,"Info",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        return null;
                    }


                    Commu_1.SendTimeout=6000;  //设置发送命令的最长时间为2000ms；超过该时间则报错。
                    byte[] back=new byte[46];
                    try
                    {
                        Commu_1.SendTo(Sender, AGV_1);      //发送命令
                    }
                    catch (System.Threading.ThreadAbortException wrong)
                    {
                        //MessageBox.Show("Failed to receive the message from AGV2" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        //return null;
                    }
                    catch(Exception wrong)
                    {
                        MessageBox.Show("Failed to send the Command1" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return null;
                    }
                    EndPoint remote=new IPEndPoint(IP_1,PortID);


                    Commu_1.ReceiveTimeout = 12000;          //接受回复的时间间隔为2000ms；
                    
                    try
                    {
                        Commu_1.ReceiveFrom(back, ref remote);      //接受回复；
                    }
                    catch (System.Threading.ThreadAbortException wrong)
                    {
                        //MessageBox.Show("Failed to receive the message from AGV1" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        //return null;
                    }
                    catch (Exception wrong)
                    {
                        Updatedatabase("off", 1);
                        //MessageBox.Show("Failed to receive the message from AGV1" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return null;
                    }
                    
                    Commu_1.Shutdown(SocketShutdown.Both);      //关闭连接
                    Commu_1.Close();
                    return back;
                    break;

                case 2:
                    Socket Commu_2 = new Socket(IP_2.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    try
                    {
                        Commu_2.Connect(AGV_2);     //尝试连接AGV2
                    }
                    catch(Exception wrong)
                    {
                        MessageBox.Show("Failed to connect the AGV2"+wrong.Message,"Info",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        return null;
                    }
                    Commu_2.SendTimeout = 6000;         //设置发送命令超时2000ms；
                    byte[] back_2=new byte[46];
                    try
                    {
                        Commu_2.SendTo(Sender, AGV_2);
                    }
                    catch (System.Threading.ThreadAbortException wrong)
                    {
                        //MessageBox.Show("Failed to receive the message from AGV2" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        //return null;
                    }
                    catch(Exception wrong)
                    {
                        MessageBox.Show("Failed to Send the message to AGV2" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return null;
                    }


                    EndPoint remote_2=new IPEndPoint(IP_2,9600);
                    Commu_2.ReceiveTimeout = 12000;            //设置接受超时2000s；
                    
                    try
                    {
                        Commu_2.ReceiveFrom(back_2, ref remote_2);
                    }
                    catch (System.Threading.ThreadAbortException wrong)
                    {
                        //MessageBox.Show("Failed to receive the message from AGV2" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        //return null;
                    }
                    catch (Exception wrong)
                    {
                        Updatedatabase("off", 2);
                        //MessageBox.Show("Failed to receive the message from AGV2" + wrong.Message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return null;
                    }
                    
                    return back_2;
                    Commu_2.Shutdown(SocketShutdown.Both);
                    Commu_2.Close();
                    break;
                default:
                    byte[] error = null;
                    return error;
                
            }
        }
        public static byte[] hexstrtobyte(string hexString)         //16string to byte[]
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length%2)!=0)
            {
                hexString += " ";
            }
            byte[] returnbytes = new byte[hexString.Length / 2];
            for (int i=0;i<returnbytes.Length;i++)
            {
                returnbytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(),16);
            }
            return returnbytes;
        }
        public static string bytetohexstr(byte[] Byte)              //byte to 16string
        {
            string returnstr = "";
            if (Byte!=null)
            {
                for (int i=0;i<Byte.Length;i++)
                {
                    returnstr += Byte[i].ToString("X2");
                }
            }
            return returnstr;
        }

        static Thread updata ;
        public static void Updatedatabase(string status,int e)
        {
            updata = new Thread(delegate()
                {
                    try
                    {
                        string SQLstr = "UPDATE agv" + e +"_status set on/off=" + status;
                        DataClass.MysqlClass.DB_Change(SQLstr);
                    }
                    catch
                    {
                        
                    }
                });
            updata.Start();
            Thread.Sleep(3000);
            updata.Abort();
        }
    }
}
