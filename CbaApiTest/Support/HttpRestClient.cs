using Newtonsoft.Json;
using RestSharp;

namespace CbaApiTest.Support
{
    public class HttpRestClient
    {
        public static RestResponse ExecuteRequest
        (
            Method method,
            string endpoint,
            object? bodydata = null,
            IDictionary<string, string>? headers = null,
            IDictionary<string, string>? queryParams = null,
            IDictionary<string, string>? parameters = null,
            IDictionary<string, string>? formData = null
            )

        {
            var client = new RestClient();
            var restRequest = new RestRequest(endpoint, method) { RequestFormat = DataFormat.Json };

            if (bodydata is not null)
            {
                restRequest.AddJsonBody(JsonConvert.SerializeObject(bodydata));
                restRequest.AddHeader("Content-Type", "application/json");
            }

            if (headers != null)
            {
                restRequest.AddHeaders(headers);
            }

            if (queryParams != null)
            {
                foreach (var kvp in queryParams)
                {
                    restRequest.AddQueryParameter(kvp.Key, kvp.Value);
                }
            }

            if (formData != null)
            {
                
                foreach (var kvp in formData)
                {
                    restRequest.AlwaysMultipartFormData = true;
                    if (kvp.Key == "file")
                    {
                       
                        restRequest.AddFile(kvp.Key, kvp.Value);
                    }
                    restRequest.AddParameter(kvp.Key, kvp.Value);
                }
            }

            if (parameters != null)
            {
                foreach (var kvp in parameters)
                {
                    restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    restRequest.AddParameter(kvp.Key, kvp.Value);
                }

            }

            return client.Execute(restRequest);
        }
    }
}
