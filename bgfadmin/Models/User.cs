﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace bgfadmin.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public int BaseProfileId { get; set; }
    }
    public class Sex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OredrBy { get; set; }
    }
    public class User : IdentityUser
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int SexId { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string Phone { get; set; }
        public int Year { get; set; }

        public bool Deactivated { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
