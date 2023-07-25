using BT_MVC_Web.Models;

namespace BT_MVC_Web.Helpers
{
    public class PaginationHelper<T>
    {
        public static PaginationViewModel<T> GetPagedData(IEnumerable<T> data, int page, int pageSize)
        {
            var totalItems = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedData = data.Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            var paginationViewModel = new PaginationViewModel<T>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Data = paginatedData
            };

            return paginationViewModel;
        }
    }

}
