using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facial_Recognition_App
{
    public partial class FormCalendar : Form
    {
        int month, year;
        public static int static_month, static_year;
        public FormCalendar()
        {
            InitializeComponent();
        }

        private void FormCalendar_Load(object sender, EventArgs e)
        {
            displayDays();
        }

        private void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            static_month = month;
            static_year = year;

            DateTime startOfMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayOfTheWeek = Convert.ToInt32(startOfMonth.DayOfWeek.ToString("d"))+1;

            for (int i = 1; i < dayOfTheWeek; i++)
            {
                UserControlBlank userControlBlank = new UserControlBlank();
                daycontainer.Controls.Add(userControlBlank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays userControlDays = new UserControlDays();
                userControlDays.days(i);
                daycontainer.Controls.Add(userControlDays);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            month--;
            static_month = month;
            static_year = year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;

            DateTime startOfMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayOfTheWeek = Convert.ToInt32(startOfMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayOfTheWeek; i++)
            {
                UserControlBlank userControlBlank = new UserControlBlank();
                daycontainer.Controls.Add(userControlBlank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays userControlDays = new UserControlDays();
                userControlDays.days(i);
                daycontainer.Controls.Add(userControlDays);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            month++;
            static_month = month;
            static_year = year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;

            DateTime startOfMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayOfTheWeek = Convert.ToInt32(startOfMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < dayOfTheWeek; i++)
            {
                UserControlBlank userControlBlank = new UserControlBlank();
                daycontainer.Controls.Add(userControlBlank);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDays userControlDays = new UserControlDays();
                userControlDays.days(i);
                daycontainer.Controls.Add(userControlDays);
            }
        }
    }
}
