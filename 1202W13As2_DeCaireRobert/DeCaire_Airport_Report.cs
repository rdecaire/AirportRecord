using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1202W13As2_DeCaireRobert
{
    public class DeCaire_Airport_Report
    {
        string airportCode, airportName, airportCity, airportState, airportCountry;
        DateTime date;
        int numArrivals, numDepartures, numPassengers;

        public string AirportCode
        {
            get
            {
                return airportCode;
            }
            set
            {
                airportCode = value;
            }
        }

        public string AirportName
        {
            get
            {
                return airportName;
            }
            set
            {
                airportName = value;
            }
        }

        public string AirportCity
        {
            get
            {
                return airportCity;
            }
            set
            {
                airportCity = value;
            }
        }

        public string AirportState
        {
            get
            {
                return airportState;
            }
            set
            {
                airportState = value;
            }
        }

        public string AirportCountry
        {
            get
            {
                return airportCountry;
            }
            set
            {
                airportCountry = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public int NumArrivals
        {
            get
            {
                return numArrivals;
            }
            set
            {
                numArrivals = value;
            }
        }

        public int NumDepartures
        {
            get
            {
                return numDepartures;
            }
            set
            {
                numDepartures = value;
            }
        }

        public int NumPassengers
        {
            get
            {
                return numPassengers;
            }
            set
            {
                numPassengers = value;
            }
        }
    }
}
