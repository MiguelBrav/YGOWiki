using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllMonsterCardsQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
