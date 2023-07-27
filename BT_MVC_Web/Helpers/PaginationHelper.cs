using BT_MVC_Web.Models;

namespace BT_MVC_Web.Helpers
{
    public class PaginationHelper<T>
    {
        public static PaginationViewModel<T> GetPagedData(IEnumerable<T> data, int page, int pageSize)
        {
            var paginationViewModel = new PaginationViewModel<T>
            {
                TotalItems = data.Count(),
                TotalPages = data.Count() / pageSize,
                CurrentPage = page,
                PageSize = pageSize,
                Data = data.ToList(),
            };

            return paginationViewModel;
        }
    }

}
