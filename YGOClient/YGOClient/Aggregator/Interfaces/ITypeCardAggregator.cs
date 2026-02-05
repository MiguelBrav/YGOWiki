using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface ITypeCardAggregator
{
    Task<ApiResponse> AllTypeCardsPageQuery(AllTypeCardsPageQuery request);

    Task<ApiResponse> AllTypeCardsQuery(AllTypeCardsQuery request);

    Task<ApiResponse> TypeCardByIdQuery(TypeCardByIdQuery request);
}
