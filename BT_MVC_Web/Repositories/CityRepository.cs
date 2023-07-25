using BT_MVC_Web.Data_Access;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;
        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateCityAsync(City city)
        {
            var objFromDb = _db.Cities.FirstOrDefault(u => u.CityId == city.CityId);
            if (objFromDb != null)
            {
                objFromDb.CityName = city.CityName;
            }
        }

    }
}
