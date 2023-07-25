using BT_MVC_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.DTOs
{
    public class DistrictGetDto
    {
        public int DistrictId { get; set; }

        public string DistrictName { get; set; }
        public int? CityId { get; set; }
    }
}
