using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AuthenticationResult
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public int Expires_In { get; set; } = 0;
    }
}
