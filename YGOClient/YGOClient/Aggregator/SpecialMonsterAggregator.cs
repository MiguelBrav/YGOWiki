using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class SpecialMonsterAggregator : ISpecialMonsterAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllSpecialMonsterCardsQueryHandler _allSpecialMonsterCardsQueryHandler;
    private readonly AllSpecialMonsterCardsPageQueryHandler _allSpecialMonsterCardsPageQueryHandler;
    private readonly SpecialMonsterCardByIdQueryHandler _specialMonsterCardByIdQueryHandler;

    public SpecialMonsterAggregator(UseCaseDispatcher dispatcher, AllSpecialMonsterCardsQueryHandler allSpecialMonsterCardsQueryHandler, AllSpecialMonsterCardsPageQueryHandler allSpecialMonsterCardsPageQueryHandler, SpecialMonsterCardByIdQueryHandler specialMonsterCardByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allSpecialMonsterCardsQueryHandler = allSpecialMonsterCardsQueryHandler;
        _allSpecialMonsterCardsPageQueryHandler = allSpecialMonsterCardsPageQueryHandler;
        _specialMonsterCardByIdQueryHandler = specialMonsterCardByIdQueryHandler;
    }

    public async Task<ApiResponse> AllSpecialMonsterCardsPageQuery(AllSpecialMonsterCardsPageQuery request)
    {
        return await _dispatcher.Dispatch(_allSpecialMonsterCardsPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllSpecialMonsterCardsQuery(AllSpecialMonsterCardsQuery request)
    {
        return await _dispatcher.Dispatch(_allSpecialMonsterCardsQueryHandler, request);
    }

    public async Task<ApiResponse> SpecialMonsterCardByIdQuery(SpecialMonsterCardByIdQuery request)
    {
        return await _dispatcher.Dispatch(_specialMonsterCardByIdQueryHandler, request);
    }
}
