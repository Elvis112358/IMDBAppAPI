using BLL;
using DAL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDal userDal;
        private readonly IApiValidationBLL apiValidationBll;
        public UsersController(
            IApiValidationBLL apiValidationBll,
            IUserDal userDal)
        {
            this.apiValidationBll = apiValidationBll;
            this.userDal = userDal;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            apiValidationBll.ValidateAndUpdateNewUserCredentials(user);
            user = userDal.AddUser(user);
            user.Password = null;
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(string username, string password)
        {
            if (username == null || password == null)
                throw new Exception("Username or password missing");
            User user = userDal.GetUserByUsername(username);
            apiValidationBll.ValidateUserCredentials(user, password);
            string token = apiValidationBll.GenerateToken(user);
            return Ok(new { id = user.Id, username = user.Username, role = user.Role, token });
        }
    }
}
