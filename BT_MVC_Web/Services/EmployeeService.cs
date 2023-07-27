using AutoMapper;
using BT_MVC_Web.DTOs;
using BT_MVC_Web.Helpers;
using BT_MVC_Web.Models;
using BT_MVC_Web.Repositories.Interface;
using BT_MVC_Web.Services.Interface;
using OfficeOpenXml;
using System.Linq.Expressions;

namespace BT_MVC_Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddEmployeeAsync(NewEmployeeDto obj)
        {
            var IdentityCardNumberExists = await _unitOfWork.Employee.GetAsync(e => e.IdentityCardNumber == obj.IdentityCardNumber);
            var PhoneNumberExists = await _unitOfWork.Employee.GetAsync(e => e.PhoneNumber == obj.PhoneNumber);
            if (obj.IdentityCardNumber != null && IdentityCardNumberExists != null)
            {
                throw new Exception("CCCD/CMND đã tồn tại!");
            }
            if (obj.PhoneNumber != null && PhoneNumberExists != null)
            {
                throw new Exception("Số điện thoại đã tồn tại!");
            }

            Employee employee = _mapper.Map<Employee>(obj);
            await _unitOfWork.Employee.AddAsync(employee);
            await _unitOfWork.SaveAsync();
        }

        public Employee FindById(int id, string? includeProperties = null)
        {
            return _unitOfWork.Employee.FindById(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync(int page, int pageSize, string? includeProperties = null)
        {
            return await _unitOfWork.Employee.GetAllAsync(page, pageSize, includeProperties);
        }

        public async Task<Employee> GetEmployeeAsync(Expression<Func<Employee, bool>> filter, string? includeProperties = null)
        {
            return await _unitOfWork.Employee.GetAsync(filter, includeProperties);
        }

        public async Task RemoveEmployee(Employee employee)
        {
            _unitOfWork.Employee.Remove(employee);
            await _unitOfWork.SaveAsync();
        }

        public void UpdateEmployee(Employee obj)
        {
            var IdentityCardNumberExists = _unitOfWork.Employee.Get(e => e.IdentityCardNumber == obj.IdentityCardNumber);
            var PhoneNumberExists = _unitOfWork.Employee.Get(e => e.PhoneNumber == obj.PhoneNumber);
            if (obj.IdentityCardNumber != null && IdentityCardNumberExists != null && IdentityCardNumberExists.Id != obj.Id)
            {
                throw new Exception("CCCD/CMND đã tồn tại!");
            }
            if (obj.PhoneNumber != null && PhoneNumberExists != null && PhoneNumberExists.Id != obj.Id)
            {
                throw new Exception("Số điện thoại đã tồn tại!");
            }

            _unitOfWork.Employee.UpdateEmployee(obj);
            _unitOfWork.Save();
        }
        public async Task<List<EmployeeExport>> GetSelectedEmployee(string[] selectedIds)
        {
            List<EmployeeExport> employees = new List<EmployeeExport>();
            for (int i = 0; i < selectedIds.Length; i++)
            {
                var employee = await _unitOfWork.Employee.GetAsync(e => e.Id == (int.Parse(selectedIds[i])), "Ethnicity,Occupation,District,Ward,City");

                employees.Add(new EmployeeExport
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    age = employee.age,
                    DateOfBirth = employee.DateOfBirth.ToString(),
                    IdentityCardNumber = employee.IdentityCardNumber,
                    PhoneNumber = employee.PhoneNumber,
                    District = employee.District.DistrictName,
                    City = employee.City.CityName,
                    Ethnicity = employee.Ethnicity.EthnicityName,
                    Occupation = employee.Occupation.OccupationName,
                    Ward = employee.Ward.WardName
                });
            }
            return employees;
        }

        public async Task ImportFromExcel(IFormFile file)
        {
            List<Employee> employees = new List<Employee>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var districtEmployee = await _unitOfWork.District.GetAsync(c => c.DistrictName == worksheet.Cells[row, 7].Value.ToString());
                    var ethnicityEmployee = await _unitOfWork.Ethnicity.GetAsync(c => c.EthnicityName == worksheet.Cells[row, 8].Value.ToString());
                    var occupationEmployee = await _unitOfWork.Occupation.GetAsync(c => c.OccupationName == worksheet.Cells[row, 9].Value.ToString());
                    var wardEmployee = await _unitOfWork.Ward.GetAsync(c => c.WardName == worksheet.Cells[row, 10].Value.ToString());
                    var cityEmployee = await _unitOfWork.City.GetAsync(c => c.CityName == worksheet.Cells[row, 11].Value.ToString());

                    Employee employee = new Employee
                    {
                        Name = worksheet.Cells[row, 2].Value?.ToString(),
                        DateOfBirth = DateTime.Parse(worksheet.Cells[row, 3].Value?.ToString()),
                        age = int.Parse(worksheet.Cells[row, 4].Value?.ToString()),
                        IdentityCardNumber = worksheet.Cells[row, 5].Value?.ToString(),
                        PhoneNumber = worksheet.Cells[row, 6].Value?.ToString(),
                        CityId = cityEmployee.CityId,
                        DistrictId = districtEmployee.DistrictId,
                        EthnicityId = ethnicityEmployee.EthnicityId,
                        OccupationId = occupationEmployee.OccupationId,
                        WardId = wardEmployee.WardId,
                    };
                    employees.Add(employee);
                }
            }
            await _unitOfWork.Employee.AddRangeAsync(employees);
            await _unitOfWork.SaveAsync();
        }
    }
}
