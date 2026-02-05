using UseCaseCore.UseCases;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Aggregator;

public class AttributeAggregator : IAttributeAggregator
{
    private readonly UseCaseDispatcher _dispatcher;
    private readonly AllAttributesQueryHandler _attributesQueryHandler;
    private readonly AllAttributesPageQueryHandler _allAttributesPageQueryHandler;
    private readonly AttributeByIdQueryHandler _attributeByIdQueryHandler;

    public AttributeAggregator(UseCaseDispatcher dispatcher, AllAttributesQueryHandler attributesQueryHandler, AllAttributesPageQueryHandler allAttributesPageQueryHandler, AttributeByIdQueryHandler attributeByIdQueryHandler)
    {
        _dispatcher = dispatcher;
        _attributesQueryHandler = attributesQueryHandler;
        _allAttributesPageQueryHandler = allAttributesPageQueryHandler;
        _attributeByIdQueryHandler = attributeByIdQueryHandler;
    }

    public async Task<ApiResponse> AllAttributesPageQuery(AllAttributesPageQuery request)
    {
        return await _dispatcher.Dispatch(_allAttributesPageQueryHandler, request);

    }

    public async Task<ApiResponse> AllAttributesQuery(AllAttributesQuery request)
    {
        return await _dispatcher.Dispatch(_attributesQueryHandler, request);
    }

    public async Task<ApiResponse>  AttributeByIdQuery(AttributeByIdQuery request)
    {
        return await _dispatcher.Dispatch(_attributeByIdQueryHandler, request);
    }
}
