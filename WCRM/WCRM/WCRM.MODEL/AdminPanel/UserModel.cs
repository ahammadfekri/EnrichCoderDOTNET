using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRM.MODEL.AdminPanel
{
    public class UserModel
    {
        [Key]
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Enter Your User Name")]
        [StringLength(10, ErrorMessage = "User Name should be less than or equal to ten characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        [StringLength(100, ErrorMessage = "Password should be less than or equal to 100 characters.")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "Enter Your Full Name")]
        [StringLength(50, ErrorMessage = "Name should be less than or equal to 50 characters.")]
        public string? FullName { get; set; }

        public string? Salt { get; set; } // Store salt for password hashing
    }
}
