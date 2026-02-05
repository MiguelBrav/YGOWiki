using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface ITrapAggregator
{
    Task<ApiResponse> AllTrapsPageQuery(AllTrapsPageQuery request);

    Task<ApiResponse> AllTrapsQuery(AllTrapsQuery request);

    Task<ApiResponse> TrapByIdQuery(TrapByIdQuery request);
}
