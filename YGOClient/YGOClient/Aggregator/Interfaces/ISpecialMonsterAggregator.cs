using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface ISpecialMonsterAggregator
{
    Task<ApiResponse> AllSpecialMonsterCardsPageQuery(AllSpecialMonsterCardsPageQuery request);

    Task<ApiResponse> AllSpecialMonsterCardsQuery(AllSpecialMonsterCardsQuery request);

    Task<ApiResponse> SpecialMonsterCardByIdQuery(SpecialMonsterCardByIdQuery request);
}
