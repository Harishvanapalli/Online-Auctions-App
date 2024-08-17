using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Suspend { get; set; } = false;
    }
}
