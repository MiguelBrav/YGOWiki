using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Linq;
using YGOClient.Models;

namespace YGOClient.Interfaces
{
    public interface IPaginationService<T>
    {
        RepeatedField<T> GetPagedData(RepeatedField<T> allData, int pageNumber, int pageSize);

        PagedResult<T> GetPagedResult(RepeatedField<T> allData, int pageId, int pageSize);

    }
}
