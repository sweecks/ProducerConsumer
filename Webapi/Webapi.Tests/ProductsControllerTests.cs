using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapi.Controllers;
using Webapi.Models.DTOs;
using Webapi.Services.RabbitMQ;
using Webapi.Services.Redis;
using Xunit;

namespace Webapi.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IRedisService> _mockRedisService;
        private readonly Mock<IMessageProducer> _mockRabbitMQProducer;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockRedisService = new Mock<IRedisService>();
            _mockRabbitMQProducer = new Mock<IMessageProducer>();
            _controller = new ProductsController(_mockRedisService.Object, _mockRabbitMQProducer.Object);
        }

        [Fact]
        public async Task AddItemsToRedis_WhenCalled_ReturnsOkResult()
        {
            // Arange
            var productList = new List<ProductDTO>
                                    { 
                                        new ProductDTO
                                        { 
                                            Name = "Test prodcut 1",
                                            Description = "This is test product 1",
                                            Price = 3.50,
                                            InStock = 3
                                        },
                                        new ProductDTO
                                        {
                                            Name = "Test prodcut 2",
                                            Description = "This is test product 2",
                                            Price = 4.50,
                                            InStock = 5
                                        },
                                        new ProductDTO
                                        {
                                            Name = "Test prodcut 3",
                                            Description = "This is test product 3",
                                            Price = 5.50,
                                            InStock = 10
                                        }
                                    };

            // Act
            var okResult = await _controller.AddItemsToRedis(productList);

            // Assert
            Assert.IsType<OkResult>(okResult as OkResult);
        }

        [Fact]
        public async Task AddItemsToRedis_WhenCalled_ReturnsBadRequestResult()
        {
            // Arange
            var productList = new List<ProductDTO>
                                    {
                                        new ProductDTO
                                        {
                                            Description = "This is test product 1",
                                            Price = 3.50,
                                            InStock = 3
                                        },
                                    };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badRequestResult = await _controller.AddItemsToRedis(productList);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult as BadRequestObjectResult);
        }

        [Fact]
        public void ItemsAdded_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.ItemsAdded();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
    }
}
