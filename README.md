# ProducerConsumer

Producer Project
Consumer Project
DB

## Structure of the assignment solution

In the repo you can find two folders, one is "Webapi" which cotains the solution project for the Producer, and the other is "WorkerApp" which is the solution project for the Consumer.

You can also find the bak "Products" file to easly restor the DB on your local machine.

## Prerequisites

In oreder to run the project on your local machine first you will need to make sure that you have some additional software.

RabbitMQ :
In order to use RabbitMQ local instance you will need to install Docker and Docker Desktop.
Here is a link for Docker instalation file - https://www.docker.com/products/docker-desktop/

After you install Docker you will need to set up RabbitMQ docker image.
Here you can find the documentation for it, follow the setps - https://www.rabbitmq.com/download.html

Redis :
For Redis local instance you will need Redis Server.
You can download the zip file from here - https://github.com/microsoftarchive/redis/releases/tag/win-3.0.504
Extract the zip file.

## Setup Projects

1. Restor the DB from the bak file into SSMS.
2. Start RabbitMQ container in Docker Desktop.
3. Start Redis server by running "redis-server". You can also run "redis-cli" to test the server.
4. Open "Webapi" solution and press F5 to run it.
5. Open "WorkerApp" solution and press F5 to run it.

## Implementation

The "Webapi" solution contains one ASP.NET core rest api projcet "Webapi" and one "Webapi.Tests" xUnit project for the unit tests.

In the "Webapi" project you can find one controller named "ProductsController" which contains two end points - "AddItemsToRedis" and "ItemsAdded".
In this project you can also find "Services" and "Models".

The "Webapi.Tests" contains unit tests related to the api endpoints.


The "WorkerApp" soluton contains four projects - "WorkerApp", "WorkerApp.DataLayer", "WorkerApp.Models", "WorkerApp.Services".

The "WorkerApp" project listens for messages from RabbitMQ, then if message is recieved it gets the list of objects from Redis, and then inserts the list into the DB.
The "WorkerApp.DataLayer" is where the DbContext is implemented.
In "WorkerApp.Models" we have our models.
In "WorkerApp.Services" we have the implementaton of Redis service.
