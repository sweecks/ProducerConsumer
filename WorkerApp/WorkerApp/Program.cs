using EFCore.BulkExtensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerApp.DataLayer;
using WorkerApp.Models;
using WorkerApp.Models.DTOs;
using WorkerApp.Services.Redis;

namespace WorkerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            IRedisService redisService = new RedisService();
            EGTDbContext dbContext = new EGTDbContext();

            var productsList = new List<Product>();

            #region RabbitMQ
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "Prducts",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine("Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received message: {message}");

                if (message == "New items added!")
                {
                    // Reading the data from Redis
                    var data = redisService.GetSerializedList("productsList");
                    var dataToProductsList = JsonConvert.DeserializeObject<List<ProductDTO>>(data);

                    foreach (var product in dataToProductsList)
                    {
                        productsList.Add(
                                new Product 
                                {
                                    Name = product.Name,
                                    Description = product.Description,
                                    Price = product.Price,
                                    InStock = product.InStock
                                });
                    }

                    // Adding items to the DB
                    dbContext.BulkInsert(productsList);
                    dbContext.SaveChanges();
                }
            };
            channel.BasicConsume(queue: "Prducts",
                                 autoAck: true,
                                 consumer: consumer);
            #endregion

            Console.ReadKey();
        }
    }
}
