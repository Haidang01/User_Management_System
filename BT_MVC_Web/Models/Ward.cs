using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BT_MVC_Web.Models
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên xã.")]
        [Display(Name = "Tên xã")]
        public string WardName { get; set; }

        [ForeignKey(nameof(DistrictId))]
        public int? DistrictId { get; set; }

        [ValidateNever]
        public District District { get; set; }
    }
}
