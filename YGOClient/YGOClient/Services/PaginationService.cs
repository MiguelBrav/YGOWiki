using Google.Protobuf.Collections;
using YGOClient.Interfaces;

namespace YGOClient.Services
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public RepeatedField<T> GetPagedData(RepeatedField<T> allData, int pageId, int pageSize)
        {
            int totalCount = allData.Count;
            int totalPages = (int)System.Math.Ceiling((double)totalCount / pageSize);

            if (pageId > totalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(pageId), "The requested page is outside the valid range.");
            }

            pageId = System.Math.Max(1, System.Math.Min(pageId, totalPages));

            int startIndex = (pageId - 1) * pageSize;
            int endIndex = System.Math.Min(startIndex + pageSize, totalCount);

            RepeatedField<T> pagedData = new RepeatedField<T>();

            for (int i = startIndex; i < endIndex; i++)
            {
                pagedData.Add(allData[i]);
            }

            return pagedData;
        }
    }
}
