using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bgf.Models
{
    public class Student : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender_ID { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string Phone { get; set; }
        public int Year { get; set; }
        public int StudentProfile_ID { get; set; }

        public bool Deactivated { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
