using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facial_Recognition_App
{

    class ConvolutionMatrix
    {
        public ConvolutionMatrix()
        {
            Pixel = 1;
            Factor = 1;
        }

        public void Apply(int Val)
        {
            TopLeft = TopMid = TopRight = MidLeft = MidRight = BottomLeft = BottomMid = BottomRight = Pixel = Val;
        }

        public int TopLeft { get; set; }

        public int TopMid { get; set; }

        public int TopRight { get; set; }

        public int MidLeft { get; set; }

        public int MidRight { get; set; }

        public int BottomLeft { get; set; }

        public int BottomMid { get; set; }

        public int BottomRight { get; set; }

        public int Pixel { get; set; }

        public int Factor { get; set; }

        public int Offset { get; set; }
    }

    class Convolution
    {
        public void Convolution3x3(ref Bitmap bmp)
        {
            int Factor = Matrix.Factor;

            if (Factor == 0) return;

            int TopLeft = Matrix.TopLeft;
            int TopMid = Matrix.TopMid;
            int TopRight = Matrix.TopRight;
            int MidLeft = Matrix.MidLeft;
            int MidRight = Matrix.MidRight;
            int BottomLeft = Matrix.BottomLeft;
            int BottomMid = Matrix.BottomMid;
            int BottomRight = Matrix.BottomRight;
            int Pixel = Matrix.Pixel;
            int Offset = Matrix.Offset;

            Bitmap TempBmp = (Bitmap)bmp.Clone();

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData TempBmpData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0.ToPointer();
                byte* TempPtr = (byte*)TempBmpData.Scan0.ToPointer();

                int Pix = 0;
                int Stride = bmpData.Stride;
                int DoubleStride = Stride * 2;
                int Width = bmp.Width - 2;
                int Height = bmp.Height - 2;
                int stopAddress = (int)ptr + bmpData.Stride * bmpData.Height;

                for (int y = 0; y < Height; ++y)
                    for (int x = 0; x < Width; ++x)
                    {
                        Pix = (((((TempPtr[2] * TopLeft) + (TempPtr[5] * TopMid) + (TempPtr[8] * TopRight)) +
                          ((TempPtr[2 + Stride] * MidLeft) + (TempPtr[5 + Stride] * Pixel) + (TempPtr[8 + Stride] * MidRight)) +
                          ((TempPtr[2 + DoubleStride] * BottomLeft) + (TempPtr[5 + DoubleStride] * BottomMid) + (TempPtr[8 + DoubleStride] * BottomRight))) / Factor) + Offset);

                        if (Pix < 0) Pix = 0;
                        else if (Pix > 255) Pix = 255;

                        ptr[5 + Stride] = (byte)Pix;

                        Pix = (((((TempPtr[1] * TopLeft) + (TempPtr[4] * TopMid) + (TempPtr[7] * TopRight)) +
                              ((TempPtr[1 + Stride] * MidLeft) + (TempPtr[4 + Stride] * Pixel) + (TempPtr[7 + Stride] * MidRight)) +
                              ((TempPtr[1 + DoubleStride] * BottomLeft) + (TempPtr[4 + DoubleStride] * BottomMid) + (TempPtr[7 + DoubleStride] * BottomRight))) / Factor) + Offset);

                        if (Pix < 0) Pix = 0;
                        else if (Pix > 255) Pix = 255;

                        ptr[4 + Stride] = (byte)Pix;

                        Pix = (((((TempPtr[0] * TopLeft) + (TempPtr[3] * TopMid) + (TempPtr[6] * TopRight)) +
                              ((TempPtr[0 + Stride] * MidLeft) + (TempPtr[3 + Stride] * Pixel) + (TempPtr[6 + Stride] * MidRight)) +
                              ((TempPtr[0 + DoubleStride] * BottomLeft) + (TempPtr[3 + DoubleStride] * BottomMid) + (TempPtr[6 + DoubleStride] * BottomRight))) / Factor) + Offset);

                        if (Pix < 0) Pix = 0;
                        else if (Pix > 255) Pix = 255;

                        ptr[3 + Stride] = (byte)Pix;

                        ptr += 3;
                        TempPtr += 3;
                    }
            }

            bmp.UnlockBits(bmpData);
            TempBmp.UnlockBits(TempBmpData);
        }

        public ConvolutionMatrix Matrix { get; set; }
    }




    public partial class FiltersOther : Form
    {
        public FiltersOther()
        {
            InitializeComponent();
        }

        public Bitmap MedianFilter( Bitmap sourceBitmap,
                                            int matrixSize,
                                              int bias = 0,
                                    bool grayscale = false)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                float rgb = 0;


                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;


            int byteOffset = 0;

            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    neighbourPixels.Clear();


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {


                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                (filterY * sourceData.Stride);


                            neighbourPixels.Add(BitConverter.ToInt32(
                                             pixelBuffer, calcOffset));
                        }
                    }


                    neighbourPixels.Sort();

                    middlePixel = BitConverter.GetBytes(
                                       neighbourPixels[filterOffset]);


                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);


            resultBitmap.UnlockBits(resultData);

            resultBitmap.Save("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\median-"+matrixSize+".png");
            return resultBitmap;
        }

        public static Bitmap ImageSmooth( Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;
            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);
            for (int i = 2; i < w - 2; i++)
            {
                for (int j = 2; j < h - 2; j++)
                {
                    int p = i * 3 + j * image_data.Stride;
                    for (int k = 0; k < 3; k++)
                    {
                        List<int> vals = new List<int>();
                        for (int xkernel = -2; xkernel < 3; xkernel++)
                        {
                            for (int ykernel = -2; ykernel < 3; ykernel++)
                            {
                                int kernel_p = k + p + xkernel * 3 + ykernel * image_data.Stride;
                                vals.Add(buffer[kernel_p]);
                            }
                        }
                        result[p + k] = (byte)(vals.Sum() / vals.Count);
                    }
                }
            }
            Bitmap res_img = new Bitmap(w, h);
            BitmapData res_data = res_img.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);
            Marshal.Copy(result, 0, res_data.Scan0, bytes);
            res_img.UnlockBits(res_data);

            res_img.Save("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\smooth.png");
            return res_img;
        }

        public static void ApplyGaussianBlur(ref Bitmap bmp, int Weight)
        {
            ConvolutionMatrix m = new ConvolutionMatrix();
            m.Apply(1);
            m.Pixel = Weight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = Weight + 12;

            Convolution C = new Convolution();
            C.Matrix = m;
            C.Convolution3x3(ref bmp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\andrei.jpg");
            Bitmap bmp2 = new Bitmap("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\andrei2.jpg");
            //MedianFilter(bmp,11);
            //ImageSmooth(bmp);
            ApplyGaussianBlur(ref bmp2, 6);
        }
    }
}
