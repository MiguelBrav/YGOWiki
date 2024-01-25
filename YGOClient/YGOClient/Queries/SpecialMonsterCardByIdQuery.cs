using MediatR;
using YGOClient.DTO.APIResponse;

namespace YGOClient.Queries
{
    public class SpecialMonsterCardByIdQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
        public int Id { get; set; }
    }
}
