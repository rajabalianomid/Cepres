using System.Linq;

namespace Cepres.Common.Domain
{
    public class Pagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
    }
    public class Pagination<TEntity> : Pagination
    {
        public Pagination(int count, int pageSize, int pageIndex, string sort, TEntity data)
        {
            Count = count;
            Data = data;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Sort = sort;
        }
        public int Count { get; set; }

        public TEntity Data { get; set; }

        public bool Next => PageIndex + 1 < (PageCount);

        public bool Previous => PageIndex > 0;

        public int PageCount => PageSize > 0
            ? ((Count % PageSize) > 0 ? (Count / PageSize) + 1 : (Count / PageSize))
            : 0;

    }
}
