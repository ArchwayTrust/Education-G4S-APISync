using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using System.Web;

namespace G4SApiSync.Client
{
    class APIRequest<EndPoint, DTO> : IAPIRequest<EndPoint, DTO>
        where EndPoint : IEndPoint<DTO>
    {
        private string pResource;
        private string pBearer;
        private string pAcYear;

        public APIRequest(string EndPointURL, string Bearer, string AcademicYear)
        {
            pResource = EndPointURL;
            pBearer = Bearer;
            pAcYear = AcademicYear;
        }

        public string ReturnedJSON(int? cursor)
        {
            //Use RestSharp to query G4S API
            var client = new RestClient("https://api.go4schools.com");

            string fullResource;

            if (cursor == null)
            {
                fullResource = pResource;
            }
            else
            {
                fullResource = pResource + "?cursor=" + HttpUtility.UrlEncode(cursor.ToString());
            }

            var request = new RestRequest(fullResource, Method.GET);
            request.AddHeader("Authorization", "Bearer " + pBearer);
            request.AddParameter("academicYear", pAcYear);

            var response = client.Execute(request);
            string content = response.Content; // Returned JSON as string.
            return content;
        }

        public List<DTO> ToList()
        {
            List<DTO> listToReturn = new List<DTO>();

            try
            {
                EndPoint obj = JsonConvert.DeserializeObject<EndPoint>(ReturnedJSON(null));
                listToReturn.AddRange(obj.DTOs);

                    while (obj.HasMore)
                    {
                        int? cursor = obj.Cursor;
                        obj = JsonConvert.DeserializeObject<EndPoint>(ReturnedJSON(cursor));
                        listToReturn.AddRange(obj.DTOs);
                    }

            }

            catch
            {
                try
                {
                    List<DTO> obj = JsonConvert.DeserializeObject<List<DTO>>(ReturnedJSON(null));
                    listToReturn = obj;
                }
                catch
                {
                    DTO obj = JsonConvert.DeserializeObject<DTO>(ReturnedJSON(null));
                    listToReturn.Add(obj);
                }

            }

            return listToReturn;

        }
    }
}
