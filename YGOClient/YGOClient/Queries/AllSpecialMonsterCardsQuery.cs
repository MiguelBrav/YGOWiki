using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllSpecialMonsterCardsQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
