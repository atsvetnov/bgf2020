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
    public class UserFieldsMetaInfoController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public UserFieldsMetaInfoController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }

        private UserFieldsMetaInfo[] FillUserFieldsMetaInfo()
        {
            string sql = @"SELECT * FROM  FieldsMetaInfo WHERE Objectname='User'";
            DataTable dt = SqlHelper.GetDatatable(_context, sql);

            UserFieldsMetaInfo[] fmi = new UserFieldsMetaInfo[dt.Rows.Count];
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                fmi[k] = new UserFieldsMetaInfo();
                fmi[k].FieldName = (string)dt.Rows[k]["FieldName"];
                fmi[k].Caption = (string)dt.Rows[k]["Caption"];
                fmi[k].Mandatory = (bool)dt.Rows[k]["Mandatory"];
            }
            return fmi;
        }

        [HttpGet]
        public IEnumerable<UserFieldsMetaInfo> Get()
        {
            return FillUserFieldsMetaInfo().ToArray();
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class UserFieldsCaptionController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public UserFieldsCaptionController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
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
    public class SexController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public SexController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
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
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public ProfileController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }
      
        [HttpGet]
        public IEnumerable<Profile> Get()
        {
            if (Globals.Profile == null)
                Globals.FillGlobals(_context);
            return Globals.Profile;
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public TreeController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }

        [HttpGet]
        public TreeAdmin[] Get()
        {
           TreeAdmin[] userTree = HttpContext.Session.Get<TreeAdmin[]>("userTree");
            if (userTree == null) return null;

            return userTree;
        }

        [HttpGet("{parentId}")]
        public async Task<ActionResult<TreeAdmin[]>> GetTree(int parentId)
        {
           TreeAdmin[] userTree = HttpContext.Session.Get<TreeAdmin[]>("userTree");
            if (userTree == null) return null;
           return userTree.Where(t => t.ParentId == parentId).OrderBy(t => t.OrderBy).ToArray(); 
        }
    }


        [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private BgfAdminContext _context;


        public UserController(BgfAdminContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginController> logger)
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
        /*
         http://localhost/bgfadmin/Users
        {
             "Id":"e193059d-740b-48e4-acc5-47ac33230fc5",
             "ProfileId":2,
            "SexId":1,
             "Email":"atsvetnov@atalan.net",
             "LastName":"Tsvetnov",
             "FirstName":"Sacha",
             "MiddleName":"Sacha",
            "BirthDate":"2019-07-26T00:00:00"
         }
         */
        public async Task<ActionResult<UserShortInfoResult>> EditUser(UserShortInfo usersi)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(usersi);
            var result = System.Text.Json.JsonSerializer.Deserialize<UserShortInfoResult>(json);
            result.Success = false;

            DateTime datetimenow = DateTime.Now;
            if (usersi.Id == null) // create user
            {
                User user = new User();
                UserShortInfo.CopyUserShortInfoToUser(usersi, user);
                user.EmailConfirmed = true;
                user.CreationDate = datetimenow;
                user.LastModificationDate = datetimenow;
                string password = Utils.GenerateLoginPasswordRandomString(DateTime.Now.Millisecond);
                var res = await _userManager.CreateAsync(user, password);

                if (res.Succeeded)
                {
                    result.Success = true;
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        result.Error += error + "";
                    }
                    result.Success = false;
                }

            }
            else // modify user
            {
                var user = await _userManager.FindByIdAsync(usersi.Id);
                if(user == null)
                {
                    result.Error = "User not found";
                    return result;
                }
                if (!Utils.IsValidEmail(usersi.Email))
                {
                    result.Error = "Invalid Email";
                    return result;
                }

                UserShortInfo.CopyUserShortInfoToUser(usersi, user);
                user.LastModificationDate = datetimenow;

                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    result.Success = true;
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        result.Error += error + "";
                    }
                    result.Success = false;
                }

            }
            
 
            return result;
        }

    }

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
            if (Globals.Tree == null)
                Globals.FillGlobals(_context);

        }


        [HttpGet]
        //public async IEnumerable<TreeAdmin> Get()
        public async Task<ActionResult<LoginResult>> Get()
        //        public async IAsyncEnumerable<TreeAdmin> Get()
        {
            //            var user = await _userManager.FindByEmailAsync("atsvetnov@atalan.net");
            //            await _signInManager.SignInAsync(user, false);
            var sres = await _signInManager.PasswordSignInAsync("atsvetnov@atalan.net", "Ats$314271", false, false);
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

            try
            {
                if (!Utils.IsValidEmail(login.Email)) throw new Exception("Invalid Email");
                var sres = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if (!sres.Succeeded) throw new Exception("Invalid Email or password");
                var user = await _userManager.FindByEmailAsync(login.Email);
                bool signedin = _signInManager.IsSignedIn(User);

                TreeAdmin[] userTree = Globals.GetProfileTree(user.ProfileId);
                HttpContext.Session.Set("user", user);
                HttpContext.Session.Set("userTree", userTree);
                result.Success = true;
                result.Email = user.Email;
                result.FullName = user.LastName + " " + user.FirstName;

                //Globals.Tree.Where(t => t.ParentId == 0).OrderBy(t => t.OrderBy);
            }
            catch(Exception exc)
            {
                result.Success = false;
                result.Error = exc.Message;
            }

            return result;
        }

    }

}


