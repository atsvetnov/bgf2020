using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bgfadmin.Models
{
    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginResult
    {
        public bool Success  { get; set; }
        public string Email { get; set; }
        public string FullName  { get; set; }
        public string Error { get; set; }
    }

}
