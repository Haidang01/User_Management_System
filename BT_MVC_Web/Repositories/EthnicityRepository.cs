using BT_MVC_Web.Data_Access;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class EthnicityRepository : Repository<Ethnicity>, IEthnicityRepository
    {
        private readonly ApplicationDbContext _db;
        public EthnicityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateEthnicityAsync(Ethnicity ethnicity)
        {
            Ethnicity? objFromDb = _db.Ethnicities.FirstOrDefault(u => u.EthnicityId== ethnicity.EthnicityId);
            if (objFromDb != null)
            {
                objFromDb.EthnicityName= ethnicity.EthnicityName;
            }
        }

    }
}
