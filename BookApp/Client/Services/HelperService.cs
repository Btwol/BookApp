using BookApp.Shared.Models.Services;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class HelperService
    {
        public static async Task<T> HandleResponse<T>(HttpResponseMessage response) where T : class
        {
            if (!response.IsSuccessStatusCode)
            {
                await TriggerServiceResponseError(response);
                return null;
            }
            else return await ReadServiceResponse<T>(response);
        }

        public static async Task<ServiceResponse> HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await TriggerServiceResponseError(response);
                return null;
            }
            else return await ReadServiceResponse(response);
        }

        public static async Task<T> ReadServiceResponse<T>(HttpResponseMessage response) where T : class
        {
            if(!response.IsSuccessStatusCode)
            {
                var responseErrorMessage = (await response.Content.ReadFromJsonAsync<ServiceResponse>()).Message;
                throw new Exception(responseErrorMessage);
            }
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
