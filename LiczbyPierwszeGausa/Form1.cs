using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiczbyPierwszeGausa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.Black;
            textBox3.BackColor = Color.Red;
            textBox7.BackColor = Color.LightGreen;
            textBox4.Text = "6";
            textBox5.Text = "9";
            textBox6.Text = "0,66";
            textBox8.Text = "8";
        }
        public static bool isPrime(float n)
        {
            if (n <= 1)
                return false;

            for (int i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        public bool isGausPrime(float a, float b)
        {
            bool answer = false;
            float sum = a * a + b * b;
            if (isPrime(sum)) answer = true;
            if (a == 0 && isPrime(b) && b % 4 == 3) answer = true;
            if (b == 0 && isPrime(a) && a % 4 == 3) answer = true;

            return answer;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            float kw = 6000;
            float zakres = 1;
            float bc = 6;
            float rc = 9;
            float sc = 3 / 2;
            float offset = 8;
            if (textBox1.Text != "")
            {
              float.TryParse(textBox1.Text,out zakres);
            }
            if (textBox4.Text != "")
            {
                float.TryParse(textBox4.Text, out bc);
            }
            if (textBox5.Text != "")
            {
                float.TryParse(textBox5.Text, out rc);
            }
            if (textBox6.Text != "")
            {
                float.TryParse(textBox6.Text, out sc);
            }
            if (textBox8.Text != "")
            {
                float.TryParse(textBox8.Text, out offset);
            }
            if (offset == 0) { offset = 1; }

            float zakress = (zakres * 2)+1;
            float jed = kw / zakress;
        
            





             
            Bitmap mapa = new Bitmap(Convert.ToInt32(kw), Convert.ToInt32(kw));
            Pen bp = new Pen(textBox2.BackColor, bc);
            Pen br = new Pen(textBox3.BackColor, rc);
            Color gbp = textBox7.BackColor;
            SolidBrush gb = new SolidBrush(gbp);
            using (var g = Graphics.FromImage(mapa))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                int x = 1;
                int y = 1;
                // rysowanie poziome
                for (float i = jed; i < kw; i+=jed) {
                    
                    if (x==zakress/2 - 0.5 ||x == zakress / 2 + 0.5)
                        g.DrawLine(br, 0, i, kw, i);
                    else
                        g.DrawLine(bp, 0, i, kw, i);
                    x+=1;
                }
                // rysowanie pionowe
                for (float i = jed; i < kw; i += jed) {
                    if (y == zakress / 2 - 0.5 || y == zakress / 2 + 0.5)
                        g.DrawLine(br, i, 0, i, kw);
                    else
                        g.DrawLine(bp, i, 0, i, kw);
                    y += 1;
                }
                // zaznaczanie skali
               //poziom
                float z = -zakres;
                for (float i = 0; i < kw; i += jed)
                {

                    
                        
                        RectangleF rectf = new RectangleF(i+jed/5,kw/2 - jed/3,jed, jed);
                        g.DrawString(Convert.ToString(Convert.ToInt32(z)), new Font("Tahoma", jed/3), Brushes.Black, rectf);
                         g.Flush();
                    z += 1;

                }
                //pion
                z = zakres;
                for (float i = 0; i < kw; i += jed)
                {
                    RectangleF rectf = new RectangleF(kw / 2 - jed / 3,i + jed / 5, jed, jed);
                    if(z!=0)
                        g.DrawString(Convert.ToString(Convert.ToInt32(z)), new Font("Tahoma", jed / 3), Brushes.Black, rectf);
                    g.Flush();
                    z -= 1;
                }


            }
            
            // liczenie Gausa
            for (float a = 0; a <= zakres; a++)
            {
                for (float b = 0; b <= zakres; b++)
                {
                    if (isGausPrime(a, b))
                    {
                        //MessageBox.Show(a.ToString()+ " " +  b.ToString());

                        // jeśli gaus
                        float x = a + zakres;
                        float y = b +zakres;
                        using (var graphics = Graphics.FromImage(mapa))
                        {
                            
                            graphics.FillRectangle(gb, x*jed+jed/offset, y*jed+jed/offset , jed * sc, jed * sc);
                            graphics.FillRectangle(gb, -x * jed + jed / offset + kw - jed, -y * jed + jed / offset + kw - jed, jed * sc, jed * sc);
                            graphics.FillRectangle(gb, x * jed + jed / offset, -y * jed + jed / offset + kw - jed, jed * sc, jed * sc);
                            graphics.FillRectangle(gb, -x * jed + jed / offset + kw - jed, y * jed + jed / offset, jed * sc, jed * sc);


                        }


                    }
                }

            }
           
            mapa.Save("pic.bmp");
            pictureBox1.Image = mapa;
           // MessageBox.Show("done");
            // koniec fora
            




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"pic.bmp");
            } catch (Exception ex)
            { }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBox2.BackColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBox3.BackColor = colorDialog2.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                textBox7.BackColor = colorDialog3.Color;
            }
        }
    }
}
