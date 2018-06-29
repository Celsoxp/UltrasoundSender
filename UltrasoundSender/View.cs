using DotNumerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltrasoundSender
{
    public partial class View : Form
    {
        public View()
        {
            images = new List<Bitmap>();
            FillH();
            InitializeComponent();
        }
        
        private IList<Bitmap> images;
        private Matrix H,g,x;
        public float[] Ultrasound;
        private static readonly HttpClient client = new HttpClient();

        private void FillH()
        {
            H = new Matrix(50816, 3600);
            string path = "E:\\files\\H-1.txt";

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            for (int i = 0; i < 50816; i++)
            {
                string[] buffer = file.ReadLine().Split(',');
                for (int j = 0; j < 3600; j++)
                    if (!String.IsNullOrWhiteSpace(buffer[j]))
                        H[i, j] = float.Parse(buffer[j], CultureInfo.InvariantCulture);
            }
            file.Close();
        }

        private Matrix MatrixSubtraction(Matrix a, Matrix b)
        {
            int col = a.ColumnCount, row = b.RowCount;
            Matrix c = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }
            return c;
        }
        private Matrix MatrixSum(Matrix a, Matrix b)
        {
            int col = a.ColumnCount, row = b.RowCount;
            Matrix c = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }
        private Matrix MatrixXScalar(Matrix a, double scalar)
        {
            int col = a.ColumnCount, row = a.RowCount;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    a[i, j] = a[i, j]*scalar;
                }
            }
            return a;
        }
        private Matrix Transpose(Matrix a)
        {
            int col = a.ColumnCount, row = a.RowCount;
            Matrix t = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    t[j, i] = a[i, j];
                }
            }

            return t;
        }

        private void CGNE (Matrix H, Matrix g, out Matrix x)
        {
            x = new Matrix(3600, 1);
            for (int i = 0; i < 3600; i++)
            {
                x[i, 0] = Ultrasound[i];
            }
            Matrix r = MatrixSubtraction(g,H * x);

            Matrix p = Transpose(H) * r;
            double a, B, rtXr = (Transpose(r) * r)[0,0], ritXri;
            Matrix ri;
            for (int i = 0; i < 15; i++)
            {
                while (true)
                {
                    a = rtXr / (Transpose(p) * p)[0, 0];
                    x = MatrixSum(x, MatrixXScalar(p, a));
                    ri = MatrixSubtraction(r, MatrixXScalar(H * p, a));
                    ritXri = (Transpose(ri) * ri)[0,0];
                    if (Math.Sqrt(ritXri) < 1e-10)
                        break;
                    B = ritXri/rtXr;
                    r = ri;
                    p = MatrixSum(Transpose(H) * r, MatrixXScalar(p, B));
                    rtXr = ritXri;
                }
            }
        }

        private void View_Shown(object sender, EventArgs e)
        {
            g = new Matrix(50816, 1);
            for (int i = 0; i < 50816; i++)
            {
                g[i, 0] = Ultrasound[i];
            }

            CGNE(H, g, out x);

            Bitmap bmp = new Bitmap(60,60, PixelFormat.Format16bppGrayScale);

            int k = 0;
            for (int i = 0; i < 60; i++)
                for (int j = 0; j < 60; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb((int)Math.Round((x[k,0]))));
                    k++;
                }
            images.Add(bmp);
            pictureBox1.Image = images.First();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {

        }

        private void PrevButton_Click(object sender, EventArgs e)
        {

        }

    }
}
