using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class TypeCardByIdQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
        public int Id { get; set; }
    }
}
