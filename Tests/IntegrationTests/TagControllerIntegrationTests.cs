using BookApp.Server.Models;
using BookApp.Shared.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    [Trait("Tag", "Integration")]
    public class TagControllerIntegrationTests : ControllerIntegrationTests
    {
        public TagControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestCreateNewTag()
        {
            // Arrange
            var testAnalysis = await AddTestAnalysisToDatabase();
            var newTagModel = new TagModel
            {
                Name = "TestTag"
            };

            var createUrl = $"/Tag/CreateNewTag/{testAnalysis.Id}";

            // Act
            var createResponse = await DeserializeResponse<ServiceResponse<TagModel>>(
                await _HttpClient.PostAsJsonAsync(createUrl, newTagModel)
            );

            // Assert
            AssertResponseSuccess(createResponse);
            Assert.NotNull(createResponse.Content);
            Assert.True(createResponse.Content.Id > 0); 
        }

        [Fact]
        public async Task TestDeleteTag()
        {
            // Arrange
            var testTag = await AddTestTagToDatabase();

            // Act
            var deleteUrl = $"/Tag/DeleteTag/{testTag.Id}";
            var deleteResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.DeleteAsync(deleteUrl)
            );

            // Assert
            AssertResponseSuccess(deleteResponse);
            var tagExists = (await FindInDatabaseByConditionsFirstOrDefault<Tag>(t => t.Id == testTag.Id)) is not null;
            Assert.False(tagExists);
        }

        [Fact]
        public async Task TestEditTag()
        {
            // Arrange
            var testTag = await AddTestTagToDatabase();

            var updatedTagModel = new TagModel
            {
                Id = testTag.Id,
                Name = "edited"
            };

            var editUrl = "/Tag/EditTag";

            // Act
            var editResponse = await DeserializeResponse<ServiceResponse>(
                await _HttpClient.PutAsJsonAsync(editUrl, updatedTagModel)
            );

            // Assert
            AssertResponseSuccess(editResponse);

            var analysis = await GetBookAnalysisRequest(testTag.BookAnalysisId);
            var editedTag = analysis.Content.Tags.FirstOrDefault(t => t.Id == testTag.Id);

            Assert.Equal(updatedTagModel.Id, editedTag.Id);
            Assert.Equal(updatedTagModel.Name, editedTag.Name);
        }
    }
}
