using BT_MVC_Web.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BT_MVC_Web.Models
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }

        [ValidateNever]
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        [ValidateNever]
        public IEnumerable<SelectListItem> Citys { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Districts { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Wards { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Occupations { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Ethnicity { get; set; }
    }
}
