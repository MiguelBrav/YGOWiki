using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class BanlistAggregator : IBanlistAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllBanlistQueryHandler _allBanlistQueryHandler;
    private readonly AllBanlistPageQueryHandler _allBanlistPageQueryHandler;
    private readonly BanlistByIdQueryHandler _banlistByIdQueryHandler;

    public BanlistAggregator(UseCaseDispatcher dispatcher, AllBanlistQueryHandler allBanlistQueryHandler, AllBanlistPageQueryHandler allBanlistPageQueryHandler, BanlistByIdQueryHandler banlistByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allBanlistQueryHandler = allBanlistQueryHandler;
        _allBanlistPageQueryHandler = allBanlistPageQueryHandler;
        _banlistByIdQueryHandler = banlistByIdQueryHandler;
    }

    public async Task<ApiResponse> AllBanlistPageQuery(AllBanlistPageQuery request)
    {
        return await _dispatcher.Dispatch(_allBanlistPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllBanlistQuery(AllBanlistQuery request)
    {
        return await _dispatcher.Dispatch(_allBanlistQueryHandler, request);
    }

    public async Task<ApiResponse> BanlistByIdQuery(BanlistByIdQuery request)
    {
        return await _dispatcher.Dispatch(_banlistByIdQueryHandler, request);
    }
}
