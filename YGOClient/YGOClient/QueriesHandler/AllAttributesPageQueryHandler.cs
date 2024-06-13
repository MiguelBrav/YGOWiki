using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Interfaces;
using YGOClient.Models;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllAttributesPageQueryHandler : IRequestHandler<AllAttributesPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<AttributeDetail> _paginationService;
        public AllAttributesPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<AttributeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllAttributesPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllAttributeReply response = await _client.GettAllAttributesAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<AttributeDetail> attributesPage =  _paginationService.GetPagedData(response.Attributes, request.PageId, request.PageSize);
                PagedResult<AttributeDetail> attributesPage = _paginationService.GetPagedResult(response.Attributes, request.PageId, request.PageSize);


                if (response.Attributes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.Attributes.Clear();

                //response.Attributes.AddRange(attributesPage);

                var jsonResponse = JsonSerializer.Serialize(attributesPage, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                });

                grpcResponse.StatusCode = 200;
                grpcResponse.ResponseMessage = jsonResponse;
                grpcResponse.Response = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                grpcResponse.StatusCode = 405;
                grpcResponse.ResponseMessage = ex.Message;
                grpcResponse.Response = false;
            }
            catch (Exception)
            {
                grpcResponse.StatusCode = 500; 
                grpcResponse.ResponseMessage = "Error";
                grpcResponse.Response = false;
            }

            return grpcResponse;

        }
    }
}
