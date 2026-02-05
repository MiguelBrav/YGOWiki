using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class SpellAggregator : ISpellAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllSpellsQueryHandler _allSpellsQueryHandler;
    private readonly AllSpellsPageQueryHandler _allSpellsPageQueryHandler;
    private readonly SpellByIdQueryHandler _spellByIdQueryHandler;

    public SpellAggregator(UseCaseDispatcher dispatcher, AllSpellsQueryHandler allSpellsQueryHandler, AllSpellsPageQueryHandler allSpellsPageQueryHandler, SpellByIdQueryHandler spellByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allSpellsQueryHandler = allSpellsQueryHandler;
        _allSpellsPageQueryHandler = allSpellsPageQueryHandler;
        _spellByIdQueryHandler = spellByIdQueryHandler;
    }

    public async Task<ApiResponse> AllSpellsPageQuery(AllSpellsPageQuery request)
    {
        return await _dispatcher.Dispatch(_allSpellsPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllSpellsQuery(AllSpellsQuery request)
    {
        return await _dispatcher.Dispatch(_allSpellsQueryHandler, request);
    }

    public async Task<ApiResponse> SpellByIdQuery(SpellByIdQuery request)
    {
        return await _dispatcher.Dispatch(_spellByIdQueryHandler, request);
    }
}
