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
    public partial class FormIstoric : Form
    {
        public FormIstoric()
        {
            InitializeComponent();
        }

        private void btnAfisare_Click(object sender, EventArgs e)
        {
            string str = "Server= localhost; Database= andreidb;Integrated Security = SSPI; ";           
            SqlConnection con = new SqlConnection(str);

           
           //tabelul 
            string querry = "select nume, dataDetectie from istoric order by dataDetectie desc";
            
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(querry, con);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds, "istoric");
            dataGridView1.DataSource = ds.Tables["istoric"].DefaultView;

            //graficul

            string queryChart = "select format(dataDetectie, 'yyyy-MM') as Data_Calendaristica, count( convert(date, dataDetectie)) as Numar from istoric group by format(dataDetectie, 'yyyy-MM'); ";
            SqlCommand cmd = new SqlCommand(queryChart, con);

            SqlDataAdapter SDA = new SqlDataAdapter(queryChart, con);
            DataTable dt = new DataTable();
            SDA.Fill(dt);

            chart1.Series["Numar_detectari"].XValueMember = "Data_Calendaristica";
            chart1.Series["Numar_detectari"].YValueMembers = "Numar";
            chart1.DataSource = dt;
            chart1.DataBind();
        
            con.Close();
        }
    }
}
