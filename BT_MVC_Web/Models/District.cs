using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BT_MVC_Web.Models
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }

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
