using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace _1202W13As2_DeCaireRobert
{
    public partial class DeCaire_New_Record : Form
    {
        int arrivals;
        int passengers;
        DeCaire_Airport_API airport = new DeCaire_Airport_API();

        public DeCaire_New_Record()
        {
            InitializeComponent();
        }

        bool validateAirportName()
        {
            if(!String.IsNullOrEmpty(textBox2.Text))
            {
               return true;
            }
            else
            {
               MessageBox.Show("Airport name cannot be left empty.");
               return false;
            }
        }

        bool validateArrivals()
        {
           
            if (int.TryParse(textBox6.Text, out arrivals))
            {
                if (arrivals > 0)
                {
                    return true;
                }
                
            }

            MessageBox.Show("Number of arrivals must be a positive integer");
            return false;
        }

        bool validatePassengers()
        {

            if (int.TryParse(textBox8.Text, out passengers))
            {
                if (passengers > 0)
                {
                    return true;
                }
            }

            MessageBox.Show("Number of passengers must be a positive integer");
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool validAirport = validateAirportName();
            bool validArrivals = validateArrivals();
            bool validPassengers = validatePassengers();
            if (validAirport && validArrivals && validPassengers)
            {
             
            DeCaire_Airport_Report report = new DeCaire_Airport_Report();
            report.AirportCode = textBox1.Text;
            report.AirportName = textBox2.Text;
            report.AirportCity = textBox3.Text;
            report.AirportState = textBox4.Text;
            report.AirportCountry = textBox5.Text;
            report.Date = dateTimePicker1.Value;
            report.NumArrivals = int.Parse(textBox6.Text);
            report.NumDepartures = int.Parse(textBox7.Text);
            report.NumPassengers = int.Parse(textBox7.Text);
            DeCaire_Main_Menu.reportList.Add(report);
            MessageBox.Show("Report entered successfully.");
            this.Close();
            }
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 3)
            {
                Call airDetails = airport.GetCall(textBox1.Text);
                if (!String.IsNullOrEmpty(airDetails.code) && !String.IsNullOrEmpty(airDetails.location) && !String.IsNullOrEmpty(airDetails.name))
                {
                    string code = airDetails.code;
                    string airportLocation = airDetails.location;
                    string airportName = airDetails.name;
                    string[] addressWords = Regex.Split(airportLocation, ", ");
                    string city = addressWords[0];
                    string country = addressWords[addressWords.Length - 1];
                    if (addressWords.Length == 3)
                    {
                        string province = addressWords[1];
                        textBox4.Text = province;
                    }
                    else
                    {
                        textBox4.Text = "";
                    }
                    textBox2.Text = airportName;
                    textBox3.Text = city;
                    textBox5.Text = country;
                }
                 
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard this entry?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
