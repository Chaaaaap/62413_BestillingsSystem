using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers;

namespace WebAPI.Controllers
{
    [Authorize]
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
        public ActionResult<Item> GetItem(int id)
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
    }
}