using BT_MVC_Web.Models;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services.Interface
{
    public interface IWardService
    {
        Task<IEnumerable<Ward>> GetAllWardsAsync(string? includeProperties = null);
        Task<Ward>? GetWardAsync(Expression<Func<Ward, bool>> filter, string? includeProperties = null);
        Task AddWardAsync(Ward ward);
        Task UpdateWardAsync(Ward ward);
        Task RemoveWard(Ward ward);

    }
}
