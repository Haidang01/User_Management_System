using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;

namespace BT_MVC_Web.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void UpdateEmployee(Employee employee);
        Employee FindById(int id);
    }

}
