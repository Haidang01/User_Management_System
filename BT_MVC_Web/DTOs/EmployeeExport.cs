using BT_MVC_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.DTOs
{
    public class EmployeeExport
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string DateOfBirth { get; set; }

        public int age { get; set; }

        public string IdentityCardNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string District { get; set; }

        public string Ethnicity { get; set; }

        public string Occupation { get; set; }

        public string Ward { get; set; }

        public string City { get; set; }
    }
}
