using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface IRarityAggregator
{
    Task<ApiResponse> AllRaritiesPageQuery(AllRaritiesPageQuery request);

    Task<ApiResponse> AllRaritiesQuery(AllRaritiesQuery request);

    Task<ApiResponse> RarityByIdQuery(RarityByIdQuery request);
}
