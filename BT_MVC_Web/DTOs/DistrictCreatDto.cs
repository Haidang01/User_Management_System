using BT_MVC_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.DTOs
{
    public class DistrictCreatDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên huyện.")]
        [Display(Name = "Tên huyện")]
        public string DistrictName { get; set; }

        [ForeignKey(nameof(CityId))]
        public int? CityId { get; set; }

        [ValidateNever]
        public City City { get; set; }

        [ValidateNever]
        public ICollection<Ward> Wards { get; set; }
    }
}
