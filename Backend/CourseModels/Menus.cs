using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseModels
{
    public class Menu
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public string MenuName { get; set; }
    }
}
