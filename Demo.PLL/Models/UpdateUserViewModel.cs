using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Demo.PL.Models
{
    public class UpdateUserViewModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LName { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
