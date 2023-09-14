using BookApp.Shared.Models.Services;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class HelperService
    {
        public static async Task<T> ReadServiceResponse<T>(HttpResponseMessage response) where T : class
        {
            return (await response.Content.ReadFromJsonAsync<ServiceResponse<T>>()).Content;
        }

        public static async Task<ServiceResponse> ReadServiceResponse(HttpResponseMessage response)
        {
            return (await response.Content.ReadFromJsonAsync<ServiceResponse>());
        }

        public static async Task TriggerServiceResponseError(HttpResponseMessage response)
        {
            var responseMessage = (await response.Content.ReadFromJsonAsync<ServiceResponse>()).Message;
            throw new Exception(responseMessage);
        }
    }
}
