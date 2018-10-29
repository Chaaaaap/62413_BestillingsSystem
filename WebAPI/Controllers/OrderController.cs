﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderHandler _handler;

        public OrderController()
        {
            _handler = new OrderHandler();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Order> GetOrder(int id)
        {
            try
            {
                var orderFromDb = _handler.GetOrder(id);

                if (orderFromDb != null)
                {
                    return Ok(orderFromDb);
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
        public ActionResult<List<Order>> GetAllOrders()
        {
            try
            {
                var allOrdersFromDb = _handler.GetAllOrders();

                if (allOrdersFromDb != null)
                {
                    return Ok(allOrdersFromDb);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return NotFound();
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Order>> GetAllUserOrders(int id)
        {
            try
            {
                var allUserOrdersFromDb = _handler.GetAllUserOrders(id);

                if (allUserOrdersFromDb != null)
                {
                    return Ok(allUserOrdersFromDb);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return NotFound();
        }

        [HttpGet("item/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Order>> GetAllItems(int id)
        {
            try
            {
                var allItemsFromDb = _handler.GetAllItems(id);

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
        public IActionResult CreateOrder([FromForm] Order order)
        {
            try
            {
                _handler.CreateOrder(order);

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
        public IActionResult UpdateOrder([FromForm] Order order, int id)
        {
            try
            {
                _handler.UpdateOrder(id, order);

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
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _handler.DeleteOrder(id);

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