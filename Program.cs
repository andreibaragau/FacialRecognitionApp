using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facial_Recognition_App
{
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            var fileName= "D:\\Dot Net Workspace\\Facial Recognition App\\Images\\gray.png";
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} does not exist");
                return;
            }

            var image = Image.FromFile(fileName);
            var blur = new GaussianBlur(image as Bitmap);

            var sw = Stopwatch.StartNew();
            var result = blur.Process(10);
            Console.WriteLine($"Finished in: {sw.ElapsedMilliseconds}ms");
            result.Save("blur-gri2.jpg", ImageFormat.Jpeg);
            
            //https://stackoverflow.com/questions/903632/sharpen-on-a-bitmap-using-c-sharp
            var fileName2 = "D:\\Dot Net Workspace\\Facial Recognition App\\Images\\andrei.jpg";
            var image2 = Image.FromFile(fileName2);
            Bitmap bitmap = new Bitmap(image2);
            Filters filters = new Filters();
            */
            //Filters.sharpen(bitmap);
            Application.Run(new Form1());
        }
    }
}
