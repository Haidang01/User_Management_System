using BT_MVC_Web.Models;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services.Interface
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync(string? includeProperties = null);
        Task<City>? GetCityAsync(Expression<Func<City, bool>> filter, string? includeProperties = null);
        Task AddCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task RemoveCity(City city);

    }
}
