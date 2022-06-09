namespace Howest.MagicCards.WebAPI.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        {
            get => (int)Math.Ceiling(TotalRecords / (double)PageSize);
        }
        public int TotalRecords { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }
    }
}
