using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers;

namespace WebAPI.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemHandler _handler;

        public ItemController()
        {
            _handler = new ItemHandler();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Item> GetItem(long id)
        {
            try
            {
                var itemFromDb = _handler.GetItem(id);

                if (itemFromDb != null)
                {
                    return Ok(itemFromDb);
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
        public ActionResult<List<Item>> GetAllItems()
        {
            try
            {
                var allItemsFromDb = _handler.GetAllItems();

                if (allItemsFromDb != null)
                {
                    return Ok(allItemsFromDb);
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
        public IActionResult CreateItem([FromBody] Item item)
        {
            try
            {
                _handler.CreateItem(item);

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
        public IActionResult UpdateItem([FromBody] Item item, int id)
        {
            try
            {
                _handler.UpdateItem(id, item);

                return Ok();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                _handler.DeleteItem(id);

                return Ok();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [HttpPost("{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AddItemPicture(long itemId, byte[] image)
        {
            try
            {
                _handler.AddItemPicture(itemId, image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }

            return Ok();
        }
    }
}