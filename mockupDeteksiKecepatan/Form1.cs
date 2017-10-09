using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace mockupDeteksiKecepatan
{    
    public partial class Form1 : Form
    {
        Bitmap objBitmap;
        int blurAmount;
        float[] gn = new float[384];
        float[,] h = new float[4, 384];
        float[,] hs = new float[4, 384];
        public Form1()
        {
            InitializeComponent();
        }

        private void histogram(Bitmap bitmap, int indeks, System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            for (int i = 0; i < 384; i++)
                h[indeks, i] = 0;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int red = color.R;
                    int green = color.G;
                    int blue = color.B;
                    red /= 2; green /= 2; blue /= 2;
                    h[indeks, red]++;
                    h[indeks, 128 + green]++;
                    h[indeks, 256 + blue]++;
                }
            }
            float hmax = h[indeks, 0];
            for (int i = 1; i < 384; i++)
                if (h[indeks, i] > hmax) hmax = h[indeks, i];

            for (int i = 0; i < 384; i++)
                h[indeks, i] = 110 * h[indeks, i] / hmax;

            for (int i = 0; i < 384; i++)
                chart.Series["Series1"].Points.AddXY(i, h[indeks, i]);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "1234";
            for (int x = 0; x < objBitmap.Width; x++)
            {
                for (int y = 0; y < objBitmap.Height; y++)
                {
                    try
                    {
                        Color prevX = objBitmap.GetPixel(x - 2, y);
                        Color nextX = objBitmap.GetPixel(x + 2, y);
                        Color prevY = objBitmap.GetPixel(x, y - 2);
                        Color nextY = objBitmap.GetPixel(x, y + 2);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                        objBitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                        
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            pictureBox3.Image = objBitmap;
 
        }

        private void browser_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                objBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = objBitmap;
            }

            histogram(objBitmap, 0, chart1);

        }       
        
        private void process_Click(object sender, EventArgs e)
        {

        }

        private void grayscale_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < objBitmap.Width; x++)
            {
                for (int y = 0; y < objBitmap.Height; y++)
                {
                    Color originalColor = objBitmap.GetPixel(x, y);
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    objBitmap.SetPixel(x, y, newColor);
                }
            }
            pictureBox2.Image = objBitmap;
        }

        private void updateBlur(object sender, EventArgs e)
        {
            blurAmount = int.Parse(trackBar1.Value.ToString());
        }
    }
}
