using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapi.Models.DTOs;
using Webapi.Services.RabbitMQ;
using Webapi.Services.Redis;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRedisService _redisService;
        private readonly IMessageProducer _messageProducer;

        private const string productsListCacheKey = "productsList";
        private const string itemsAddedQueueName = "Prducts";
        private const string itemsAddedMessage = "New items added!";

        public ProductsController(IRedisService redisService, IMessageProducer messageProducer)
        {
            this._redisService = redisService;
            this._messageProducer = messageProducer;
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddItemsToRedis(IEnumerable<ProductDTO> products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await this._redisService.AddList(productsListCacheKey, products);

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult ItemsAdded()
        {
            string response = "Message send succesfuly!";

            this._messageProducer.SendTextMessage(itemsAddedQueueName, itemsAddedMessage);

            return Ok(response);
        }
    }
}
