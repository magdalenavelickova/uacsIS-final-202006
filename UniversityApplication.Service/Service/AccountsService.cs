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
    public class AccountsService
    : IAccountsRepository
    {
        private readonly IMapper _mapper;
        private readonly BankDataContext _dataContext;

        public AccountsService(BankDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IEnumerable<AccountDTO> GetAccounts()
        {
            var accounts = _dataContext.Accounts
                .Include(a => a.Client);

            return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }

        public async Task<AccountDTO> GetAccount(int accountId)
        {
            var account = await _dataContext.Accounts
                    .Include(a => a.Client)
                .FirstOrDefaultAsync(x => x.Id == accountId);
            return _mapper.Map<AccountDTO>(account);
        }

        public AccountDTO SaveAccount(AccountDTO account)
        {
            var newAccount = _mapper.Map<Account>(account);

            _dataContext.Accounts.Add(newAccount);
            _dataContext.SaveChanges();

            return _mapper.Map<AccountDTO>(newAccount);
        }

        public bool DeleteAccount(int accountId)
        {
            var account = _dataContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            if (account == null)
            {
                return false;
            }

            _dataContext.Accounts.Remove(account);
            _dataContext.SaveChanges();
            return true;
        }

        public AccountDTO PutAccount(int id, AccountDTO accountObject)
        {
            var account = _dataContext.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            accountObject.Id = id;
            account = _mapper.Map<Account>(accountObject);
            _dataContext.SaveChanges();

            return _mapper.Map<AccountDTO>(account);
        }
    }
}