using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace CPS_Logistics.Scheduling
{
    public partial class Sequenece : Form
    {
        public Sequenece()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)          ///to get the distance information 
        {
            int[,] m=Method.algrithom.floyd(Method.algrithom.b);
            for(int i=0;i<30;i++)
            {
                for (int j=0;j<30;j++)
                {
                    richTextBox1.Text += m[i,j].ToString()+", ";
                }
                richTextBox1.Text += "},{";
            }
        }

        

        private void button2_Click(object sender, EventArgs e)          ///get the path information
        {
            Method.algrithom.Path();
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    richTextBox2.Text += "\""+Method.algrithom.d[i, j] +"\""+ " ,";
                }
                richTextBox2.Text += " } ,{";
            }

            /*
             * Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlbook = excel.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Worksheet xisheet =xlbook.Worksheets[1];
            for(int i=0;i<27;i++)
            {
                for(int j=0;j<27;j++)
                {
                    excel.Cells[i + 1, j + 1] = Method.algrithom.d[i, j].ToString();
                }
            }
            xlbook.Saved = true;
            xlbook.SaveCopyAs(@"C:\Users\Robin\Desktop\p.xls");
            excel.Quit();
            excel = null;
             */
        }

        private void Sequenece_Load(object sender, EventArgs e)
        {

        }
    }
}
