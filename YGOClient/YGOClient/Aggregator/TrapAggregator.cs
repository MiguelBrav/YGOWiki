using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class TrapAggregator : ITrapAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllTrapsQueryHandler _allTrapsQueryHandler;
    private readonly AllTrapsPageQueryHandler _allTrapsPageQueryHandler;
    private readonly TrapByIdQueryHandler _trapByIdQueryHandler;

    public TrapAggregator(UseCaseDispatcher dispatcher, AllTrapsQueryHandler allTrapsQueryHandler, AllTrapsPageQueryHandler allTrapsPageQueryHandler, TrapByIdQueryHandler trapByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allTrapsQueryHandler = allTrapsQueryHandler;
        _allTrapsPageQueryHandler = allTrapsPageQueryHandler;
        _trapByIdQueryHandler = trapByIdQueryHandler;
    }

    public async Task<ApiResponse> AllTrapsPageQuery(AllTrapsPageQuery request)
    {
        return await _dispatcher.Dispatch(_allTrapsPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllTrapsQuery(AllTrapsQuery request)
    {
        return await _dispatcher.Dispatch(_allTrapsQueryHandler, request);
    }

    public async Task<ApiResponse> TrapByIdQuery(TrapByIdQuery request)
    {
        return await _dispatcher.Dispatch(_trapByIdQueryHandler, request);
    }
}
