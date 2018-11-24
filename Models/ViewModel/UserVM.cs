using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XBitApi.Models.ViewModel
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public string Password { get; set; }
        //UserInformation...
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Username { get; set; }

        public UserRolesVM UserClaimsRoles { get; set; }
        [Required]
        public AddressVM Address { get; set; }
        public string Url { get; set; }
    }
}
