using BT_MVC_Web.Models;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services.Interface
{
    public interface IOccupationService
    {
        Task<IEnumerable<Occupation>> GetAllOccupationsAsync(int page, int pageSize, string? includeProperties = null);
        Task<Occupation>? GetOccupationAsync(Expression<Func<Occupation, bool>> filter, string? includeProperties = null);
        Task AddOccupationAsync(Occupation occupation);
        Task UpdateOccupationAsync(Occupation occupation);
        Task RemoveOccupation(Occupation occupation);

    }
}
