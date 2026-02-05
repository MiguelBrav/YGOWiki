using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class RarityAggregator : IRarityAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllRaritiesQueryHandler _allRaritiesQueryHandler;
    private readonly AllRaritiesPageQueryHandler _allRaritiesPageQueryHandler;
    private readonly RarityByIdQueryHandler _rarityByIdQueryHandler;

    public RarityAggregator(UseCaseDispatcher dispatcher, AllRaritiesQueryHandler allRaritiesQueryHandler, AllRaritiesPageQueryHandler allRaritiesPageQueryHandler, RarityByIdQueryHandler rarityByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allRaritiesQueryHandler = allRaritiesQueryHandler;
        _allRaritiesPageQueryHandler = allRaritiesPageQueryHandler;
        _rarityByIdQueryHandler = rarityByIdQueryHandler;
    }

    public async Task<ApiResponse> AllRaritiesPageQuery(AllRaritiesPageQuery request)
    {
        return await _dispatcher.Dispatch(_allRaritiesPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllRaritiesQuery(AllRaritiesQuery request)
    {
        return await _dispatcher.Dispatch(_allRaritiesQueryHandler, request);
    }

    public async Task<ApiResponse> RarityByIdQuery(RarityByIdQuery request)
    {
        return await _dispatcher.Dispatch(_rarityByIdQueryHandler, request);
    }
}
