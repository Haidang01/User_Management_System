using BT_MVC_Web.Data_Access;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        private readonly ApplicationDbContext _db;
        public DistrictRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateDistrictAsync(District district)
        {
            var objFromDb = _db.Districts.FirstOrDefault(u => u.DistrictId == district.DistrictId);
            if (objFromDb != null)
            {
                objFromDb.DistrictName = district.DistrictName;
                objFromDb.CityId = district.CityId;
            }
        }

    }
}
