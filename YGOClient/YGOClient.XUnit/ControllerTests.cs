using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerYGO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using YGOClient.Controllers;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.XUnit
{
    public class ControllerTests
    {
        #region "Attribute Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllAttributes_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllAttributesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 204 };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);     
        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllAttributes_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllAttributesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 505, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.IsType<string>(apiResponse.ResponseMessage);
        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllAttributes_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllAttributesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllAttributeReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.IsType<string>(apiResponse.ResponseMessage);
        }
        #endregion
    }
}
