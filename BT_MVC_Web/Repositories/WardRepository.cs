using BT_MVC_Web.Data_Access;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class WardRepository : Repository<Ward>, IWardRepository
    {
        private readonly ApplicationDbContext _db;
        public WardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateWardAsync(Ward ward)
        {
            var objFromDb = _db.Wards.FirstOrDefault(u => u.WardId == ward.WardId);
            if (objFromDb != null)
            {
                objFromDb.WardName = ward.WardName;
                objFromDb.DistrictId = ward.DistrictId;
            }
        }

    }
}
