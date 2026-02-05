using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Aggregator.Interfaces;

public interface IAttributeAggregator
{
    Task<ApiResponse> AllAttributesPageQuery(AllAttributesPageQuery request);

    Task<ApiResponse> AllAttributesQuery(AllAttributesQuery request);

    Task<ApiResponse> AttributeByIdQuery(AttributeByIdQuery request);
}
