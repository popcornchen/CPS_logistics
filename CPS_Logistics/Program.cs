using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPS_Logistics
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new OrderReceive_1());
            //if (nextform_1==1)
            //{
               // Application.Run(new OrderReceive_2());
            //}
            //if (nextform_2 ==1)
            //{
                //Application.Run(new MPS());
            //}
              
            //Application.Run(new CPS_Logistics.Scheduling.Scheduling());
            
            //Application.Run(new CPS_Logistics.Scheduling.AutoControl());
            Application.Run(new CPS_Logistics.Scheduling.AutoControl());//记得改回mainform

            Method.CommuAGV .Updatedatabase("off", 2);
            Method.CommuAGV.Updatedatabase("off", 1);
            Scheduling.AutoControl.Updastation_Elapsed(null, null);


            Application.Exit();
        }
             
        public static int nextform_1 =0;
        public static int nextform_2 = 0;
             
    }
}
