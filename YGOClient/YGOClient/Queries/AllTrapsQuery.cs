using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class AllTrapsQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
