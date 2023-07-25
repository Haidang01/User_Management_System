using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_MVC_Web.DTOs
{
    public class WardGetDto
    {
        public int WardId { get; set; }
        public string WardName { get; set; }
        public int? DistrictId { get; set; }
    }
}
