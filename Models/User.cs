using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DEMO1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } ="Client";

    }
}
