using BT_MVC_Web.Models;

namespace BT_MVC_Web.Repositories.Interface
{
    public interface ICityRepository : IRepository<City>
    {
        Task UpdateCityAsync(City city);
    }

}
