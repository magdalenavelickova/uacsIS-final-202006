using System.Collections.Generic;
using System.Threading.Tasks;
using BankApplication.Data.DTOs;

namespace BankApplication.Service.Repositories
{
    public interface IClientsRepository
    {
        IEnumerable<ClientDTO> GetClients();

        Task<ClientDTO> GetClient(int clientId);

        ClientDTO SaveClient(ClientDTO client);

        bool DeleteClient(int clientId);

        ClientDTO PutClient(int id, ClientDTO client);
    }
}