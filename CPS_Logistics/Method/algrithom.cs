﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPS_Logistics.Method
{
    class algrithom
    {
        public static int[,] b = new int[30, 30] {
    { 0	,130	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000},
    {1000000,	0,	200	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,130,	1000000,	1000000,	1000000	,1000000,	1000000},
    {1310,	1000000,	0,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000},
    {1000000,	1000000	,1000000	,0	,110	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000},
    {1000000	,1000000,	1000000,	1000000,	0	,110,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000},
    {1000000	,1000000,	1000000,	1000000,	1000000,	0	,110	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	0	,110,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	0	,110	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	0,	695,	1000000,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	0	,110,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	320,	1000000	,1000000,	1000000	,1000000,	1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	0	,110	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	1000000	,320	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000},
    {1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	0	,110	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	320	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000},
    {1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	0	,110,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000,	1000000,	1000000},
    {1000000,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	0	,110	,1000000	,1000000,	1000000	,1000000,	380	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000},
    {1000000	,1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,0	,170	,1000000,	1000000,	320	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000},
    {1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	0,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,120	,1000000,	1000000,	130	,1000000,	1000000	,1000000},
    {360,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,0,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000},
    {240,	1000000	,1000000	,1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000	,1000000	,0	,1000000	,1000000,	1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000},
    {1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,320	,1000000	,1000000	,1000000,	0,	180,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000},
    {1000000	,1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	380,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	0,	180,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000},
    {1000000	,1000000,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	320,	1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000,	0,	110,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000	,1000000	,1000000},
    {1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,320	,1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,0	,110	,1000000	,1000000	,1000000,	1000000,	1000000	,1000000	,1000000},
    {1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000,	360	,1000000,	1000000	,1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	0	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000,	1000000},
    {1000000,	1000000,	1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000	,120	,240,	1000000,	1000000,	1000000	,1000000	,1000000,	0	,1000000,	1000000,	1000000,	1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	320,	1000000	,1000000,	150,	1000000,	1000000	,1000000,	1000000,	1000000,	0	,1000000,	1000000,	1000000	,1000000	,1000000},
    {1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,0,	1000000,	280,	335	,1000000},
    {1000000	,1000000,	1000000,	505,	1000000,	1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000	,160	,0	,1000000	,1000000	,1000000},
    {1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	0	,1000000,	985},
    {1000000,	1000000	,1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,1000000	,1000000	,1000000,	1000000	,1000000,	1000000,	1000000	,1000000,	0,	935},
    {600,	1000000,	1000000,	1000000	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000	,1000000	,1000000	,1000000,	1000000,	1000000	,1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000,	1000000	,1000000,	1000000,	1000000	,1000000	,1000000	,1000000	,0}};
        public static int[,] c = new int[30, 30];
        public static string[,] d = new string[30, 30];
        public static int[,] floyd(int[,] m)        ///floyd algrithum
        {
            int i = 1;
            int j = 1;
            int k = 1;
            for (k = 0; k < 30; k++)
            {
                for (i = 0; i < 30; i++)
                {
                    for (j = 0; j < 30; j++)
                    {
                        if (m[i, j] > m[i, k] + m[k, j])
                        {
                            m[i, j] = m[i, k] + m[k, j];
                            c[i, j] = k + 1;
                        }
                    }
                }
            }
            return m;
        }
        #region   creat the path, insert and sequence. and there is need a nest loop
        public static string path=null ;
        public static void start (int a ,int b)
        {
            if(c[a-1,b-1]!=0)
            {
                if((a<10))
                {
                    path = "0" + a.ToString();
                }
                else
                {
                    path = a.ToString();
                }
                if(c[a-1,b-1]<10)
                {
                    path = path + "0" + c[a - 1, b - 1].ToString();
                }
                else
                {
                    path = path + c[a - 1, b - 1].ToString();
                }
                if(b<10)
                {
                    path = path + "0" + b.ToString();
                }
                else
                {
                    path = path + b.ToString();
                }
                mid(c[a - 1, b - 1], a, b);
            }
            else
            {
                if ((a < 10))
                {
                    path = "0" + a.ToString();
                }
                else
                {
                    path = a.ToString();
                }
                if (b < 10)
                {
                    path = path + "0" + b.ToString();
                }
                else
                {
                    path = path + b.ToString();
                }
            }
        }
        public static void  mn(int a ,int b)
        {
            if (c[a-1,b-1] != 0)
            {
                string insert = c[a - 1, b - 1].ToString();
                if (c[a - 1, b - 1]<10)
                {
                    if (a < 10)
                    {
                        path = path.Insert(path.IndexOf("0" + a.ToString()) +2, "0" + insert);
                    }
                    else
                    {
                        path = path.Insert(path.IndexOf(a.ToString()) +2, "0" + insert);
                    }
                }
                else
                {
                    if (a < 10)
                    {
                        path = path.Insert(path.IndexOf("0" + a.ToString())+2, insert);
                    }
                    else
                    {
                        path = path.Insert(path.IndexOf(a.ToString()) +2, insert);
                    }
                }
                mid(c[a - 1, b - 1], a, b);
            }
        }
        public static void  pq(int a, int b)
        {
            if (c[a - 1, b - 1] != 0)
            {
                string insert = c[a - 1, b - 1].ToString();
                if (c[a - 1, b - 1] < 10)
                {
                    if(a<10)
                    {
                        path = path.Insert(path.IndexOf("0"+a.ToString()) +2, "0" + insert);
                    }
                    else
                    {
                        path = path.Insert(path.IndexOf(a.ToString()) +2, "0" + insert);
                    }
                }
                else
                {
                    if(a<10)
                    {
                        path = path.Insert(path.IndexOf("0"+a.ToString()) +2, insert);
                    }
                    else
                    {
                        path = path.Insert(path.IndexOf(a.ToString())+2 , insert);
                    }
                }
                mid(c[a - 1, b - 1], a, b);
            }
            
        }
        public static void  mid(int m,int a ,int b)
        {
            pq(m, b);
            mn(a, m);
        }
        public static void Path()
        {
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    start(i + 1, j + 1);
                    d[i, j] = algrithom.path;
                }
            }
        }
        #endregion
    }
}
