using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface IBanlistAggregator
{
    Task<ApiResponse> AllBanlistPageQuery(AllBanlistPageQuery request);

    Task<ApiResponse> AllBanlistQuery(AllBanlistQuery request);

    Task<ApiResponse> BanlistByIdQuery(BanlistByIdQuery request);
}
