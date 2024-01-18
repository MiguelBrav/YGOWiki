using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllTypeCardsQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get;set; }
    }
}
