using BT_MVC_Web.DTOs;
using BT_MVC_Web.Models;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services.Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeeAsync(int page, int pageSize, string? includeProperties = null);
        Task<Employee>? GetEmployeeAsync(Expression<Func<Employee, bool>> filter, string? includeProperties = null);
        Task AddEmployeeAsync(NewEmployeeDto employee);
        void UpdateEmployee(Employee employee);
        Task RemoveEmployee(Employee employee);

        Employee FindById(int id, string? includeProperties = null);

        Task<List<EmployeeExport>> GetSelectedEmployee(string[] selectedIds);
        Task ImportFromExcel(IFormFile file);

    }
}
