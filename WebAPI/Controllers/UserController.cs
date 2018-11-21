using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers;

namespace WebAPI.Controllers
{
    
    [Produces("application/json")]
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserHandler _handler;

        public UserController()
        {
            _handler = new UserHandler();
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                var userFromDb = _handler.GetUser(id);

                if (userFromDb != null)
                {
                    return Ok(userFromDb);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return NotFound();
        }

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                var allUsersFromDb = _handler.GetAllUsers();

                if (allUsersFromDb != null)
                {
                    return Ok(allUsersFromDb);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        public IActionResult CreateUser([FromForm] User user)
        {
            try
            {
                _handler.CreateUser(user);

                return Ok();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);

                return Unauthorized();
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser([FromForm] User user, int id)
        {
            try
            {
                _handler.UpdateUser(id, user);

                return Ok();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _handler.DeleteUser(id);

                return Ok();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }
    }
}