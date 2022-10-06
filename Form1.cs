using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;
using System.IO;
using System.Threading;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace Facial_Recognition_App
{
    public partial class Form1 : Form
    {
        //color
        /* 
          45; 86; 100
          35; 69; 79
         */
        #region Variables
        private Capture videoCapture = null;
        private Image<Bgr, byte> currentFrame = null;
        Mat frame = new Mat();
        private bool faceDetectionEnabled = false;

        private bool maskDetectionEnable = false;
        CascadeClassifier faceCascadeClasifier = new CascadeClassifier(@"D:\Dot Net Workspace\Facial Recognition App\haarcascade_frontalface_alt.xml");
      // CascadeClassifier faceCascadeClasifier = new CascadeClassifier(@"D:\Dot Net Workspace\Facial Recognition App\cascade9.xml");

        CascadeClassifier faceCascadeClasifierMask = new CascadeClassifier(@"D:\Dot Net Workspace\Facial Recognition App\cascade9.xml");//7

        private bool enableSaveImage = false;
        private static bool isTrained = false;

        List<Image<Gray, Byte>> TrainedFaces = new List<Image<Gray, byte>>();
        List<int> PersonsLabes = new List<int>();
        List<string> PersonsNames = new List<string>();


        EigenFaceRecognizer recognizer;

     
        public static string name = "";
        string ora = DateTime.Now.ToLongTimeString();
        string data = DateTime.Today.ToString("D");
        public static string ImagePath = "";
        public List<String> PersonsPdf = new List<string>();

        string path;
        string email;

        #endregion

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            
        }

        
            
        private void btnCapture_Click(object sender, EventArgs e)
        {
            videoCapture = new Capture();
            videoCapture.ImageGrabbed += ProcessFrame;
            videoCapture.Start();
        }

        #region preprocesare
      


        #endregion

        private void ProcessFrame(object sender, EventArgs e)
        {
            // Step 1: Video capture
            videoCapture.Retrieve(frame, 0);
            currentFrame = frame.ToImage<Bgr, Byte>().Resize(picCapture.Width, picCapture.Height, Inter.Cubic);

            // Step 2: Face detection
            if(faceDetectionEnabled)
            {
                // Convert from Bgr to Gray image
                Mat grayImage = new Mat();
                /*Bitmap bImg = 
                    ((Bitmap)Filters.grayFilter(currentFrame.ToBitmap()));*///Filters.contrast_function((Bitmap)Filters.grayFilter(currentFrame.ToBitmap()));
               // bImg.Save("D:\\Dot Net Workspace\\Facial Recognition App\\Images\\ceva5.jpg");
              //  Image<Bgr, byte> imageCV = new Image<Bgr, byte>(bImg); //Image Class from Emgu.CV
              //  Mat iMat = imageCV.Mat;
                CvInvoke.CvtColor(/*imageCV*/currentFrame, grayImage, ColorConversion.Bgr2Gray);
                
                
                // Get better resolution of the image
                CvInvoke.EqualizeHist(grayImage, grayImage);

                System.Drawing.Rectangle[] faces = faceCascadeClasifier.DetectMultiScale(grayImage, 1.1, 3, Size.Empty, Size.Empty);

                if (faces.Length > 0) 
                {
                    // Draw a rectangle around each detected face
                    foreach (var face in faces)
                    {
                        CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Blue).MCvScalar, 2);

                        //Step 3: Add person
                        //Assign the face to the picture Box face picDetected
                        Image<Bgr, Byte> resultImage = currentFrame.Convert<Bgr, Byte>();
                        resultImage.ROI = face;
                       // picDetected.SizeMode = PictureBoxSizeMode.StretchImage;
                       // picDetected.Image = resultImage.Bitmap;

                        if (enableSaveImage)
                        {
                            //We will create a directory if does not exists!
                            string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            //we will save 10 images with delay a second for each image 
                            //to avoid hang GUI we will create a new task
                            Task.Factory.StartNew(() =>
                            {
                                for (int i = 0; i < 10; i++)//10
                                {
                                    //resize the image then saving it
                                    resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" + txtPersonName.Text + "_" + DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpg");
                                    Thread.Sleep(1000);
                                }
                                //resultImage.Resize(200, 200, Inter.Cubic).Save("D:\\Dot Net Workspace\\Facial Recognition App\\Images" + @"\" + txtPersonName.Text + "_" + DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".png");
                            });
                           

                        }
                        enableSaveImage = false;

                        System.Drawing.Rectangle[] facesMask = faceCascadeClasifierMask.DetectMultiScale(grayImage, 1.1, 3, Size.Empty, Size.Empty);

                        if (maskDetectionEnable)
                        {
                            
                           // System.Drawing.Rectangle[] facesMask = faceCascadeClasifierMask.DetectMultiScale(grayImage, 1.3, 3, Size.Empty, Size.Empty);
                            if (facesMask.Length > 0)
                            {
                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.White).MCvScalar, 2);
                                MessageBox.Show("Persoana detectata poarta masca.");
                               
                            }
                            else
                            {
                                MessageBox.Show("Persoana detectata nu poarta masca.");
                            }

                            facesMask =  new System.Drawing.Rectangle[0];

                        }
                        maskDetectionEnable = false;

                        //de aici am adaugat 02.05.22
                        // Step 5: Recognize the face 
                        if (isTrained)
                        {
                            Image<Gray, Byte> grayFaceResult = resultImage.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);//200-200
                            CvInvoke.EqualizeHist(grayFaceResult, grayFaceResult);
                            var result = recognizer.Predict(grayFaceResult);

                            
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBox1.Image = grayFaceResult.Bitmap;
                            pictureBox2.Image = TrainedFaces[result.Label].Bitmap;
                            Debug.WriteLine(result.Label + ". " + result.Distance);

                            //Here results found known faces

                            if (result.Label != -1 && result.Distance < 15000)     //2000. eu am pus 9000 si merge mai bine, era15000
                            {
                                CvInvoke.PutText(currentFrame, PersonsNames[result.Label], new Point(face.X - 2, face.Y - 2),
                                    FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);

                                //persoana identificata
                                personsLabel.Invoke(new Action(() =>
                                {
                                    personsLabel.Text = PersonsNames[result.Label];
                                }));
                                
                                name = PersonsNames[result.Label];

                               if(! PersonsPdf.Contains(PersonsNames[result.Label]))
                               {
                                    //saveImage();
                                    PersonsPdf.Add(PersonsNames[result.Label]);

                                    //toate persoanele identificate
                                    resultLabel.Invoke(new Action(() =>
                                    {
                                        if (resultLabel.Text.Equals("-"))
                                            resultLabel.Text = "";
                                        if (resultLabel.Text != "")
                                            resultLabel.Text = resultLabel.Text + ", ";
                                        resultLabel.Text = resultLabel.Text + PersonsNames[result.Label];
                                    }));

                                    string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";
                                    SqlConnection con = new SqlConnection(str);
                                    SqlCommand cmd = new SqlCommand();

                                    con.Open();
                                    cmd.CommandText = "insert into istoric (nume, dataDetectie) values ('"+ name+"', '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "');";
                                   
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();

                                    con.Close();

                                    saveImage();
                                    pdfGenerator();
                                    sendMail(path);
                                    
                                }

                               button2.Invoke(new Action(() =>
                               {
                                   button2.Enabled = true;
                                   
                               }));

                                
                                Debug.WriteLine(PersonsNames[result.Label]);
                             
                            }
                            //here results did not found any know faces
                            /*else
                            {
                                CvInvoke.PutText(currentFrame, "Unknown", new Point(face.X - 2, face.Y - 2),
                                    FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);

                            }*/

                        }
                    }
                }

            }

            //Render the video capture into the PictureBox
            picCapture.Image = currentFrame.Bitmap;
           

        }

        //pdf generator

        private void pdfGenerator()
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 100, 100, 75, 75);
            path = (string)(name + DateTime.Now.ToString("d_MM_yyyy")+"_"+ DateTime.Now.ToString("HH-mm-ss") + ".pdf");
            FileStream fileStream = new FileStream(path, FileMode.Create);
            PdfWriter.GetInstance(doc, fileStream);

            doc.Open();
            doc.Add(new Paragraph("         APLICATIE RECUNOASTERE FACIALA"));
            doc.Add(new Paragraph("         "));
            doc.Add(new Paragraph(""+data + " " + ora));
            doc.Add(new Paragraph("Persoana detectata este: " + name));
            doc.Add(new Paragraph("         "));
            
            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader r;
            cmd.CommandText = "select * from profil where nume='" + Form1.name + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            r = cmd.ExecuteReader();

            while (r.Read())
            {
                doc.Add(new Paragraph("Email: " + r.GetString(2)));
                email = r.GetString(2);
                doc.Add(new Paragraph("Telefon: " + r.GetString(3)));
                doc.Add(new Paragraph("Adresa: " + r.GetString(4)));
                doc.Add(new Paragraph("Observatii: " + r.GetString(5)));

            }
            r.Close();
            //con.Close();
            doc.Add(new Paragraph("         "));
            doc.Add(new Paragraph(" Agenda de astazi:"));
            
            //din a 2- a db
            SqlDataReader rdr;
            //data nu e buna!!!!!!
            DateTime now = DateTime.Now;
            string nowDay = now.Day.ToString();
            string nowMonth = now.Month.ToString();
            string nowYear = now.Year.ToString();
            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");
           // string dateD = DateTime.Now.ToString("d");
            cmd.CommandText = "select eventC from calendar where nume='" + Form1.name + "'and dataC= '"  + nowDay + "/" + nowMonth + "/" + nowYear + "'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            int i = 1;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                doc.Add(new Paragraph(i.ToString()+") "+rdr.GetString(0)));
                i++;
            }

            con.Close();

            ///adaugare imagine
            doc.Add(new Paragraph("         "));
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ImagePath);
            jpg.ScaleToFit(450f, 450f);
            doc.Add(jpg);
        
            //inchidere document
            doc.Close();
        }
        /// <summary>
        /// send email
        /// </summary>
        /// <param name="attachPath"></param>
        private void sendMail(string attachPath)
        {
            NetworkCredential login;
            SmtpClient client;
            MailMessage msg;

            string pass = "dnpdiglisflscvir";
            string user = "andreiandrei2498";
            string smtpString = "smtp.gmail.com";

            login = new NetworkCredential(user, pass);
            client = new SmtpClient(smtpString);
            client.Port = 587;//Convert.ToInt32(txtPort.Text);
            client.EnableSsl = true;//chkSSL.Checked;
            client.Credentials = login;
            msg = new MailMessage { From = new MailAddress(user + smtpString.Replace("smtp.", "@"), "Planificare activitati", Encoding.UTF8) };
            //aici sa iau din bd
            msg.To.Add(new MailAddress(email));// "andreibaragau@yahoo.com"));
           // if (!string.IsNullOrEmpty(txtCC.Text))
           //     msg.To.Add(new MailAddress(txtCC.Text));

            if (!string.IsNullOrEmpty(attachPath))
                msg.Attachments.Add(new Attachment(attachPath));

            msg.Subject = "Raport aplicatie recunoastere faciala";
            msg.Body = "Planificarea activitatilor de astazi. ";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string userState = "Sending...";
            client.SendAsync(msg, userState);
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            //14-21
            if (e.Cancelled)
                MessageBox.Show("Nu s-a trimis mesajul!");
            if (e.Error != null)
                MessageBox.Show("eroare!" + e.Error);
            else
                MessageBox.Show("Mesaj trimis!");
        }
        //salvare imagine
        private void saveImage()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            if (InvokeRequired)
            {
                // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate
                {

                    DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
                }));
                
            }
            string data = DateTime.Now.ToString("M_d_yyyy");
            ImagePath = data + ".jpeg";

            bitmap.Save(ImagePath, ImageFormat.Jpeg);
        }

        //12:31
        private void btnDetectFaces_Click(object sender, EventArgs e)
        {
            faceDetectionEnabled = true;
        }

       //20:00
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnAddPerson.Enabled = false;
            enableSaveImage = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnAddPerson.Enabled = true;
            enableSaveImage = false;
        }

        /// <summary>
        /// Step 4: Train images, we will use save images from directory
        /// </summary>
        private bool TrainImagesFromDir()
        {
            int ImagesCount = 0;
            //33500
            double Threshold = 33500;//2000   --2500 parca e mai bine  --13500 si mai bine

            try
            {
                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200, 200, Inter.Cubic);
                    CvInvoke.EqualizeHist(trainedImage, trainedImage);
                    TrainedFaces.Add(trainedImage);
                    PersonsLabes.Add(ImagesCount);
                    //aici erau comentate
                    string name = file.Split('\\').Last().Split('_')[0];
                    PersonsNames.Add(name);
                    ImagesCount++;
                    Debug.WriteLine(ImagesCount + ". " + name);
                }

                if (TrainedFaces.Count() > 0)
                {
                    recognizer = new EigenFaceRecognizer(ImagesCount, Threshold);
                    recognizer.Train(TrainedFaces.ToArray(), PersonsLabes.ToArray());

                    isTrained = true;
                    Debug.WriteLine(ImagesCount);
                    Debug.WriteLine(isTrained);
                    return true;
                }
                else
                {
                    isTrained = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in training images" + ex.Message);
                return isTrained = false;
               
            }


        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            //check first if trainImage folder exist
            string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Nu este salvata nici o persoana!");
            }
            else
            {
                TrainImagesFromDir();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 formMail = new Form3();
            formMail.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 formP = new Form4();
            formP.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormIstoric formIstoric = new FormIstoric();
            formIstoric.Show();          
        }

        private void btnMaskDetect_Click(object sender, EventArgs e)
        {
            maskDetectionEnable = true;
        }
    }

}
