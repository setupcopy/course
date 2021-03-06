using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDtos
{
    public class UserUpdateDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmed { get; set; }
        [Required]
        public string Role { get; set; }
        public string NickName { get; set; }
    }
}
