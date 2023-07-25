using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services.Interface
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAllDistrictsAsync(string? includeProperties = null);
        Task<District>? GetDistrictAsync(Expression<Func<District, bool>> filter, string? includeProperties = null);
        Task AddDistrictAsync(DistrictCreatDto district);
        Task UpdateDistrictAsync(District district);
        Task RemoveDistrict(District district);

    }
}
