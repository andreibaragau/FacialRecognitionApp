using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Facial_Recognition_App
{
    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        public int Factor = 1;
        public int Offset = 0;
        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }
    }


    public class BitmapFilter
    {
        public static bool Invert(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        p[0] = (byte)(255 - p[0]);
                        ++p;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }


        public static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride + 6 - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
    }

        public partial class Form2 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=licenta;Uid=root;Pwd=;SslMode=none;");
        public Form2()
        {
            InitializeComponent();
        }


        #region preprocesare
       


        public Bitmap bilash_filter(Bitmap bp)
        {

            Bitmap bp_original;
            bp_original = (Bitmap)bp.Clone();

            int count, k;

            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];

            int[] count_red = new int[256];
            int[] count_green = new int[256];
            int[] count_blue = new int[256];


            for (int i = 0; i < bp_original.Width; i++)
            {

                for (int j = 0; j < bp_original.Height; j++)
                {
                    count = 0;

                    if (i + 1 < bp_original.Width && j + 1 < bp_original.Height)
                    {
                        red[count] = bp_original.GetPixel(i + 1, j + 1).R;
                        green[count] = bp_original.GetPixel(i + 1, j + 1).G;
                        blue[count] = bp_original.GetPixel(i + 1, j + 1).B;
                        count++;
                    }
                    if (i + 1 < bp_original.Width && j - 1 >= 0)
                    {
                        red[count] = bp_original.GetPixel(i + 1, j - 1).R;
                        green[count] = bp_original.GetPixel(i + 1, j - 1).G;
                        blue[count] = bp_original.GetPixel(i + 1, j - 1).B;
                        count++;
                    }
                    if (i - 1 >= 0 && j + 1 < bp_original.Height)
                    {
                        red[count] = bp_original.GetPixel(i - 1, j + 1).R;
                        green[count] = bp_original.GetPixel(i - 1, j + 1).G;
                        blue[count] = bp_original.GetPixel(i - 1, j + 1).B;
                        count++;
                    }
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        red[count] = bp_original.GetPixel(i - 1, j - 1).R;
                        green[count] = bp_original.GetPixel(i - 1, j - 1).G;
                        blue[count] = bp_original.GetPixel(i - 1, j - 1).B;
                        count++;
                    }


                    if (j + 1 < bp_original.Height)
                    {
                        red[count] = bp_original.GetPixel(i, j + 1).R;
                        green[count] = bp_original.GetPixel(i, j + 1).G;
                        blue[count] = bp_original.GetPixel(i, j + 1).B;
                        count++;
                    }
                    if (i + 1 < bp_original.Width)
                    {
                        red[count] = bp_original.GetPixel(i + 1, j).R;
                        green[count] = bp_original.GetPixel(i + 1, j).G;
                        blue[count] = bp_original.GetPixel(i + 1, j).B;
                        count++;
                    }
                    if (i - 1 >= 0)
                    {
                        red[count] = bp_original.GetPixel(i - 1, j).R;
                        green[count] = bp_original.GetPixel(i - 1, j).G;
                        blue[count] = bp_original.GetPixel(i - 1, j).B;
                        count++;
                    }
                    if (j - 1 >= 0)
                    {
                        red[count] = bp_original.GetPixel(i, j - 1).R;
                        green[count] = bp_original.GetPixel(i, j - 1).G;
                        blue[count] = bp_original.GetPixel(i, j - 1).B;
                        count++;
                    }


                    red[count] = bp_original.GetPixel(i, j).R;
                    green[count] = bp_original.GetPixel(i, j).G;
                    blue[count] = bp_original.GetPixel(i, j).B;
                    count++;

                    int max_red = 0, max_blue = 0, max_green = 0;


                    int max_red1 = 0, max_blue1 = 0, max_green1 = 0;
                    int flag_red = 0, flag_blue = 0, flag_green = 0;

                    int min_red = 256, min_blue = 256, min_green = 256;

                    for (k = 0; k < count; k++)
                    {
                        count_red[red[k]]++;

                        if (count_red[red[k]] > 1)
                            flag_red = 1;

                        count_green[green[k]]++;

                        if (count_green[green[k]] > 1)
                            flag_green = 1;

                        count_blue[blue[k]]++;

                        if (count_blue[blue[k]] > 1)
                            flag_blue = 1;


                    }

                    int index_r, index_g, index_b;

                    index_r = 0;
                    index_g = 0;
                    index_b = 0;

                    if ((flag_blue + flag_green + flag_red) != 0)
                    {
                        for (k = 0; k < count; k++)
                        {
                            if (max_red < count_red[red[k]])
                            {
                                max_red = count_red[red[k]];
                                max_red1 = red[k];
                                index_r = k;

                            }

                            if (max_green < count_green[green[k]])
                            {
                                max_green = count_green[green[k]];
                                max_green1 = green[k];
                                index_g = k;

                            }
                            if (max_blue < count_blue[blue[k]])
                            {
                                max_blue = count_blue[blue[k]];
                                max_blue1 = blue[k];
                                index_b = k;

                            }


                        }
                    }


                    int max = max_red;

                    if (max < max_green)
                        max = max_green;
                    if (max < max_blue)
                        max = max_blue;



                    if (flag_red == 0 && flag_green == 0 && flag_blue == 0)
                    {
                        max_red1 = bp.GetPixel(i, j).R;
                        max_green1 = bp.GetPixel(i, j).G;
                        max_blue1 = bp.GetPixel(i, j).B;
                    }

                    else
                    {
                        if (max == max_blue)
                        {
                            max_blue1 = blue[index_b];
                            max_green1 = green[index_b];
                            max_red1 = red[index_b];
                        }
                        else if (max == max_red)
                        {
                            max_blue1 = blue[index_r];
                            max_green1 = green[index_r];
                            max_red1 = red[index_r];
                        }
                        else if (max == max_green1)
                        {
                            max_blue1 = blue[index_g];
                            max_green1 = green[index_g];
                            max_red1 = red[index_g];
                        }
                    }

                    bp.SetPixel(i, j, Color.FromArgb(max_red1, max_green1, max_blue1));

                    Array.Clear(count_blue, 0, 256);
                    Array.Clear(count_green, 0, 256);
                    Array.Clear(count_red, 0, 256);
                }
            }

            Random rand = new Random();
            bp.Save("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\brilashh.png");
            return bp;

        }

        #region filtre nefolosite
        public static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride + 6 - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }

        public static bool Smooth(Bitmap b, int nWeight /* default to 1 */)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.Factor = nWeight + 8;

            return BitmapFilter.Conv3x3(b, m);
        }

        public static bool GaussianBlur(Bitmap b, int nWeight /* default to 4*/)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;

            return BitmapFilter.Conv3x3(b, m);
        }
        public static bool MeanRemoval(Bitmap b, int nWeight /* default to 9*/ )
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(-1);
            m.Pixel = nWeight;
            m.Factor = nWeight - 8;

            return BitmapFilter.Conv3x3(b, m);
        }
        public static bool Sharpen(Bitmap b, int nWeight /* default to 11*/ )
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(0);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -2;
            m.Factor = nWeight - 8;

            return BitmapFilter.Conv3x3(b, m);
        }
        #endregion

        #endregion







        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
           /*
            string query = "INSERT INTO info (username, bpm, data, stare, aritmia) VALUES('" + LogIn.username + "','" + Form_Ekg.BPM + "','" + DateTime.Now.ToString("M_d_yyyy") + "','" + comboBox1.Text + "','" + aritmia + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Valoare salvata!");
           */
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\andrei.jpg");
            Filters.grayFilter(bmp);
            Filters.contrast_function(bmp);
            Filters.Brightness(bmp, 100);
            bilash_filter(bmp);

        }
        /*
try
   {
       conn.Open();
       string p = LogIn.username;
string query = "select bpm, stare, aritmia, data  from info where username='" + LogIn.username + "'";
MySqlCommand cmd = new MySqlCommand(query, conn);

MySqlDataAdapter SDA = new MySqlDataAdapter(query, conn);
DataTable dt = new DataTable();
SDA.Fill(dt);

       dataGridView1.DataSource = dt;


       chart1.Series["Series1"].XValueMember = "data";
       chart1.Series["Series1"].YValueMembers = "bpm";
       chart1.DataSource = dt;
       chart1.DataBind();

       conn.Close();
   }
   catch (Exception except)
   {
       MessageBox.Show(except.Message);
   }*/
    }
}
