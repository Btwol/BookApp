using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services
{
    public class HelperService
    {
        public async static Task<ServiceResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadFromJsonAsync<ServiceResponse>();
            if (content.SuccessStatus)
            {
                return (ServiceResponse<T>)content;
            }
            else
            {
                throw new Exception(content.Message);
            }
        }
    }
}
