using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllAttributesQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
