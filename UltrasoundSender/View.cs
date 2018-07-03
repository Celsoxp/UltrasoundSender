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
using Newtonsoft.Json;

namespace UltrasoundSender
{
    public partial class View : Form
    {
        public class Response
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("image")]
            public string image { get; set; }
            [JsonProperty("image_height")]
            public int image_height { get; set; }
            [JsonProperty("image_width")]
            public int image_width { get; set; }
            [JsonProperty("created_at")]
            public string created_at { get; set; }
        }

        public class ResponseList
        {
            [JsonProperty("ultrasounds")]
            public IList<Response> ultrasounds { get; set; }
        }

        public View()
        {
            InitializeComponent();
            getImages();
        }

        private IList<Bitmap> images;
        private Matrix H = null, g, x;
        public float[] Ultrasound;
        private static readonly HttpClient client = new HttpClient();
        int imageAt = 0;
        private static Bitmap Base64StringToBitmap(string base64String)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
            {
                var bmpReturn = (Bitmap)System.Drawing.Image.FromStream(memoryStream);
                memoryStream.Close();
                return bmpReturn;
            }
        }

        private async void getImages()
        {
            images = new List<Bitmap>();

            var response = await client.GetAsync("https://csm30-ultrasound-api.herokuapp.com/ultrasound");

            var responseString = await response.Content.ReadAsStringAsync();

            ResponseList dataObjects = JsonConvert.DeserializeObject<ResponseList>(responseString);

            foreach (Response ult in dataObjects.ultrasounds)
                images.Add(Base64StringToBitmap(ult.image.Split(',')[1]));
            if (images.Count > 0)
                NextButton.Enabled = true;
            else
                NextButton.Enabled = false;
            PrevButton.Enabled = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = images.ElementAt(0);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            imageAt++;
            pictureBox1.Image = images.ElementAt(imageAt);
            if (imageAt != 0)
                PrevButton.Enabled = true;
            if (imageAt == images.Count - 1)
                NextButton.Enabled = false;
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            imageAt--;
            pictureBox1.Image = images.ElementAt(imageAt);
            if (imageAt == 0)
                PrevButton.Enabled = false;
            NextButton.Enabled = true;
        }


        /// <summary>
        /// Remover depois o que está abaixo
        /// </summary>
        private void FillH()
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
        }

        //Muliplica toda a matrix por um valor fixo(scalar)
        private Matrix MatrixXScalar(Matrix a, double scalar)
        {
            int col = a.ColumnCount, row = a.RowCount;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    a[i, j] = a[i, j] * scalar;
                }
            }
            return a;
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
                        sum += a[k, i] + b[k, j];
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
            Matrix r = g - H * f;//r0 = g - Hf0

            Matrix p = MatrixMultTranpose(H, r); //p0 = HTr0
            double a, B;
            double rtXr = MatrixMultTranpose(r, r)[0, 0]; //=riT * ri serve pra não precisar calcular duas vezes
            double ritXri;//=ri+1T * ri+1 serve pra não precisar calcular duas vezes
            Matrix ri;// ri+1
            for (int i = 0; i < 15; i++)
            {
                a = rtXr / MatrixMultTranpose(p, p)[0, 0];//ai = riT * ri / piT * pi
                f = f + MatrixXScalar(p, a);//fi+1 = fi + ai * pi
                ri = r - MatrixXScalar(H * p, a);//ri+1 = ri - ai * H * pi
                ritXri = MatrixMultTranpose(ri, ri)[0, 0];//=ri+1T * ri+1
                B = ritXri / rtXr;//Bi = ri+1T * ri+1 / riT * ri
                p = MatrixMultTranpose(H, ri) + MatrixXScalar(p, B);//pi = HT * ri+1 + Bi * pi
                r = ri;// ri = ri+1
                rtXr = ritXri;
            }
        }

        private void CriaImagem()
        {
            g = new Matrix(50816, 1);
            for (int i = 0; i < 50816; i++)//constroi matrix do vetor de entrada
            {
                g[i, 0] = Ultrasound[i];
            }
            string aux = JsonConvert.SerializeObject(g.CopyToArray());
            CGNE(H, g, out x);
            aux = JsonConvert.SerializeObject(x.CopyToArray());

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
            if (min < 0 && max > 0)//Se os valores possuem dois sinais diferentes,
                                   //Primeiro o minimo é subtraido de todos os valores para que todos fiquem positivos. (subtrai por que o minimo é negativo)
                                   //Ai divide o resultado pelo maior valor (que vai ser max-min) pra deixar o valor de 0 até 1
                                   //No fim multiplica por 255.999 e trunca pra ficar um valor de 0 até 255
                for (int i = 0; i < 60; i++)
                    for (int j = 0; j < 60; j++)
                    {
                        value = (int)(((x[k, 0] - min) / (max - min)) * 255.999);
                        bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                        k++;
                    }
            else if (max > 0)//Caso todos os valores tenham o mesmo sinal, só divide eles pelo máximo e multiplica por 255.999.
                for (int i = 0; i < 60; i++)
                    for (int j = 0; j < 60; j++)
                    {
                        value = (int)((x[k, 0] / max) * 255.999);
                        bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                        k++;
                    }
            else //Caso todos os valores sejam negativos eles tem que ser divididos pelo mínimo.
                for (int i = 0; i < 60; i++)
                    for (int j = 0; j < 60; j++)
                    {
                        value = (int)((x[k, 0] / min) * 255.999);
                        bmp.SetPixel(i, j, Color.FromArgb(value, value, value));
                        k++;
                    }

            images.Add(bmp);
            pictureBox1.Image = images.First();
            bmp.Save("img.bmp");
        }


    }
}
