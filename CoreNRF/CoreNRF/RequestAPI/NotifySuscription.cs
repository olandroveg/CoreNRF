using CoreNRF.Dtos.Notification;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreNRF.RequestAPI
{
    public class NotifySuscription
    {
        private string _NFUrl;
        public NotifySuscription(string NFaddress)
        {
            _NFUrl = NFaddress;
        }
        public async Task<string> Notify (NotifSuscripDto notification)
        {
            var responseForm = "";
            try
            {
                var dataObj = notification;
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(dataObj), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(_NFUrl, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //responseForm = JsonConvert.DeserializeObject<string>(apiResponse);
                            responseForm = apiResponse;
                            if (responseForm == "Success")
                            {
                                return responseForm;
                            }
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            responseForm = "unauthorized";

                    }
                    return responseForm;
                }
            }
            catch (Exception e)
            {
                responseForm = "exception";
                return responseForm;
            }
        }
    }
}
