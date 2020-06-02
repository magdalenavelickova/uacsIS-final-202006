
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Tests.Service;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Test.ClientsTests
{
    [TestFixture]
    class ClientsControllerTest
    {
        private readonly ClientService clientService;
        public ClientsControllerTest()
        {
            clientService = new ClientService();
        }

        [Test, Category("API")]
        public async Task ShouldReturnAllClientAsync()
        {
            // Arrange 

            //Act
            var response = await clientService.GetClient();

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<List<ClientDTO>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(5, clientResponse.Count);

        }

        [Test, Category("API")]
        public async Task ShouldReturnSpecificClientAsync()
        {
            // Arrange 
            const int clientId = 1;

            //Act
            var response = await clientService.GetClient($"GetAll/{clientId}");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(clientResponse);
            Assert.AreEqual(clientId, clientResponse.Id);
        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToDeleteClientAsync()
        {
            // Arrange 
            const int clientId = 6;

            //Act
            var response = await clientService.DeleteClient($"RemoveClient/{clientId}");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var deleteResponse = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("true", deleteResponse);
        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToUpdateClientsAsync()
        {
            // Arrange 
            ClientDTO client = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "073666777",
                Mail = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 4
            };

            //Act
            var response = await clientService.UpdateClient(client, $"UpdateClient/6");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("0001", clientResponse.Id);
            Assert.AreEqual(4, clientResponse.Address);

        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToAddNewClient()
        {
            // Arrange 
            ClientDTO client = new ClientDTO()
            {

                Name = "Client",
                PhoneNumber = "073666777",
                Mail = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 4
            };

            //Act
            var response = await clientService.NewClient(client);

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("test", clientResponse.PhoneNumber);
            Assert.AreEqual("client.test@mail.com", clientResponse.Mail);
        }


    }
}
