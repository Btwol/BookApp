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
            var testAnalysisId = (await AddTestAnalysisToDatabase()).Id;

            // Act
            var getUrl = $"/BookAnalysis/GetAnalysisById/{testAnalysisId}";
            var getResponse = await DeserializeResponse<ServiceResponse<BookAnalysisDetailedModel>>(
                await _HttpClient.GetAsync(getUrl)
            );

            // Assert
            AssertResponseSuccess(getResponse);
            Assert.NotNull(getResponse.Content);
            Assert.Equal(testAnalysisId, getResponse.Content.Id);
        }

        [Fact]
        public async Task TestGetAnalysisByHash()
        {
            // Arrange
            var testAnalysisHash = (await AddTestAnalysisToDatabase()).BookHash;

            // Act
            var getUrl = $"/BookAnalysis/GetAnalysesByHash/{testAnalysisHash}";
            var getResponse = await DeserializeResponse<ServiceResponse<List<BookAnalysisDetailedModel>>>(
                await _HttpClient.GetAsync(getUrl)
            );

            // Assert
            AssertResponseSuccess(getResponse);
            Assert.NotNull(getResponse.Content);
            Assert.True(getResponse.Content.Any(a => a.BookHash == testAnalysisHash));
        }

        [Fact]
        public async Task TestEditBookAnalysis()
        {
            var testAnalysis = await AddTestAnalysisToDatabase();

            // Update properties for the analysis
            var updatedAnalysisModel = new BookAnalysisSummaryModel
            {
                Id = testAnalysis.Id,
                AnalysisTitle = "edited",
                Authors = new List<string> { "edited" },
                BookHash = testAnalysis.BookHash,
                BookTitle = "edited"
            };

            var editUrl = "/BookAnalysis/EditBookAnalysis";

            // Act
            var editResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.PutAsJsonAsync(editUrl, updatedAnalysisModel)
            );

            // Assert
            AssertResponseSuccess(editResponse);
            var analysis = await GetBookAnalysisRequest(testAnalysis.Id);

            AssertResponseSuccess(analysis);
            Assert.NotNull(analysis.Content);
            Assert.Equal(testAnalysis.Id, analysis.Content.Id);
            Assert.Equal(updatedAnalysisModel.AnalysisTitle, analysis.Content.AnalysisTitle);
        }

        [Fact]
        public async Task TestCreateBookAnalysis()
        {
            // Arrange
            var newAnalysisModel = new BookAnalysisSummaryModel
            {
                BookHash = "TestHash",
                AnalysisTitle = "TestAnalysis",
                Authors = new List<string> { "testAuthor" },
                BookTitle = "TestBook"
            };

            var createUrl = "/BookAnalysis/CreateBookAnalysis";

            // Act
            var createResponse = await DeserializeResponse<ServiceResponse<BookAnalysisSummaryModel>>(
                await _HttpClient.PostAsJsonAsync(createUrl, newAnalysisModel)
            );

            // Assert
            AssertResponseSuccess(createResponse);
            Assert.NotNull(createResponse.Content);
            Assert.True(createResponse.Content.Id > 0);
        }

        [Fact]
        public async Task TestDeleteBookAnalysis()
        {
            // Arrange
            var testAnalysis = await AddTestAnalysisToDatabase();

            // Act
            var deleteUrl = $"/BookAnalysis/DeleteBookAnalysis/{testAnalysis.Id}";

            var deleteResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.DeleteAsync(deleteUrl)
            );

            // Assert
            AssertResponseSuccess(deleteResponse);

            var analysis = await GetBookAnalysisRequest(testAnalysis.Id);
            AssertResponseFailure(analysis);
        }
    }
}
