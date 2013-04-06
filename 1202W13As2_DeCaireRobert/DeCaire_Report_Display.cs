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
//using System.Collections.Specialized;

namespace _1202W13As2_DeCaireRobert
{
    public partial class DeCaire_Report_Display : Form
    {
        public DeCaire_Report_Display(List<DeCaire_Airport_Report> reportList)
        {
            InitializeComponent();
            displayReport(collateReport(reportList));
        }
        
        private Dictionary<string, Dictionary<string, object>> collateReport(List<DeCaire_Airport_Report> reportList)
        {
            
            Dictionary<string, Dictionary<string, object>> airCodeDict = new Dictionary<string,Dictionary<string,object>>();
            foreach (DeCaire_Airport_Report report in reportList)
            {
                // create a dictionary of airport codes
                bool isAlreadyInDict = airCodeDict.ContainsKey(report.AirportCode);
                if (!isAlreadyInDict)
                {
                    //Add the key to the dictionary if it isn't there already
                    //As the value of that key, set a second dictionary
                    Dictionary<string, object> subDict = new Dictionary<string, object>();
                    airCodeDict[report.AirportCode] = subDict;
                    //Add the airport information to the dictionary
                    subDict.Add("airportName", report.AirportName);
                    subDict.Add("airportCity", report.AirportCity);
                    subDict.Add("airportState", report.AirportState);
                    subDict.Add("airportCountry", report.AirportCountry);

                    //Add the date and number of flights
                    subDict.Add("highestFlightDate", report.Date);
                    subDict.Add("highestFlightCount", (report.NumArrivals + report.NumDepartures));
                    //Add the date and number of passengers
                    subDict.Add("highestPassengerDate", report.Date);
                    subDict.Add("highestPassengerCount", report.NumPassengers);
                    //Add a list to store the number of passengers on each day, to average later
                    List<int> passengerNumList = new List<int> { report.NumPassengers };
                    subDict.Add("passengerNumList", passengerNumList);
                }
                else
                {
                    //The airport code is already in the dictionary
                    //So let's check to see if the current report had more flights than the last
                    //one, and update it if it did.

                    int highestFlightCount = (int)airCodeDict[report.AirportCode]["highestFlightCount"];
                    if ((report.NumArrivals + report.NumDepartures) > highestFlightCount)
                    {
                        airCodeDict[report.AirportCode]["highestFlightDate"] = report.Date;
                        airCodeDict[report.AirportCode]["highestFlightCount"] = (report.NumArrivals + report.NumDepartures);
                    }

                    // And check whether this report had the highest number of passengers so far,
                    // and update it if it did.

                    int highestPassengerCount = (int)airCodeDict[report.AirportCode]["highestPassengerCount"];
                    if (report.NumPassengers > highestPassengerCount)
                    {
                        airCodeDict[report.AirportCode]["highestPassengerDate"] = report.Date;
                        airCodeDict[report.AirportCode]["highestPassengerCount"] = report.NumPassengers;
                    }

                    // And let's add the number of passengers in this report to the passenger count array.
                    // This is tricky, because we stored the list as an object, so we can't call its List<T>
                    // methods directly.  But we can make a new list that's equal to the old one (by casting
                    // the object), and then set the list again.
                    List<int> passengerNumList = (List<int>)airCodeDict[report.AirportCode]["passengerNumList"];
                    passengerNumList.Add(report.NumPassengers);
                    airCodeDict[report.AirportCode]["passengerNumList"] = passengerNumList;
                }
                
                
            }
            // So by now I have a dictionary, by airport code, containing the largest number of flights and
            // the date of that report, the largest number of passengers and the date of that report, and the
            // list of passenger counts.  Let's average those counts now for each airport.
            foreach (DeCaire_Airport_Report report in reportList)
            {
                List<int> numPassengers = (List<int>)airCodeDict[report.AirportCode]["passengerNumList"];
                airCodeDict[report.AirportCode]["averagePassengers"] = numPassengers.Average();
            }

            return airCodeDict;
        }

        private void displayReport(Dictionary<string, Dictionary<string,object>> airCodeDict)
        {
            var codeList = airCodeDict.Keys.ToList();
            codeList.Sort();
            string code=null, airportLocation=null, airportName=null, city=null, country=null, province=null;
            string[] addressWords;
            DeCaire_Airport_API airport = new DeCaire_Airport_API();
            string reportContainer = "Airport Report\n\n**********\n";
            
            foreach (string airportCode in codeList)
            {
                Call airDetails = airport.GetCall(airportCode);
                if (!String.IsNullOrEmpty(airDetails.code) && !String.IsNullOrEmpty(airDetails.location) && !String.IsNullOrEmpty(airDetails.name))
                {
                    code = airDetails.code;
                    airportLocation = airDetails.location;
                    airportName = airDetails.name;
                    addressWords = Regex.Split(airportLocation, ", ");
                    city = addressWords[0];
                    country = addressWords[addressWords.Length - 1];
                    if (addressWords.Length == 3)
                    {
                        province = addressWords[1];

                    }
                    else
                    {
                        province = "";
                    }

                }
                else
                {
                    code = airportCode;
                    airportName = (string)airCodeDict[code]["airportName"];
                    city = (string)airCodeDict[code]["airportCity"];
                    country = (string)airCodeDict[code]["airportCountry"];
                    province = (string)airCodeDict[code]["airportState"];
                }
            
                //build into reportcontainer here...
                reportContainer += ("Airport: " + code + "\nLocation: " + city + ", ");
                if (province != null)
                {
                    reportContainer += (province + ", ");
                }
            
                reportContainer += (country + "/n" +
                    "Maximum number of flights in one day: " + ((int)airCodeDict[code]["highestFlightCount"]).ToString() + "\n" +
                    "Date of maximum flights: " + ((DateTime)airCodeDict[code]["highestFlightDate"]).ToString("MMMM dd, yyyy") + "\n");
            
                reportContainer += ("Maximum number of passengers in one day: " + ((int)airCodeDict[code]["highestPassengerCount"]).ToString() + "\n" +
                    "Date of maximum passengers: " + ((DateTime)airCodeDict[code]["highestPassengerDate"]).ToString("MMMM dd, yyyy") + "\n");
                double averagePassengers = (double)airCodeDict[code]["averagePassengers"];
                reportContainer += ("Average number of passengers each day: " + averagePassengers.ToString());
            }
            textBox1.Text = reportContainer;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeCaire_Report_Display_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the report?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    
    }


}
