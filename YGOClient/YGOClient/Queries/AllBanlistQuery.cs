﻿using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllBanlistQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
