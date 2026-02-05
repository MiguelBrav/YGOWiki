using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface IMonsterCardAggregator
{
    Task<ApiResponse> AllMonsterCardsPageQuery(AllMonsterCardsPageQuery request);

    Task<ApiResponse> AllMonsterCardsQuery(AllMonsterCardsQuery request);

    Task<ApiResponse> MonsterCardByIdQuery(MonsterCardByIdQuery request);
}
