namespace UltrasoundSender
{
    partial class Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.SearchButton = new System.Windows.Forms.Button();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.GainTrackBar = new System.Windows.Forms.TrackBar();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ImgHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ImgWidth = new System.Windows.Forms.TextBox();
            this.GainValueTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.VectorSizeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MatrixWidth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MatrixHeight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ViewImagesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GainTrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.CausesValidation = false;
            this.SearchButton.Location = new System.Drawing.Point(423, 57);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(95, 32);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Procurar";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Enabled = false;
            this.FilePathTextBox.Location = new System.Drawing.Point(15, 29);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(503, 22);
            this.FilePathTextBox.TabIndex = 2;
            // 
            // GainTrackBar
            // 
            this.GainTrackBar.Location = new System.Drawing.Point(12, 215);
            this.GainTrackBar.Maximum = 5;
            this.GainTrackBar.Minimum = -5;
            this.GainTrackBar.Name = "GainTrackBar";
            this.GainTrackBar.Size = new System.Drawing.Size(377, 56);
            this.GainTrackBar.TabIndex = 3;
            this.GainTrackBar.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // SendButton
            // 
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(423, 215);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(95, 32);
            this.SendButton.TabIndex = 5;
            this.SendButton.Text = "Enviar";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ajuste de Ganho";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tamanho da Imagem ( 60 X 60 por padrão)";
            // 
            // ImgHeight
            // 
            this.ImgHeight.Location = new System.Drawing.Point(15, 110);
            this.ImgHeight.Name = "ImgHeight";
            this.ImgHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ImgHeight.Size = new System.Drawing.Size(49, 22);
            this.ImgHeight.TabIndex = 8;
            this.ImgHeight.Text = "60";
            this.ImgHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ImgHeight.LostFocus += new System.EventHandler(this.ImgHeight_LostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 161);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "X";
            // 
            // ImgWidth
            // 
            this.ImgWidth.Location = new System.Drawing.Point(146, 110);
            this.ImgWidth.Name = "ImgWidth";
            this.ImgWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ImgWidth.Size = new System.Drawing.Size(49, 22);
            this.ImgWidth.TabIndex = 10;
            this.ImgWidth.Text = "60";
            this.ImgWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ImgWidth.LostFocus += new System.EventHandler(this.ImgWidth_LostFocus);
            // 
            // GainValueTextBox
            // 
            this.GainValueTextBox.Enabled = false;
            this.GainValueTextBox.Location = new System.Drawing.Point(153, 192);
            this.GainValueTextBox.Name = "GainValueTextBox";
            this.GainValueTextBox.Size = new System.Drawing.Size(42, 22);
            this.GainValueTextBox.TabIndex = 11;
            this.GainValueTextBox.Text = "1";
            this.GainValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 197);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "=*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Caminho do Arquivo de Ultrasom";
            // 
            // VectorSizeTextBox
            // 
            this.VectorSizeTextBox.Enabled = false;
            this.VectorSizeTextBox.Location = new System.Drawing.Point(244, 62);
            this.VectorSizeTextBox.Name = "VectorSizeTextBox";
            this.VectorSizeTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VectorSizeTextBox.Size = new System.Drawing.Size(50, 22);
            this.VectorSizeTextBox.TabIndex = 15;
            this.VectorSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VectorSizeTextBox.TextChanged += new System.EventHandler(this.VectorSizeTextBox_LostFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(206, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Tamanho do Vetor do Ultrasom";
            // 
            // MatrixWidth
            // 
            this.MatrixWidth.Enabled = false;
            this.MatrixWidth.Location = new System.Drawing.Point(146, 154);
            this.MatrixWidth.Name = "MatrixWidth";
            this.MatrixWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MatrixWidth.Size = new System.Drawing.Size(49, 22);
            this.MatrixWidth.TabIndex = 19;
            this.MatrixWidth.Text = "3600";
            this.MatrixWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 113);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "X";
            // 
            // MatrixHeight
            // 
            this.MatrixHeight.Enabled = false;
            this.MatrixHeight.Location = new System.Drawing.Point(15, 156);
            this.MatrixHeight.Name = "MatrixHeight";
            this.MatrixHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MatrixHeight.Size = new System.Drawing.Size(49, 22);
            this.MatrixHeight.TabIndex = 17;
            this.MatrixHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(250, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Tamanho da Matriz de Transformação";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ViewImagesButton);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.MatrixWidth);
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.FilePathTextBox);
            this.panel1.Controls.Add(this.MatrixHeight);
            this.panel1.Controls.Add(this.GainTrackBar);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.SendButton);
            this.panel1.Controls.Add(this.VectorSizeTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ImgHeight);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.GainValueTextBox);
            this.panel1.Controls.Add(this.ImgWidth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 325);
            this.panel1.TabIndex = 20;
            this.panel1.Click += new System.EventHandler(this.Panel1_Click);
            // 
            // ViewImagesButton
            // 
            this.ViewImagesButton.Location = new System.Drawing.Point(15, 277);
            this.ViewImagesButton.Name = "ViewImagesButton";
            this.ViewImagesButton.Size = new System.Drawing.Size(503, 32);
            this.ViewImagesButton.TabIndex = 20;
            this.ViewImagesButton.Text = "Vizualizar Imagens do Servidor";
            this.ViewImagesButton.UseVisualStyleBackColor = true;
            this.ViewImagesButton.Click += new System.EventHandler(this.ViewImagesButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 325);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Envio de Ultrasom";
            ((System.ComponentModel.ISupportInitialize)(this.GainTrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.TrackBar GainTrackBar;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ImgHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ImgWidth;
        private System.Windows.Forms.TextBox GainValueTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox VectorSizeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MatrixWidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MatrixHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ViewImagesButton;
    }
}

