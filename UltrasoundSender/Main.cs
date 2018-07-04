using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Newtonsoft.Json;
using DotNumerics.LinearAlgebra;

namespace UltrasoundSender
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public IList<float> Ultrasound;
        public float[] ultravector;
        private static readonly HttpClient client = new HttpClient();
        private Matrix H = null,g,x;
        private void SearchButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text File|*.txt";
            openFileDialog1.Title = "Select a Ultrasound File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePathTextBox.Text = openFileDialog1.FileName;
                string[] buffer = File.ReadAllText(FilePathTextBox.Text).Split('\n');
                Ultrasound = new List<float>();
                foreach (string number in buffer)
                {
                    if (!String.IsNullOrWhiteSpace(number))
                        Ultrasound.Add(float.Parse(number, CultureInfo.InvariantCulture));
                }
                VectorSizeTextBox.Text = Ultrasound.Count.ToString();
                SendButton.Enabled = true;
            }
        }

        private float[] AjustUltraSoundGain(float gain)
        {
            if (gain != 1)
            {
                int i = 0;
                int size = Ultrasound.Count;
                int tenPer = size / 10;
                float auxG;
                float[] ret = new float[size];
                auxG = (gain / 2 - gain / 20);
                foreach (float value in Ultrasound)
                {
                    if (i % tenPer == 0)
                        auxG += gain / 20;
                    ret[i] = value * auxG;
                    i++;
                }
                return ret;
            }
            return Ultrasound.ToArray();
        }
        private async void SendButton_Click(object sender, EventArgs e)
        {

            if (H == null)
            {
                H = new Matrix(50816, 3600);
                string path = "E:\\Files\\H-1.txt";

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
            ultravector = AjustUltraSoundGain(float.Parse(GainValueTextBox.Text));
            CriaImagem();
            var values = new
            {
                ImgHeight = int.Parse(ImgHeight.Text),
                ImgWidth = int.Parse(ImgWidth.Text),
                Ultrasound = AjustUltraSoundGain(float.Parse(GainValueTextBox.Text, CultureInfo.InvariantCulture))
            };
            var stringPayload = JsonConvert.SerializeObject(values);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://csm30-ultrasound-api.herokuapp.com/ultrasound", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }

        private void TrackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            GainValueTextBox.Text = ((float)GainTrackBar.Value).ToString(CultureInfo.InvariantCulture);
        }
        private void ImgHeight_LostFocus(object sender, System.EventArgs e)
        {
            if (!int.TryParse(ImgHeight.Text, out int n))
                ImgHeight.Text = "60";
            MatrixWidth.Text = (int.Parse(ImgHeight.Text) * int.Parse(ImgWidth.Text)).ToString();
        }
        private void ImgWidth_LostFocus(object sender, System.EventArgs e)
        {
            if (!int.TryParse(ImgWidth.Text, out int n))
                ImgWidth.Text = "60";
            MatrixWidth.Text = (int.Parse(ImgHeight.Text) * int.Parse(ImgWidth.Text)).ToString();
        }

        private void VectorSizeTextBox_LostFocus(object sender, System.EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(VectorSizeTextBox.Text))
                VectorSizeTextBox.Text = "50816";
            MatrixHeight.Text = VectorSizeTextBox.Text;
        }

        private void ViewImagesButton_Click(object sender, EventArgs e)
        {
            View form = new View();
            form.Show();
        }

        private void Panel1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
        }


        //serve pra multiplicar com transposta sem precisar alocar nova matrix na memória, por que da out of memory exception
        private Matrix MatrixMultTranpose(Matrix a, Matrix b)
        {
            int col = b.ColumnCount, row = a.ColumnCount;
            int m = a.RowCount;
            Matrix c = new Matrix(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m; k++)
                        sum += a[k, i] * b[k, j];
                    c[i, j] = sum;
                }
            }
            return c;
        }
        /*H é a matrix gigante de tramanho(50816, 3600), 
         * g é o vetor de ultrasom passado de tamanho (50816, 1)
         * f é a saida de tamanho (3600,1) que vai ser iniciada em 0
         * O tamanho 50816 vem do tamannho do vetor de entrada
         * O tamanho 3600 vem do tamanho da imagem final (60X60)
         */
        private void CGNE(Matrix H, Matrix g, out Matrix f)
        {
            f = new Matrix(3600, 1);
            for (int i = 0; i < 3600; i++)//f0=0
            {
                f[i, 0] = 0;
            }
            Matrix r = g-H*f;//r0 = g - Hf0

            Matrix p = MatrixMultTranpose(H, r); //p0 = HTr0
            double a, B;
            double rtXr = (r.Transpose()* r)[0, 0]; //=riT * ri serve pra não precisar calcular duas vezes
            double ritXri;//=ri+1T * ri+1 serve pra não precisar calcular duas vezes
            Matrix ri;// ri+1
            for (int i = 0; i < 15; i++)
            {
                a = rtXr / (p.Transpose()* p)[0, 0];//ai = riT * ri / piT * pi
                f = f + p.Multiply(a);//fi+1 = fi + ai * pi
                ri = r - (H* p).Multiply(a);//ri+1 = ri - ai * H * pi
                ritXri = (ri.Transpose()*ri)[0, 0];//=ri+1T * ri+1
                B = ritXri / rtXr;//Bi = ri+1T * ri+1 / riT * ri
                p = MatrixMultTranpose(H, ri) + p.Multiply(B);//pi = HT * ri+1 + Bi * pi
                r = ri;// ri = ri+1
                rtXr = ritXri;
            }
        }

        private void CriaImagem()
        {
            g = new Matrix(50816, 1);
            for (int i = 0; i < 50816; i++)//constroi matrix do vetor de entrada
            {
                g[i, 0] = ultravector[i];
            }

            CGNE(H, g, out x);

            //Parte que atribui valor de 0 até 255 para a imagem
            Bitmap bmp = new Bitmap(60, 60);
            double max = double.NegativeInfinity, min = double.PositiveInfinity;
            for (int i = 0; i < 3600; i++)//Calcula valor máximo e mínimo da imagem
            {
                if (x[i, 0] > max)
                    max = x[i, 0];
                if (x[i, 0] < min)
                    min = x[i, 0];
            }
            int k = 0;
            int value;
            for (int i = 0; i < 60; i++)
                for (int j = 0; j < 60; j++)
                {
                    value = (int)((255 / (max - min)) * (x[k, 0] - min));
                    bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                    k++;
                }
            bmp.Save("img.bmp");
        }
    }
}
