using BookApp.Client.Interfaces;
using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class AnalysisUpdaterClientService : IAnalysisUpdaterClientService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        private readonly IAppStorage _appStorage;

        public AnalysisUpdaterClientService(HttpClient http, IJSRuntime jsRuntime, IAppStorage appStorage)
        {
            _http = http;
            _jsRuntime = jsRuntime;
            _appStorage = appStorage;
        }

        public async Task UpdateAnalysisModel(IAnalysisComponent analysisComponent)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            while(true)
            {
                var analysisVersion = await _appStorage.GetAnalysisVersion();
                var response = await _http.PostAsJsonAsync<AnalysisVersionModel>($"AnalysisUpdater/UpdateAnalysisModel", analysisVersion);
                var updatedAnalysis = await HelperService.HandleResponse<UpdatedBookAnalysisModel>(response);
                if(updatedAnalysis.UpToDate)
                {
                    break;
                }

                if(updatedAnalysis.BookAnalysisSummary is not null)
                {
                    analysisComponent.BookAnalysis.Authors = updatedAnalysis.BookAnalysisSummary.Authors;
                    analysisComponent.BookAnalysis.AnalysisTitle = updatedAnalysis.BookAnalysisSummary.AnalysisTitle;
                    analysisComponent.BookAnalysis.BookTitle = updatedAnalysis.BookAnalysisSummary.BookTitle;
                    analysisComponent.BookAnalysis.Members = updatedAnalysis.BookAnalysisSummary.Members;
                    analysisComponent.BookAnalysis.AnalysisVersion.AnalysisSummaryVersion = updatedAnalysis.AnalysisVersion.AnalysisSummaryVersion;
                }

                if(updatedAnalysis.Tags is not null)
                {
                    analysisComponent.BookAnalysis.Tags = updatedAnalysis.Tags;
                    analysisComponent.BookAnalysis.AnalysisVersion.TagsVersion = updatedAnalysis.AnalysisVersion.TagsVersion;
                }

                if (updatedAnalysis.AnalysisNotes is not null)
                {
                    analysisComponent.BookAnalysis.AnalysisNotes = updatedAnalysis.AnalysisNotes;
                    analysisComponent.BookAnalysis.AnalysisVersion.AnalysisNotesVersion = updatedAnalysis.AnalysisVersion.AnalysisNotesVersion;
                }

                if (updatedAnalysis.ChapterNotes is not null)
                {
                    analysisComponent.BookAnalysis.ChapterNotes = updatedAnalysis.ChapterNotes;
                    analysisComponent.BookAnalysis.AnalysisVersion.ChapterNotesVersion = updatedAnalysis.AnalysisVersion.ChapterNotesVersion;
                }

                if (updatedAnalysis.ParagraphNotes is not null)
                {
                    analysisComponent.BookAnalysis.ParagraphNotes = updatedAnalysis.ParagraphNotes;
                    analysisComponent.BookAnalysis.AnalysisVersion.ParagraphNotesVersion = updatedAnalysis.AnalysisVersion.ParagraphNotesVersion;
                }

                if (updatedAnalysis.Highlights is not null)
                {
                    analysisComponent.BookAnalysis.Highlights = updatedAnalysis.Highlights;
                    analysisComponent.BookAnalysis.AnalysisVersion.HighlightVersion = updatedAnalysis.AnalysisVersion.HighlightVersion;
                }

                if (updatedAnalysis.HighlightNotes is not null)
                {
                    IEnumerable<int> highlightIds = analysisComponent.BookAnalysis.Highlights.Select(h => h.Id);
                    foreach(int highlightId in highlightIds)
                    {
                        analysisComponent.BookAnalysis.Highlights.FirstOrDefault(h => h.Id == highlightId).HighlightNotes 
                            = updatedAnalysis.HighlightNotes.Where(n => n.HighlightId == highlightId).ToList();
                    }
                    analysisComponent.BookAnalysis.AnalysisVersion.HighlightNotesVersion = updatedAnalysis.AnalysisVersion.HighlightNotesVersion;
                }
            }
        }
    }
}
