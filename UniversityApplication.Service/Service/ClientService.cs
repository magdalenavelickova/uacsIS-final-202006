using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using BankApplication.Data;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BankApplication.Service.Service
{
    public class ClientService
    : IClientsRepository
    {
        private readonly IMapper _mapper;
        private readonly BankDataContext _dataContext;

        public ClientService(BankDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            var clients = _dataContext.Clients
                .Include(c => c.Address)
                .Include(c => c.Accounts);

            return _mapper.Map<IEnumerable<ClientDTO>>(clients);
        }

        public async Task<ClientDTO> GetClient(int clientId)
        {
            var client = await _dataContext.Clients
                .Include(c => c.Address)
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(x => x.Id == clientId);
            return _mapper.Map<ClientDTO>(client);
        }

        public ClientDTO SaveClient(ClientDTO client)
        {
            var newClient = _mapper.Map<Client>(client);

            _dataContext.Clients.Add(newClient);
            _dataContext.SaveChanges();

            return _mapper.Map<ClientDTO>(newClient);
        }

        public bool DeleteClient(int clientId)
        {
            var client = _dataContext.Clients.FirstOrDefault(x => x.Id == clientId);

            if (client == null)
            {
                return false;
            }

            _dataContext.Clients.Remove(client);
            _dataContext.SaveChanges();
            return true;
        }

        public ClientDTO PutClient(int id, ClientDTO clientObject)
        {
            var client = _dataContext.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                throw new Exception("Client not found");
            }

            clientObject.Id = id;
            client = _mapper.Map<Client>(clientObject);
            _dataContext.SaveChanges();

            return _mapper.Map<ClientDTO>(client);
        }
    }
}
