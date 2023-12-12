using BookApp.Client.Models;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Identity;

namespace BookApp.Client.Services.Interfaces
{
    public interface IAppStorage
    {
        public Task<BookAnalysisDetailedModel?> GetStoredBookAnalysis(int bookAnalysisId);
        public Task StoreBookAnalysis(BookAnalysisDetailedModel bookAnalysis, bool userHasEditorRights);
        public Task<bool> AnalysisIsStored();
        public Task<string> GetStoredBookAnalysisId();
        public Task<AnalysisVersionModel> GetAnalysisVersion();
        public Task<bool> UserHasStoredAnalysisEditorialRights();
        public Task StoreBook(byte[] bookArray, string bookHash);
        public Task<byte[]> GetStoredBook();
        public Task<string> GetStoredBookHash();
        public Task StoreUser(LoginResponse loginResponse);
        public Task DeleteReaderPosition();
        public Task DeleteBookFromStorage();
        public Task DeleteAnalysisFromStorage();
        public Task DeleteUserFromStorage();
        public Task<bool> BookIsStored();
        public Task<string> GetUserToken();
        public Task<bool> UserIsStored();
        public Task<AppUserModel> GetStoredUser();
        public Task SetReaderPosition(ReaderPosition readerPosition);
        public Task<ReaderPosition> GetLastReaderPosition();
        public Task UpdateStoredBookAnalysis(BookAnalysisDetailedModel bookAnalysis);
    }
}
