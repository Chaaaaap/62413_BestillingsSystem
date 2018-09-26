using System;
using System.Data.SqlClient;
using Common;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/login/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserHandler _handler;

        public LoginController()
        {
            _handler = new UserHandler();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public ActionResult<User> Login([FromForm] Login userLogin)
        {
            try
            {
                var userFromDb = _handler.GetUserForLogin(userLogin.Username);

                if (userFromDb != null && userLogin.Password == userFromDb.Password)
                {
                    return Ok(userFromDb);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        ~LoginController()
        {
            _handler.Dispose();
        }
    }
}