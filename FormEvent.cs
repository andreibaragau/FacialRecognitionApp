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

namespace Facial_Recognition_App
{
    public partial class FormEvent : Form
    {

        public FormEvent()
        {
            InitializeComponent();
        }

       

        private void FormEvent_Load(object sender, EventArgs e)
        {
            txtDate.Text = UserControlDays.static_day + "/" + FormCalendar.static_month + "/" + FormCalendar.static_year;
           
            displayActivities();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";


            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();

            con.Open();
            string numeA = Form1.name;//"andrei";//Form1.name

            cmd.CommandText = "insert into calendar(nume, dataC, eventC) values ('" + numeA + "', '" + txtDate.Text + "','" + txtEvent.Text + "')";
                //"(@nume,@dataC,@eventC)";
            cmd.CommandType = CommandType.Text;
            /*cmd.Parameters.AddWithValue("nume", Form1.name);
            cmd.Parameters.AddWithValue("dataC", txtDate.Text);
            cmd.Parameters.AddWithValue("eventC", txtEvent.Text);*/
            cmd.Connection = con;
           
            cmd.ExecuteNonQuery();
            MessageBox.Show("Eveniment salvat!");

            con.Close();

            displayActivities();
            /*
            dataGridView1.Update();
            dataGridView1.Refresh();*/


        }

        private void displayActivities()
        {
            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";
            SqlConnection con = new SqlConnection(str);

            //string querry = "select eventC from calendar where dataC="+"'"+"'";
            string querry = "select eventC from calendar where nume='" + Form1.name + "'and dataC= '" + UserControlDays.static_day + "/" + FormCalendar.static_month + "/" + FormCalendar.static_year + "'";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(querry, con);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds, "calendar");
            dataGridView1.DataSource = ds.Tables["calendar"].DefaultView;

            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            int itemIndex = dataGridView1.SelectedRows[0].Index;
            string itemToDelete = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();

            con.Open();

            cmd.CommandText = "delete from calendar where nume='" + Form1.name + "' and eventC=" + "'" + itemToDelete + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.ExecuteNonQuery();
            con.Close();

            displayActivities();                      
        }
    }
}
