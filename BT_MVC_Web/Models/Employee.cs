using BT_MVC_Web.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_MVC_Web.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

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


        [Required(ErrorMessage = "Vui lòng chọn dân tộc !")]
        public int EthnicityId { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn nghề nghiệp.")]
        public int OccupationId { get; set; }

        public string? IdentityCardNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public Gender Gender { get; set; }

        [Display(Name = "Phone Number ")]
        public string? PhoneNumber { get; set; }

        // navigate proprety

        [Required(ErrorMessage = "Vui lòng chọn thành phố!")]
        public int CityId { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn huyện!")]
        public int DistrictId { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn xã!")]
        public int WardId { get; set; }

        [ValidateNever]
        public District District { get; set; }

        [ValidateNever]
        public Ethnicity Ethnicity { get; set; }

        [ValidateNever]
        public Occupation Occupation { get; set; }

        [ValidateNever]
        public Ward Ward { get; set; }

        [ValidateNever]
        public City City { get; set; }


    }
}
