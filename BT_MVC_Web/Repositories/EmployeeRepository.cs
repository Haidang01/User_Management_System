using BT_MVC_Web.Data_Access;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;

namespace BT_MVC_Web.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Employee FindById(int id)
        {
            return _db.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void UpdateEmployee(Employee obj)
        {
            var objFromDb = FindById(obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Gender= obj.Gender;
                objFromDb.EthnicityId = obj.EthnicityId;
                objFromDb.DateOfBirth = obj.DateOfBirth;
                objFromDb.PhoneNumber = obj.PhoneNumber;
                objFromDb.OccupationId = obj.OccupationId;
                objFromDb.IdentityCardNumber = obj.IdentityCardNumber;
                objFromDb.age = obj.age;
                objFromDb.CityId = obj.CityId;
                objFromDb.WardId = obj.WardId;
                objFromDb.DistrictId = obj.DistrictId;

            }
        }
    }
}
