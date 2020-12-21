using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSOUpdate;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

namespace CPS_Logistics.Method
{
    class Autosequence
    {
        public static PSOUpdate.ALGO Dyna = new ALGO();
        public static double  swarmsize = 100;
        public static MWArray[] ending = new MWArray[3];
        public static MWArray agv1;
        public static MWArray agv2;
        public static double [,] sequence1;
        public static double [,] sequence2;

        public static void GetPath(int [] jobsequence,int [] emergency)
        {
            try
            {
                sequence1 = null;
                sequence2 = null;
                MWArray[] trans = new MWArray[] { swarmsize, (MWNumericArray)jobsequence, (MWNumericArray)emergency };
                Dyna.PSOUpdate(3, ref ending, trans);
                MWNumericArray x1 = ending[1] as MWNumericArray;
                MWNumericArray x2 = ending[2] as MWNumericArray;
                sequence1 = (double[,])x1.ToArray();
                sequence2 = (double[,])x2.ToArray();
            }
            catch(Exception str)
            {
                MessageBox.Show("Error!!" + str.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        public static int [] get1route()
        {
            if(sequence1.Length!=0)
            {
                int[] agv1se = new int[sequence1.Length];
                for (int i = 0; i < sequence1.Length; i++)
                {
                    agv1se[i] = Convert.ToInt16(sequence1[0, i]);
                }
                return agv1se;
            }
            else
            {
                int[] agv1se = new int[] { 0 };
                return agv1se;
            }
        }
        public static int[] get2route()
        {
            if(sequence2.Length!=0)
            {
                int[] agv2se = new int[sequence2.Length];
                for (int i = 0; i < sequence2.Length; i++)
                {
                    agv2se[i] = Convert.ToInt16(sequence2[0, i]);
                }
                return agv2se;
            }
            else
            {
                int[] agv2se = new int[] { 0 };
                return agv2se;
            }
        }

    }
}
