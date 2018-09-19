using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers;

namespace WebAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserHandler _handler;

        public LoginController()
        {
            _handler = new UserHandler();
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] Login userLogin)
        {
            try
            {
                var userFromDb = _handler.GetUser(userLogin.Username);

                if (userFromDb != null && userLogin.Password == userFromDb.Password)
                {
                    return Ok(userFromDb);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return Unauthorized();
        }
    }
}