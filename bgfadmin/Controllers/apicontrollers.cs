using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using bgfadmin;
using bgfadmin.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Reflection;
using System.Collections;

namespace bgfadmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public LoginController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }


        [HttpGet]
        //public async IEnumerable<TreeAdmin> Get()
 public async Task<ActionResult<LoginResult>> Get()
//        public async IAsyncEnumerable<TreeAdmin> Get()
        {
//            var user = await _userManager.FindByEmailAsync("atsvetnov@atalan.net");
//            await _signInManager.SignInAsync(user, false);
            var sres = await _signInManager.PasswordSignInAsync("atsvetnov@atalan.net","Ats$314271",false,false);
            //bool signedIn = _signInManager.IsSignedIn(this.User);
            bool signedIn = false;
            if (User != null)
                signedIn = User.Identity.IsAuthenticated;

            LoginResult result = new LoginResult();
            return result;
            var array = _context.TreeAdmin.ToArray();
            //return array;
            //return new JsonResult("hello");
        }



        [HttpPost]
//        [ValidateAntiForgeryToken]
        public async Task<ActionResult<LoginResult>> PostLogin(LoginData login)
        {
            LoginResult result = new LoginResult();
            if(!Utils.IsValidEmail(login.Email))
            {
                result.Success = false;
                result.Error = "Invalid Email";
                return result;
            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            //var sres = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
            if (user != null)
            {
                try
                {
                    await _signInManager.SignInAsync(user, false);
                    bool signedin = _signInManager.IsSignedIn(User);
                    HttpContext.Session.Set("User", user );
                    result.Success = true;
                    result.Email = user.Email;
                    result.FullName = user.LastName + " " + user.FirstName;
                }
                catch 
                {
                    result.Success = false;

                }
            }

              return result;
        }

    }


    [ApiController]
    [Route("[controller]")]
    public class UserFieldCaptionsController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public UserFieldCaptionsController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }

        private Hashtable GetCaptions()
        {
            string sql = @"
                            SELECT FieldName, Caption
                            FROM FieldsMetaInfo WHERE ObjectName='User'
                            ORDER BY FieldName
                            ";
            DataTable dt = SqlHelper.GetDatatable(_context, sql);

            Hashtable ht = new Hashtable();
            foreach(DataRow dr in dt.Rows)
            {
                ht.Add((string)dr["FieldName"], (string)dr["Caption"]);
            }

            return ht;
        }
        [HttpGet]
        public JsonResult Get()
        {
            Hashtable ht = GetCaptions();


            return new JsonResult(ht);
        }
    }


    [ApiController]
    [Route("[controller]")]
    public class SexesController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public SexesController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }

        [HttpGet]
        public IEnumerable<Sex> Get()
        {
            var array = _context.Sex.ToArray();
            return array;
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public ProfilesController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }
      
        [HttpGet]
        public IEnumerable<Profile> Get()
        {
            var array = _context.Profile.ToArray();
            return array;
        }

    }

 
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public UsersController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }
        private UserShortInfo[] FillUserShortInfo0(DbSet<User> dbUser)
        {
            string sql = @"
                            SELECT AspNetUsers.Id, LastName, FirstName, Profile.Name AS ProfileName FROM AspNetUsers 
                            INNER JOIN Profile ON AspNetUsers.ProfileId = Profile.Id
                            ORDER BY LastName, FirstName
                            ";
            DataTable dt = SqlHelper.GetDatatable(_context, sql);


            User[] users = dbUser.ToArray();
            UserShortInfo[] susers = new UserShortInfo[users.Length];
            for(int k=0; k< users.Length; k++)
            {
                susers[k] = new UserShortInfo();
                susers[k].Id = users[k].Id;
                susers[k].Email = users[k].Email;
                susers[k].FirstName = users[k].FirstName;
                susers[k].LastName = users[k].LastName;
            }
            return susers;
        }

        private UserShortInfo[] FillUserShortInfo(DbSet<User> dbUser, string OrderBy)
        {
            string sql = @"
                            SELECT AspNetUsers.Id AS Id, Email, LastName, FirstName, MiddleName, BirthDate, Address1, Address2, Phone, Deactivated, Profile.Name AS ProfileName, Sex.Name AS SexName FROM AspNetUsers 
                            INNER JOIN Profile ON AspNetUsers.ProfileId = Profile.Id
                            INNER JOIN Sex ON AspNetUsers.SexId = Sex.Id
                            ORDER BY " + OrderBy;
                            
            DataTable dt = SqlHelper.GetDatatable(_context, sql);


            
            UserShortInfo[] susers = new UserShortInfo[dt.Rows.Count];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                susers[k] = new UserShortInfo();
                susers[k].Id = dt.Rows[k]["Id"].ToString();
                susers[k].Email = dt.Rows[k]["Email"].ToString();
                susers[k].ProfileName = dt.Rows[k]["ProfileName"].ToString();
                susers[k].SexName = dt.Rows[k]["SexName"].ToString();
                susers[k].FirstName = dt.Rows[k]["FirstName"].ToString();
                susers[k].LastName = dt.Rows[k]["LastName"].ToString();
                susers[k].MiddleName = dt.Rows[k]["MiddleName"].ToString();
                if(!dt.Rows[k].IsNull("BirthDate"))
                    susers[k].BirthDate = (DateTime)dt.Rows[k]["BirthDate"];
                susers[k].Address1 = dt.Rows[k]["Address1"].ToString();
                susers[k].Address2 = dt.Rows[k]["Address2"].ToString();
                if(!dt.Rows[k].IsNull("Deactivated"))
                    susers[k].Deactivated = (bool)dt.Rows[k]["Deactivated"];
                susers[k].ProfileName = dt.Rows[k]["ProfileName"].ToString();
            }
            return susers;
        }

        //public IEnumerable<User> Get()
        public JsonResult Get()
        {
            string OrderBy = "LastName, FirstName";
            if (this.HttpContext.Request.Query.ContainsKey("OrderBy"))
                OrderBy = this.HttpContext.Request.Query["OrderBy"];
             UserShortInfo[] susers = FillUserShortInfo(_context.User, OrderBy);
 
            //http://localhost/bgfadmin/users
            return new JsonResult(susers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var usr = await _context.User.FindAsync(id);


            if (usr == null)
            {
                return NotFound();
            }

            return usr;
        }

        [HttpPost]
        //        [ValidateAntiForgeryToken]
        public async Task<ActionResult<UserShortInfo>> EditUser(UserShortInfo usersi)
        {
            if(usersi.Id == null) // create user
            {

            }
            else // modify user
            {
                var user = await _userManager.FindByIdAsync(usersi.Id);
                if(user != null)
                {
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
            if (!Utils.IsValidEmail(usersi.Email))
            {
                usersi.Email = null;
                return usersi;
            }
 
            return usersi;
        }

    }

}
