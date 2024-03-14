using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YGOClient.Interfaces
{
    public interface IPaginationService<T>
    {
        RepeatedField<T> GetPagedData(RepeatedField<T> allData, int pageNumber, int pageSize);
    }
}
