using BT_MVC_Web.Data_Access;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class OccupationRepository : Repository<Occupation>, IOccupationRepository
    {
        private readonly ApplicationDbContext _db;
        public OccupationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateOccupationAsync(Occupation occupation)
        {
            Occupation? objFromDb = _db.Occupations.FirstOrDefault(u => u.OccupationId == occupation.OccupationId);
            if (objFromDb != null)
            {
                objFromDb.OccupationName = occupation.OccupationName;
            }
        }

    }
}
