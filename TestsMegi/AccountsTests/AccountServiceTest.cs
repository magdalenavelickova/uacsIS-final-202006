using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Repositories;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniversityApplication.Tests.Internal;

namespace BankApplication.Test.AccountsTests
{
    [TestFixture]
    public class AccountServiceTest
    {
        public class AccountService
        {
            private IAccountsRepository _accountService;
            private readonly IMapper _mapper;

            public AccountService()
            {
                var config = new MapperConfiguration(mc =>
                {
                    mc.AddMaps("BankApplication.Data");
                });
                _mapper = config.CreateMapper();
            }

            [Test, Category("DB"), Category("Service")]
            public async Task GetById_Should_Return_Correct_Account()
            {
                // Arrange
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                var accountId = 1;

                // Act
                var actual = await _accountService.GetAccount(accountId);

                // Assert
                Assert.AreEqual(accountId, actual.Id);
            }

            [Test, Category("DB"), Category("Service")]
            public async Task GetById_Should_Return_Null_Account()
            {
                // Arrange
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                var accountId = 6;

                // Act
                var actual = await _accountService.GetAccount(accountId);

                // Assert
                Assert.IsNull(actual);
            }

            [Test, Category("DB"), Category("Service")]
            public async Task GetAccounts_Should_Return_Correct_Count()
            {
                // Arrange
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                const int accountCount = 4;

                // Act
                var actual = _accountService.GetAccounts();

                // Assert
                Assert.AreEqual(accountCount, actual.Count());
            }

            public async Task ShouldBeAbleToAddAccountAsync()
            {
                // Arrange 
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                AccountDTO account = new AccountDTO()
                {
                    Name = "Account",
                    Balance = 000,
                    Type = AccountType.SavingsAccount,
                    IsActive = false,
                    ClientId = 4
                };

                //Act
                var response = _accountService.SaveAccount(account);
                var item = dbContext.Accounts.Find(response.Id);

                // Assert
                Assert.AreEqual(item.Name, response.Name);
                Assert.AreEqual(item.Balance, response.Balance);
                Assert.AreEqual(item.Type, response.Type);
                Assert.AreEqual(item.IsActive, response.IsActive);
                Assert.AreEqual(item.ClientId, response.ClientId);
            }

            [Test, Category("DB"), Category("Service")]
            public async Task ShouldBeAbleToDeleteAccountAsync()
            {
                // Arrange 
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                int accountId = 1;

                //Act
                var response = _accountService.DeleteAccount(accountId);

                // Assert
                Assert.IsTrue(response);
                Assert.AreEqual(4, dbContext.Accounts.Count());
                Assert.IsNull(dbContext.Accounts.Find(accountId));
            }

            [Test, Category("DB"), Category("Service")]
            public async Task ShouldNotToDeleteAccountAsync()
            {
                // Arrange 
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                int accountId = 6;

                //Act
                var response = _accountService.DeleteAccount(accountId);

                // Assert
                Assert.IsFalse(response);
                Assert.AreEqual(5, dbContext.Accounts.Count());
                Assert.IsNull(dbContext.Accounts.Find(accountId));
            }

            [Test, Category("DB"), Category("Service")]
            public async Task ShouldBeAbleToUpdateAccountAsync()
            {
                // Arrange 
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                Account accountEntity = new Account()
                {
                    Name = "Account",
                    Balance = 000,
                    Type = AccountType.SavingsAccount,
                    IsActive = false,
                    ClientId = 4
                };
                dbContext.Accounts.Add(accountEntity);
                dbContext.SaveChanges();

                AccountDTO AccountDto = new AccountDTO()
                {
                    Name = "Account",
                    Balance = 000,
                    Type = AccountType.SavingsAccount,
                    IsActive = false,
                    ClientId = 4,
                    Id = accountEntity.Id
                };

                //Act
                var response = _accountService.PutAccount(accountEntity.Id, AccountDto);

                // Assert
                Assert.AreEqual(AccountDto.Name, response.Name);
                Assert.AreEqual(AccountDto.Balance, response.Balance);
                Assert.AreEqual(AccountDto.Type, response.Type);
                Assert.AreEqual(AccountDto.ClientId, response.ClientId);
            }

            [Test, Category("DB"), Category("Service")]
            public async Task ShouldNotUpdateAccountAsync()
            {
                // Arrange 
                using var factory = new SQLiteDbContextFactory();
                await using var dbContext = factory.CreateContext();
                _accountService = new BankApplication.Service.Service.AccountsService(dbContext, _mapper);
                AccountDTO accountDto = new AccountDTO()
                {
                    Name = "Account",
                    Balance = 000,
                    Type = AccountType.SavingsAccount,
                    IsActive = false,
                    ClientId = 4,
                    Id = 6
                };

                //Act
                var ex = Assert.Throws<Exception>(() => _accountService.PutAccount(accountDto.Id, accountDto));

                // Assert
                Assert.That(ex.Message == "Account not found");
            }
        }
    }
}
