using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllRaritiesQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
