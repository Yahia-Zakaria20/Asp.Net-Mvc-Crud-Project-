using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class RoleViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }

        [DisplayName("Role Name")]
        [Required(ErrorMessage = "Role Name Is Required")]
        public string RoleName { get; set; }


        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
