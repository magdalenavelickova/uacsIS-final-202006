using System.Collections.Generic;
using System.Threading.Tasks;

using BankApplication.Data.DTOs;

namespace BankApplication.Service.Repositories
{
    public interface IAccountsRepository
    {
        IEnumerable<AccountDTO> GetAccounts();

        Task<AccountDTO> GetAccount(int accountId);

        AccountDTO SaveAccount(AccountDTO account);

        bool DeleteAccount(int accountId);

        AccountDTO PutAccount(int id, AccountDTO account);
    }
}