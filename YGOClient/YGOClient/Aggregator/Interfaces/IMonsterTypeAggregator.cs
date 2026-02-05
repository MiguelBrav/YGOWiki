using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface IMonsterTypeAggregator
{
    Task<ApiResponse> AllMonsterTypesPageQuery(AllMonsterTypesPageQuery request);

    Task<ApiResponse> AllMonsterTypesQuery(AllMonsterTypesQuery request);

    Task<ApiResponse> MonsterTypeByIdQuery(MonsterTypeByIdQuery request);
}
