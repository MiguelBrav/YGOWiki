using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface ISpellAggregator
{
    Task<ApiResponse> AllSpellsPageQuery(AllSpellsPageQuery request);

    Task<ApiResponse> AllSpellsQuery(AllSpellsQuery request);

    Task<ApiResponse> SpellByIdQuery(SpellByIdQuery request);
}
