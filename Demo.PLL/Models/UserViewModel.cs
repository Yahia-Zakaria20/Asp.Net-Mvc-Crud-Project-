using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace Demo.PL.Models
{
	public class UserViewModel
	{
		public string Id { get; set; }

		[DisplayName("User Name")]
        public string? UserName  { get; set; }
		[Required]
		[DisplayName("First Name")]
        public string FName { get; set; }
		[Required]
		[DisplayName("Last Name")]
        public string LName { get; set; }
		[Required]
		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }

		public string? Email { get; set; }

		public IEnumerable<string>? Roles { get; set; }
		
	}
}
