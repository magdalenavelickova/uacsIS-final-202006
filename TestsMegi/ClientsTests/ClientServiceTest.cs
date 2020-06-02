using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Repositories;
using BankApplication.Tests.Service;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniversityApplication.Tests.Internal;

namespace BankApplication.Test.ClientsTests
{
    [TestFixture]
    public class ClientServiceTest
    {
        private IClientsRepository _clientService;
        private readonly IMapper _mapper;

        public ClientServiceTest()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddMaps("BankApplication.Data");
            });
            _mapper = config.CreateMapper();
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Correct_Client()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            var clientId = 1;

            // Act
            var actual = await _clientService.GetClient(clientId);

            // Assert
            Assert.AreEqual(clientId, actual.Id);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Null_Client()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            var clientId = 6;

            // Act
            var actual = await _clientService.GetClient(clientId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetClients_Should_Return_Correct_Count()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            const int clientCount = 4;

            // Act
            var actual = _clientService. GetClients();

            // Assert
            Assert.AreEqual(clientCount, actual.Count());
        }

        public async Task ShouldBeAbleToAddClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            ClientDTO client = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "073666777",
                Mail = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 4
            };

            //Act
            var response = _clientService.SaveClient(client);
            var item = dbContext.Clients.Find(response.Id);

            // Assert
            Assert.AreEqual(item.Name, response.Name);
            Assert.AreEqual(item.PhoneNumber, response.PhoneNumber);
            Assert.AreEqual(item.Email, response.Mail);
            Assert.AreEqual(item.Type, response.Type);
            Assert.AreEqual(item.AddressId, response.AddressId);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToDeleteClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            int clientId = 1;

            //Act
            var response = _clientService.DeleteClient(clientId);

            // Assert
            Assert.IsTrue(response);
            Assert.AreEqual(4, dbContext.Clients.Count());
            Assert.IsNull(dbContext.Clients.Find(clientId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotToDeleteClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            int clientId = 6;

            //Act
            var response = _clientService.DeleteClient(clientId);

            // Assert
            Assert.IsFalse(response);
            Assert.AreEqual(5, dbContext.Clients.Count());
            Assert.IsNull(dbContext.Clients.Find(clientId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToUpdateClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            Client clientEntity = new Client()
            {
                Name = "Client",
                PhoneNumber = "073666777",
                Email = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 4
            };
            dbContext.Clients.Add(clientEntity);
            dbContext.SaveChanges();

            ClientDTO clientDto = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "073666777",
                Mail = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 4,
                Id = clientEntity.Id
            };

            //Act
            var response = _clientService.PutClient(clientEntity.Id, clientDto);

            // Assert
            Assert.AreEqual(clientDto.Name, response.Name);
            Assert.AreEqual(clientDto.PhoneNumber, response.PhoneNumber);
            Assert.AreEqual(clientDto.Mail, response.Mail);
            Assert.AreEqual(clientDto.Type, response.Type);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotUpdateClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _clientService = new BankApplication.Service.Service.ClientService(dbContext, _mapper);
            ClientDTO clientDto = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "073666777",
                Mail = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 4,
                Id = 6
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _clientService.PutClient(clientDto.Id, clientDto));

            // Assert
            Assert.That(ex.Message == "Client not found");
        }
    }


}
