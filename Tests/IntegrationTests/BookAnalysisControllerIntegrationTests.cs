using BookApp.Shared.Models.ClientModels;
using System.Net.Http.Json;

namespace Tests.IntegrationTests
{
    //[Trait("BookAnalysis", "Integration")]
    public class BookAnalysisControllerIntegrationTests : ControllerIntegrationTests
    {
        public BookAnalysisControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestGetAnalysisById()
        {
            // Arrange
            var newAnalysisModel = new BookAnalysisSummaryModel { AnalysisTitle = "TestAnalysis", Authors = new List<string> { "TestAuthor1", "TestAuthor2" }, BookHash = "123", BookTitle = "TestBook", };
            var createUrl = "/BookAnalysis/CreateBookAnalysis";

            var createResponse = await DeserializeResponse<ServiceResponse<BookAnalysisSummaryModel>>(
                await _HttpClient.PostAsJsonAsync(createUrl, newAnalysisModel)
            );

            AssertResponseSuccess(createResponse);
            var createdAnalysisId = createResponse.Content.Id;

            // Act
            var getUrl = $"/BookAnalysis/GetAnalysisById/{createdAnalysisId}";
            var getResponse = await DeserializeResponse<ServiceResponse<BookAnalysisDetailedModel>>(
                await _HttpClient.GetAsync(getUrl)
            );

            // Assert
            AssertResponseSuccess(getResponse);
            Assert.NotNull(getResponse.Content);
            Assert.Equal(createdAnalysisId, getResponse.Content.Id);

            // Clean up
            var deleteUrl = $"/api/BookAnalysis/DeleteBookAnalysis/{createdAnalysisId}";
            var deleteResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.DeleteAsync(deleteUrl)
            );
            AssertResponseSuccess(deleteResponse);
        }
    }
}
