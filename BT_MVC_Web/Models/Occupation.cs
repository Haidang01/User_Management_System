using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.Models
{
    public class Occupation
    {
        [Key]
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }
    }
}
