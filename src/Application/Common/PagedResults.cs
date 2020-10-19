using System.Collections.Generic;

namespace Application.Common
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
        public int Count { get; set; }
    }
}
