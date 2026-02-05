using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class MonsterCardAggregator : IMonsterCardAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllMonsterCardsQueryHandler _allMonsterCardsQueryHandler;
    private readonly AllMonsterCardsPageQueryHandler _allMonsterCardsPageQueryHandler;
    private readonly MonsterCardByIdQueryHandler _monsterCardByIdQueryHandler;

    public MonsterCardAggregator(UseCaseDispatcher dispatcher, AllMonsterCardsQueryHandler allMonsterCardsQueryHandler, AllMonsterCardsPageQueryHandler allMonsterCardsPageQueryHandler, MonsterCardByIdQueryHandler monsterCardByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allMonsterCardsQueryHandler = allMonsterCardsQueryHandler;
        _allMonsterCardsPageQueryHandler = allMonsterCardsPageQueryHandler;
        _monsterCardByIdQueryHandler = monsterCardByIdQueryHandler;
    }

    public async Task<ApiResponse> AllMonsterCardsPageQuery(AllMonsterCardsPageQuery request)
    {
        return await _dispatcher.Dispatch(_allMonsterCardsPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllMonsterCardsQuery(AllMonsterCardsQuery request)
    {
        return await _dispatcher.Dispatch(_allMonsterCardsQueryHandler, request);
    }

    public async Task<ApiResponse> MonsterCardByIdQuery(MonsterCardByIdQuery request)
    {
        return await _dispatcher.Dispatch(_monsterCardByIdQueryHandler, request);
    }
}
