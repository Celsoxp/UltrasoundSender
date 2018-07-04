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
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = images.ElementAt(imageAt);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if (imageAt != 0)
                PrevButton.Enabled = true;
            if (imageAt == images.Count - 1)
                NextButton.Enabled = false;
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            imageAt--;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = images.ElementAt(imageAt);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if (imageAt == 0)
                PrevButton.Enabled = false;
            NextButton.Enabled = true;
        }
    }
}
