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
                    if(!String.IsNullOrWhiteSpace(number))
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
        private void SendButton_Click(object sender, EventArgs e)
        {
            View form = new View();
            form.Ultrasound = AjustUltraSoundGain(float.Parse(GainValueTextBox.Text, CultureInfo.InvariantCulture));
            form.Show();
            /*
            var values = new
            {
                Ultrasound = AjustUltraSoundGain(float.Parse(GainValueTextBox.Text, CultureInfo.InvariantCulture))
            };
            var stringPayload = JsonConvert.SerializeObject(values);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://csm30-ultrasound-api.herokuapp.com/", content);

            var responseString = await response.Content.ReadAsStringAsync();*/
        }

        private void TrackBar1_ValueChanged(object sender, System.EventArgs e)
        {
                GainValueTextBox.Text = ((float)GainTrackBar.Value/2).ToString("0.0",CultureInfo.InvariantCulture);
        }
        private void ImgHeight_LostFocus(object sender, System.EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ImgHeight.Text))
                ImgHeight.Text = "60";
            MatrixWidth.Text = (int.Parse(ImgHeight.Text) * int.Parse(ImgWidth.Text)).ToString();
        }
        private void ImgWidth_LostFocus(object sender, System.EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ImgWidth.Text))
                ImgWidth.Text = "60";
            MatrixWidth.Text = (int.Parse(ImgHeight.Text) * int.Parse(ImgWidth.Text)).ToString();
        }

        private void VectorSizeTextBox_LostFocus(object sender, System.EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(VectorSizeTextBox.Text))
                VectorSizeTextBox.Text = "50816";
            MatrixHeight.Text = VectorSizeTextBox.Text;
        }
        private void Panel1_Click(object sender, System.EventArgs e)
        {
            panel1.Focus();
        }

    }
}
