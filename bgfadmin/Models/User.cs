using System;
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
    public class UserFieldsMetaInfo// : FieldsMetaInfo
    {
        [Key]
        public string FieldName { get; set; }
        public string Caption { get; set; }

        public bool Mandatory { get; set; }

    }

    public class User : IdentityUser
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int SexId { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string Phone { get; set; }
        public int Year { get; set; }

        public bool Deactivated { get; set; }

        public Nullable<DateTime> CreationDate { get; set; }
        public Nullable<DateTime> LastModificationDate { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
    }


    public class UserShortInfo
    {
        public string Id { get; set; }
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int SexId { get; set; }
        public string SexName { get; set; }
        public string Email{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
       
       public string BirthPlace { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone { get; set; }
        public bool Deactivated { get; set; }


        public static void CopyUserShortInfoToUser(UserShortInfo usersi, User user)
        {
            //user.Id = usersi.Id;
            user.UserName = usersi.Email;
            user.Email = usersi.Email;
            user.ProfileId = usersi.ProfileId;
            user.SexId = usersi.SexId;
            user.LastName = usersi.LastName;
            user.FirstName = usersi.FirstName;
            user.MiddleName = usersi.MiddleName;
            user.BirthDate = usersi.BirthDate;
            user.BirthPlace = usersi.BirthPlace;
            user.Phone = usersi.Phone;
            user.Address1 = usersi.Address1;
            user.Address2 = usersi.Address2;
            user.Deactivated = usersi.Deactivated;
        }

    }

    public class UserShortInfoResult: UserShortInfo
    {
        public bool Success { get; set; }
        public string Error { get; set; }

    }


    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Error { get; set; }
    }

}

