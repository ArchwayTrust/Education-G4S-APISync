using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using System.Web;
using System.Threading;

namespace G4SApiSync.Client
{
    class APIRequest<EndPoint, DTO> : IAPIRequest<EndPoint, DTO>
        where EndPoint : IEndPoint<DTO>
    {
        private string pResource;
        private string pBearer;
        private string pAcYear;
        private string pYearGroup;
        private string pReportId;
        private string pDate;

        public APIRequest(string EndPointURL, string Bearer, string DataSet, string YearGroup = null, string ReportId = null, DateTime? Date = null)
        {
            pResource = EndPointURL;
            pBearer = Bearer;
            pAcYear = DataSet;
            pYearGroup = YearGroup;
            pReportId = ReportId;
            if (Date != null)
            {
                pDate = Date.Value.ToString("yyyy-MM-dd");
            }
            else 
            {
                pDate = null;
            }
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

            if (pYearGroup != null)
            {
                request.AddParameter("yearGroup", pYearGroup);
            }

            if (pReportId != null)
            {
                request.AddParameter("reportId", pReportId);
            }

            if (pDate != null)
            {
                request.AddParameter("date", pDate);
            }

            var response = client.Execute(request);

            //Check if api call suceeded and throw an exception if not.
            if ((int)response.StatusCode != 200)
            {
                throw (new APICallException((int)response.StatusCode + " - " + response.StatusDescription));
            }

            string content = response.Content; // Returned JSON as string.
            return content;
        }

        public List<DTO> ToList()
        {
            List<DTO> listToReturn = new List<DTO>();
            string JSONContent;

            JSONContent = ReturnedJSON(null);

            try
            {
                EndPoint obj = JsonConvert.DeserializeObject<EndPoint>(JSONContent);
                listToReturn.AddRange(obj.DTOs);

                while (obj.HasMore)
                {
                    int? cursor = obj.Cursor;
                    JSONContent = ReturnedJSON(cursor);
                    obj = JsonConvert.DeserializeObject<EndPoint>(JSONContent);
                    listToReturn.AddRange(obj.DTOs);
                }

            }

            catch
            {
                try
                {
                    List<DTO> obj = JsonConvert.DeserializeObject<List<DTO>>(JSONContent);
                    listToReturn = obj;
                }
                catch
                {
                    DTO obj = JsonConvert.DeserializeObject<DTO>(JSONContent);
                    listToReturn.Add(obj);
                }

            }

            Thread.Sleep(200);
            return listToReturn;

        }
    }
}
