namespace BT_MVC_Web.Models
{
    public class PaginationViewModel<T>
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }
}
