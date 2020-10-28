using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using bgfadmin.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

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
        public IEnumerable<TreeAdmin> Get()
        //public JsonResult Get()
        {
            //bool signedIn = _signInManager.IsSignedIn(this.User);
            bool signedIn = false;
            if (User != null)
                signedIn = User.Identity.IsAuthenticated;

            //DataTable dt = Utils.SqlHelper.GetDatatable(_context, "SELECT * FROM Login");
            //Utils.SqlHelper.ExecuteSqlCommand(_context, "UPDATE Login SET IsComplete=1 WHERE Id=2");
            //var Logins = _context.Login.FromSqlRaw("SELECT * FROM dbo.Login").ToArray();
            //return Logins;

            var array = _context.TreeAdmin.ToArray();
            return array;
            //return new JsonResult("hello");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TreeAdmin>> GetLogin(int id)
        {
            var item = await _context.TreeAdmin.FindAsync(id);


            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login Login)
        {
            return Login;
        }

    }
}
