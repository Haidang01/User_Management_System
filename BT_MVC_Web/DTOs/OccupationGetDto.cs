using BT_MVC_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.ViewModels
{
    public class OccupationGetDto
    {
        public int OccupationId { get; set; }

        public string OccupationName { get; set; }

    }
}
