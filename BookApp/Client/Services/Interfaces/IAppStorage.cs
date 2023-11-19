using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Identity;

namespace BookApp.Client.Services.Interfaces
{
    public interface IAppStorage
    {
        public Task<BookAnalysisDetailedModel> GetStoredBookAnalysis();
        public Task StoreBookAnalysis(BookAnalysisDetailedModel bookAnalysis);
        public Task<bool> AnalysisIsStored();
        public Task StoreBook(byte[] bookArray);
        public Task<byte[]> GetStoredBook();
        public Task StoreUser(LoginResponse loginResponse);
        public Task DeleteBookFromStorage();
        public Task DeleteAnalysisFromStorage();
        public Task DeleteUserFromStorage();
        public Task<string> GetUserToken();
        public Task<bool> UserIsStored();
        public Task<AppUserModel> GetStoredUser();
    }
}
