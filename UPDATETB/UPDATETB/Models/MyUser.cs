using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UPDATETB.Models
{
    public class MyUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
    }
}
