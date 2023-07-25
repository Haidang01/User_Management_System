using BT_MVC_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BT_MVC_Web.Enums;

namespace BT_MVC_Web.DTOs
{
    public class NewEmployeeDto
    {
        [MaxLength(30, ErrorMessage = "Tên phải dưới 30 kí tự!")]
        [Required(ErrorMessage = "Vui lòng nhập tên !")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập ngày sinh !")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh!")]
        [Range(0, 110, ErrorMessage = "Tuổi không hợp lệ hãy nhập lại ngày sinh!")]
        public int age { get; set; }


        [ForeignKey(nameof(EthnicityId))]
        [Required(ErrorMessage = "Vui lòng chọn dân tộc !")]
        public int EthnicityId { get; set; }

        [ForeignKey(nameof(OccupationId))]
        [Required(ErrorMessage = "Vui lòng chọn nghề nghiệp.")]
        public int OccupationId { get; set; }

        public string? IdentityCardNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public Gender Gender { get; set; }

        [Display(Name = "Phone Number ")]
        public string? PhoneNumber { get; set; }

        public bool NoIdentityCardNumber { get; set; }
        public bool NoPhoneNumber { get; set; }


        // navigate proprety

        [ForeignKey(nameof(CityId))]
        [Required(ErrorMessage = "vui lòng chọn thành phố!")]
        public int? CityId { get; set; }


        [ForeignKey(nameof(DistrictId))]
        [Required(ErrorMessage = "Vui lòng chọn huyện!")]
        public int? DistrictId { get; set; }


        [ForeignKey(nameof(WardId))]
        [Required(ErrorMessage = "Vui lòng chọn xã!")]
        public int? WardId { get; set; }

        [ValidateNever]
        public Occupation Occupation { get; set; }

        [ValidateNever]
        public Ethnicity Ethnicity { get; set; }
        [ValidateNever]
        public City City { get; set; }

        [ValidateNever]
        public City Ward { get; set; }

        [ValidateNever]
        public District District { get; set; }
    }
}
