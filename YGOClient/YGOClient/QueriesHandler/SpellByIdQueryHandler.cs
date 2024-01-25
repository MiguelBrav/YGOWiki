﻿using Grpc.Core;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
    public class SpellByIdQueryHandler : IRequestHandler<SpellByIdQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;

        public SpellByIdQueryHandler(YGOWiki.YGOWikiClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse> Handle(SpellByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                SpellTypeDetail response = await _client.GetSpellTypeAsync(new ByLanguageIdAndId { LanguageId = request.LanguageId, Id = request.Id });

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                });

                grpcResponse.StatusCode = 200;
                grpcResponse.ResponseMessage = jsonResponse;
                grpcResponse.Response = true;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled && !string.IsNullOrEmpty(ex.Status.Detail))
            {
                grpcResponse.StatusCode = 404;
                grpcResponse.ResponseMessage = ex.Status.Detail;
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
