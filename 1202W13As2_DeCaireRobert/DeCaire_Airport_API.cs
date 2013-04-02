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
        const string BaseUrl = "http://airportcode.riobard.com/airport/";
        readonly string _airCode;
        

        public DeCaire_Airport_API (string airportCode) {
            _airCode = airportCode;   
        }

        
        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            //client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
             // used on every request
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Data;
        }

        public Call GetCall(string callSid)
        {
            var request = new RestRequest();
            request.Resource = "{AirCode}?FMT=JSON";
            request.AddParameter("AirCode", _airCode, ParameterType.UrlSegment);
            //request.RootElement = "Call";

            //request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

            return Execute<Call>(request);
        }
    
    }

    public class Call 
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
    
    
}

