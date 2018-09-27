using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userFromDb.Id.ToString())
                        }),
                        Expires = DateTime.Now.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    userFromDb.Token = tokenHandler.WriteToken(token);
                    userFromDb.LatestLogin = DateTime.Now;
                    _handler.UpdateUser(userFromDb.Id, userFromDb);

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