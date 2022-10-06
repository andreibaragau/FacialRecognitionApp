using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Facial_Recognition_App
{
    public partial class Form4 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=facerecogprofil;Uid=root;Pwd=;SslMode=none;");
        public Form4()
        {
            InitializeComponent();
            pictureBox1.ImageLocation = @"D:\Dot Net Workspace\Facial Recognition App\Backgrounds2.jpg";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = Form1.name;
            pictureBoxPerson.ImageLocation = @"D:\Dot Net Workspace\Facial Recognition App\Images\"+ Form1.name +".jpg";
            pictureBoxPerson.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            
            pictureBoxPersonBig.ImageLocation = @"D:\Dot Net Workspace\Facial Recognition App\Images\" + Form1.name + ".jpg";
            pictureBoxPersonBig.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBoxEdit.ImageLocation = @"D:\Dot Net Workspace\Facial Recognition App\edit2.png";
            pictureBoxEdit.SizeMode = PictureBoxSizeMode.StretchImage;

            string n = Form1.name;
            //mySql
            /*
            conn.Open();
            string query = "select * from profil where nume='"+ Form1.name +"'";//"select nume from profil";// where nume='"+ Form1.name+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                txtBoxNume.Text = rdr.GetString(1);
                txtBoxEmail.Text = rdr.GetString(2);
                txtBoxTelefon.Text = rdr.GetString(3);
                txtBoxAdresa.Text = rdr.GetString(4);
                txtBoxObservatii.Text = rdr.GetString(5);
            }
           
            conn.Close();
            */
            //sql
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
                txtBoxNume.Text = r.GetString(1);
                txtBoxEmail.Text = r.GetString(2);
                txtBoxTelefon.Text = r.GetString(3);
                txtBoxAdresa.Text = r.GetString(4);
                txtBoxObservatii.Text = r.GetString(5);
            }
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //mysql
            /* conn.Open();

             string query = "update profil set email='" + txtBoxEmail.Text + "', telefon='"+ txtBoxTelefon.Text + "', adresa='"+ txtBoxAdresa.Text + "', observatii='"+ txtBoxObservatii.Text + "' where nume ='"+Form1.name+"'";
             MySqlCommand cmd = new MySqlCommand(query, conn);
             cmd.ExecuteNonQuery();

             conn.Close();*/

            //sql
            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";


            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();

            con.Open();

            cmd.CommandText = "update profil set email='" + txtBoxEmail.Text + "', telefon='" + txtBoxTelefon.Text + "', adresa='" + txtBoxAdresa.Text + "', observatii='" + txtBoxObservatii.Text + "' where nume ='" + Form1.name + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.ExecuteNonQuery();

            con.Close();

        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            FormCalendar formCalendar = new FormCalendar();
            formCalendar.Show();
        }
    }
}
