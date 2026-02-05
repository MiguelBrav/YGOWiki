using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class MonsterTypeAggregator : IMonsterTypeAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllMonsterTypesQueryHandler _allMonsterTypesQueryHandler;
    private readonly AllMonsterTypesPageQueryHandler _allMonsterTypesPageQueryHandler;
    private readonly MonsterTypeByIdQueryHandler _monsterTypeByIdQueryHandler;

    public MonsterTypeAggregator(UseCaseDispatcher dispatcher, AllMonsterTypesQueryHandler allMonsterTypesQueryHandler, AllMonsterTypesPageQueryHandler allMonsterTypesPageQueryHandler, MonsterTypeByIdQueryHandler monsterTypeByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allMonsterTypesQueryHandler = allMonsterTypesQueryHandler;
        _allMonsterTypesPageQueryHandler = allMonsterTypesPageQueryHandler;
        _monsterTypeByIdQueryHandler = monsterTypeByIdQueryHandler;
    }

    public async Task<ApiResponse> AllMonsterTypesPageQuery(AllMonsterTypesPageQuery request)
    {
        return await _dispatcher.Dispatch(_allMonsterTypesPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllMonsterTypesQuery(AllMonsterTypesQuery request)
    {
        return await _dispatcher.Dispatch(_allMonsterTypesQueryHandler, request);
    }

    public async Task<ApiResponse> MonsterTypeByIdQuery(MonsterTypeByIdQuery request)
    {
        return await _dispatcher.Dispatch(_monsterTypeByIdQueryHandler, request);
    }
}
