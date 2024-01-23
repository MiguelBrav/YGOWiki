using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class MonsterTypeByIdQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
        public int Id { get; set; }
    }
}
