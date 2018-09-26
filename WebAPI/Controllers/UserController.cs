using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common;
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
        public IActionResult CreateUser([FromForm] Login user)
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

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser([FromBody] User user, int id)
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