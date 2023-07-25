using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.Models
{
    public class Ethnicity
    {
        [Key]
        public int EthnicityId { get; set; }
        public string EthnicityName { get; set; }
    }
}
