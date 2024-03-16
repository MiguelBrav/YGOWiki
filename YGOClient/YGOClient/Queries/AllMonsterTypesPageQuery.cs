using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllMonsterTypesPageQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }

        public int PageId { get; set; }

        public int PageSize { get; set; }
    }
}
