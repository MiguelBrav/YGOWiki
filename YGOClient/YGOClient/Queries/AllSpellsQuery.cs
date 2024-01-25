using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllSpellsQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
