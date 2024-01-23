using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllMonsterTypesQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
