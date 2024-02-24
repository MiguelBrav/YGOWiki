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
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

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
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es",90)] // Invalid language and Id
        [InlineData("",99)]  // Invalid language  and Id
        public async Task GetAttribute_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AttributeByIdQuery { LanguageId = languageId , Id = id};
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)] 
        [InlineData("en-us", 1)]  
        public async Task GetAttribute_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AttributeByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)] 
        [InlineData("en-us", 1)]  
        public async Task GetAttribute_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AttributeByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AttributeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);         
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);
        }
        #endregion

        #region "Banlist Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllBanlist_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllBanlistQuery { LanguageId = languageId };
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
        public async Task GetAllBanlist_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllBanlistQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllBanlist_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllBanlistQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllBanlistReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 80)] // Invalid language and Id
        [InlineData("", 88)]  // Invalid language  and Id
        public async Task GetBanlist_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new BanlistByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)] 
        [InlineData("en-us", 1)]  
        public async Task GetBanlist_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new BanlistByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetBanlist_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new BanlistByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new BanlistTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "MonsterCard Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllMonsterCard_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllMonsterCardsQuery { LanguageId = languageId };
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
        public async Task GetAllMonsterCard_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllMonsterCardsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllMonsterCard_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllMonsterCardsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllMonsterCardTypeReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 89)] // Invalid language and Id
        [InlineData("", 66)]  // Invalid language  and Id
        public async Task GetMonsterCard_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new MonsterCardByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]  
        public async Task GetMonsterCard_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new MonsterCardByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetMonsterCard_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new MonsterCardByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new MonsterCardDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "MonsterType Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllMonsterType_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllMonsterTypesQuery { LanguageId = languageId };
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
        public async Task GetAllMonsterType_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllMonsterTypesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllMonsterType_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllMonsterTypesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllMonsterTypeReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 66)] // Invalid language and Id
        [InlineData("", 55)]  // Invalid language  and Id
        public async Task GetMonsterType_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new MonsterTypeByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetMonsterType_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new MonsterTypeByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetMonsterType_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new MonsterTypeByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new MonsterTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "Rarity Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllRarities_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllRaritiesQuery { LanguageId = languageId };
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
        public async Task GetAllRarities_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllRaritiesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllRarities_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllRaritiesQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllRarityReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 99)] // Invalid language and Id
        [InlineData("", 88)]  // Invalid language  and Id
        public async Task GetRarity_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new RarityByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetRarity_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new RarityByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetRarity_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new RarityByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new RarityTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "SpecialMonster Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllSpecialMonsters_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllSpecialMonsterCardsQuery { LanguageId = languageId };
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
        public async Task GetAllSpecialMonsters_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllSpecialMonsterCardsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllSpecialMonsters_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllSpecialMonsterCardsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllSpecialMonsterTypeReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 99)] // Invalid language and Id
        [InlineData("", 88)]  // Invalid language  and Id
        public async Task GetSpecialMonster_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new SpecialMonsterCardByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetSpecialMonster_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new SpecialMonsterCardByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetSpecialMonster_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new SpecialMonsterCardByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new SpecialMonsterTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "Spell Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllSpells_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllSpellsQuery { LanguageId = languageId };
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
        public async Task GetAllSpells_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllSpellsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllSpells_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllSpellsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllSpellTypeReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 109)] // Invalid language and Id
        [InlineData("", 200)]  // Invalid language  and Id
        public async Task GetSpell_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new SpellByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetSpell_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new SpellByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetSpell_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new SpellByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new SpellTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "Trap Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllTraps_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllTrapsQuery { LanguageId = languageId };
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
        public async Task GetAllTraps_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllTrapsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllTraps_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllTrapsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllTrapTypeReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 111)] // Invalid language and Id
        [InlineData("", 220)]  // Invalid language  and Id
        public async Task GetTrap_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new TrapByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetTrap_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new TrapByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetTrap_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new TrapByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new TrapTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion

        #region "TypeCard Controller"
        [Theory]
        [InlineData("es-es")] // Invalid language
        [InlineData("")]  // Invalid language
        public async Task GetAllTypeCards_ReturnsStatusCode204_WhenResponseIsNoContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllTypeCardsQuery { LanguageId = languageId };
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
        public async Task GetAllTypeCards_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllTypeCardsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx")]
        [InlineData("en-us")]
        public async Task GetAllTypeCards_ReturnsStatusCode200_WhenResponseIsContent(string languageId)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new AllTypeCardsQuery { LanguageId = languageId };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new AllTypeCardsReply()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-es", 112)] // Invalid language and Id
        [InlineData("", 2222)]  // Invalid language  and Id
        public async Task GetTypeCard_ReturnsStatusCode404_WhenResponseIsNoContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new TypeCardByIdQuery { LanguageId = languageId, Id = id };
            RpcException exception = new RpcException(Status.DefaultCancelled, "Error");
            var apiResponse = new ApiResponse { StatusCode = 404, ResponseMessage = exception.Message };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }

        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetTypeCard_ReturnsStatusCode500_WhenResponseIsInternalServerError(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new TypeCardByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 500, ResponseMessage = "Error" };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }


        [Theory]
        [InlineData("es-mx", 1)]
        [InlineData("en-us", 1)]
        public async Task GetTypeCard_ReturnsStatusCode200_WhenResponseIsContent(string languageId, int id)
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var query = new TypeCardByIdQuery { LanguageId = languageId, Id = id };
            var apiResponse = new ApiResponse { StatusCode = 200, ResponseMessage = JsonSerializer.Serialize(new CardTypeDetail()) };

            mediatorMock.Setup(m => m.Send(query, default(CancellationToken))).ReturnsAsync(apiResponse);

            // Act
            var response = await mediatorMock.Object.Send(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseMessage);
            Assert.Equal(apiResponse.StatusCode, response.StatusCode);
            Assert.Equal(apiResponse.ResponseMessage, response.ResponseMessage);
            Assert.IsType<string>(apiResponse.ResponseMessage);
            Assert.IsType<ApiResponse>(response);

        }
        #endregion
    }
}
