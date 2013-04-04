using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace _1202W13As2_DeCaireRobert
{
    public class DeCaire_Airport_API
    {
        // This class uses the RestSharp library (restsharp.org) to request and catch RESTful API data
        // from Airport Code at airportcode.riobard.com
        
        const string BaseUrl = "http://airportcode.riobard.com/airport/";
        
        public DeCaire_Airport_API()
        {
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Data;
        }

        public Call GetCall(string airportCode)
        {
            string airCode = airportCode;
            var request = new RestRequest();
            request.Resource = "{AirCode}?fmt=JSON";
            request.AddParameter("AirCode", airCode, ParameterType.UrlSegment);
            //request.RootElement = "Call";

            //request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

            return Execute<Call>(request);
        }

    }

    public class Call
    {
        public string code { get; set; }
        public string name { get; set; }
        public string location { get; set; }
    }


}
