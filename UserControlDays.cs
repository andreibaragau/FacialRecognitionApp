using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Facial_Recognition_App
{
    public partial class UserControlDays : UserControl
    {
        public static string static_day;
        public UserControlDays()
        {
            InitializeComponent();
            
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {
           display();
        }

        public void days(int numDay)
        {
            dayLabel.Text = numDay + "";
        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            static_day = dayLabel.Text;
            timer1.Start();
            FormEvent formEvent = new FormEvent();
            formEvent.Show();
        }

        private void display()
        {
            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";


            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();

            con.Open();
            string numeA = Form1.name;//"andrei";//Form1.name

            SqlDataReader r;
            cmd.CommandText = "select eventC from calendar where nume='"+numeA + "'and dataC= '" +dayLabel.Text+"/"+FormCalendar.static_month+"/"+FormCalendar.static_year+"'";
            
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            r = cmd.ExecuteReader();
            while (r.Read())
            {
               // eventLabel.Text = r.GetString(0)+"\n";
                
                if (eventLabel.Text.Length>0)
                {
                    eventLabel.Text = eventLabel.Text+"\n" + r.GetString(0);
                    timer1.Stop();
                }
                else
                {
                    eventLabel.Text = r.GetString(0);
                }
            }
           
            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            display();
        }
    }
}
