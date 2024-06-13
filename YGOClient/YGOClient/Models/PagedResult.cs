using Google.Protobuf.Collections;

namespace YGOClient.Models;

public class PagedResult<T>
{
    public RepeatedField<T> Data { get; set; } = new RepeatedField<T>();
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}
