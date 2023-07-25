using BT_MVC_Web.Models;

namespace BT_MVC_Web.Repositories.Interface
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task UpdateDistrictAsync(District district);
    }
}
