using BookApp.Shared.Models.ClientModels;
using System.Net.Http.Json;

namespace Tests.IntegrationTests
{
    [Trait("Highlight", "Integration")]
    public class HighlightControllerIntegrationTests : ControllerIntegrationTests
    {
        public HighlightControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestAddHighlight()
        {
            // Arrange
            var testAnalysis = await AddTestAnalysisToDatabase();
            var newHighlightModel = new HighlightModel
            {
                BookAnalysisId = testAnalysis.Id,
                FirstNodeCharIndex = 1,
                LastNodeCharIndex = 2,
                LastNodeIndex = 1,
                FirstNodeIndex = 2,
                Chapter = 1,
                RawPositionString = "",
            };

            var createUrl = "/Highlight/AddHighlight";

            // Act
            var createResponse = await DeserializeResponse<ServiceResponse<HighlightModel>>(
                await _HttpClient.PostAsJsonAsync(createUrl, newHighlightModel)
            );

            // Assert
            AssertResponseSuccess(createResponse);
            Assert.NotNull(createResponse.Content);
            Assert.True(createResponse.Content.Id > 0);
        }


        [Fact]
        public async Task TestUpdateHighlight()
        {
            // Arrange
            var testHighlight = await AddTestHighlightToDatabase();

            var updatedHighlightModel = new HighlightModel
            {
                Id = testHighlight.Id,
                RawPositionString = "edited"
            };

            var updateUrl = "/Highlight/UpdateHighlight";

            // Act
            var updateResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.PutAsJsonAsync(updateUrl, updatedHighlightModel)
            );

            // Assert
            AssertResponseSuccess(updateResponse);

            var analysis = await GetBookAnalysisRequest(testHighlight.BookAnalysisId);
            var editedHighlight = analysis.Content.Tags.FirstOrDefault(h => h.Id == testHighlight.Id);

            Assert.Equal(updatedHighlightModel.Id, updatedHighlightModel.Id);
            Assert.Equal(updatedHighlightModel.RawPositionString, updatedHighlightModel.RawPositionString);
        }

        [Fact]
        public async Task TestDeleteHighlight()
        {
            // Arrange
            var testHighlight = await AddTestHighlightToDatabase();

            var deleteUrl = $"/Highlight/DeleteHighlight/{testHighlight.Id}";

            // Act
            var deleteResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.DeleteAsync(deleteUrl)
            );

            // Assert
            AssertResponseSuccess(deleteResponse);
            var analysis = await GetBookAnalysisRequest(testHighlight.BookAnalysisId);
            var deletedHighlight = analysis.Content.Tags.FirstOrDefault(h => h.Id == testHighlight.Id);

            Assert.Null(deletedHighlight);
        }

        [Fact]
        public async Task TestAddTag()
        {
            // Arrange
            var testHighlight = await AddTestHighlightToDatabase();
            var testTag = await AddTestTagToDatabase();

            var getHighlightUrl = $"/Highlight/AddTag/{testHighlight.Id}/{testTag.Id}";
            var getHighlightResponse = await DeserializeResponse<ServiceResponse<HighlightModel>>(
                await _HttpClient.PostAsync(getHighlightUrl, null)
            );

            AssertResponseSuccess(getHighlightResponse);

            var analysis = await GetBookAnalysisRequest(testHighlight.BookAnalysisId);

            var addedTag = analysis.Content
                .Highlights.FirstOrDefault(h => h.Id == testHighlight.Id)
                .Tags.FirstOrDefault(t => t.Id == testTag.Id);

            Assert.NotNull(addedTag);
        }

        [Fact]
        public async Task TestRemoveTag()
        {
            // Arrange
            var testHighlight = await AddTestHighlightToDatabase();
            var testTag = await AddTestTagToDatabase();

            var addTagUrl = $"/Highlight/AddTag/{testHighlight.Id}/{testTag.Id}";
            var addTagResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.PostAsync(addTagUrl, null)
            );

            // Act
            var removeTagUrl = $"/Highlight/RemoveTag/{testHighlight.Id}/{testTag.Id}";
            var removeTagResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.DeleteAsync(removeTagUrl)
            );

            // Assert
            AssertResponseSuccess(removeTagResponse);

            var analysis = await GetBookAnalysisRequest(testHighlight.BookAnalysisId);
            Assert.Null(analysis.Content.Highlights.FirstOrDefault(h => h.Id == testHighlight.Id).Tags.FirstOrDefault(t => t.Id == testTag.Id));
        }
    }
}
