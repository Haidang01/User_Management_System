using BT_MVC_Web.Data_Access;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICityRepository City { get; set; }

        public IWardRepository Ward { get; set; }

        public IDistrictRepository District { get; set; }

        public IEmployeeRepository Employee { get; set; }
        public IOccupationRepository Occupation { get; set; }
        public IEthnicityRepository Ethnicity { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            City = new CityRepository(db);
            Ward = new WardRepository(db);
            Employee = new EmployeeRepository(db);
            District = new DistrictRepository(db);
            Occupation = new OccupationRepository(db);
            Ethnicity = new EthnicityRepository(db);
        }


        public void Save() => _db.SaveChanges();
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
