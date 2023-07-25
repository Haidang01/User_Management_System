using BT_MVC_Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BT_MVC_Web.ViewModels
{
    public class EthnicityGetDto
    {
        public int EthnicityId { get; set; }

        public string EthnicityName { get; set; }

    }
}
