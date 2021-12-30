
namespace Core.CustomEntities
{
    public class Metadata<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }

        public Metadata(PagedList<T> pagedList)
        {
            TotalCount = pagedList.TotalCount;
            PageSize = pagedList.PageSize;
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
            HasNextPage = pagedList.HasNextPage;
            HasPreviousPage = pagedList.HasPreviousPage;

            NextPageUrl = null;
            PreviousPageUrl = null;
        }
    }
}
