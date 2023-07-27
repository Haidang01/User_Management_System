using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [MaxLength(30, ErrorMessage = "Tên phải dưới 30 kí tự!")]
        [Required(ErrorMessage = "Vui lòng nhập tên thành phố.")]
        [Display(Name = "Tên thành phố")]
        public string CityName { get; set; }

        [ValidateNever]
        public ICollection<District> Districts { get; set; }
    }
}
