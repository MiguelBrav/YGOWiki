using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class TypeCardAggregator : ITypeCardAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllTypeCardsQueryHandler _allTypeCardsQueryHandler;
    private readonly AllTypeCardsPageQueryHandler _allTypeCardsPageQueryHandler;
    private readonly TypeCardByIdQueryHandler _typeCardByIdQueryHandler;

    public TypeCardAggregator(UseCaseDispatcher dispatcher, AllTypeCardsQueryHandler allTypeCardsQueryHandler, AllTypeCardsPageQueryHandler allTypeCardsPageQueryHandler, TypeCardByIdQueryHandler typeCardByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _allTypeCardsQueryHandler = allTypeCardsQueryHandler;
        _allTypeCardsPageQueryHandler = allTypeCardsPageQueryHandler;
        _typeCardByIdQueryHandler = typeCardByIdQueryHandler;
    }

    public async Task<ApiResponse> AllTypeCardsPageQuery(AllTypeCardsPageQuery request)
    {
        return await _dispatcher.Dispatch(_allTypeCardsPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllTypeCardsQuery(AllTypeCardsQuery request)
    {
        return await _dispatcher.Dispatch(_allTypeCardsQueryHandler, request);
    }

    public async Task<ApiResponse> TypeCardByIdQuery(TypeCardByIdQuery request)
    {
        return await _dispatcher.Dispatch(_typeCardByIdQueryHandler, request);
    }
}
