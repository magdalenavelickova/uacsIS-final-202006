using BankApplication.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace BankApplication.Data
{
    public class BankDataContext
        : DbContext
    {
        private readonly string _connStr;

        public BankDataContext(DbContextOptions<BankDataContext> options)
            : base(options)
        {
            var sqlServerOptionsExtension = options.FindExtension<SqlServerOptionsExtension>();
            if (sqlServerOptionsExtension != null)
            {
                _connStr = sqlServerOptionsExtension.ConnectionString;
            }
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(true)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode()
                    .IsRequired(false);

                entity.Property(e => e.Type)
                    .HasColumnName("ClientTypeId")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity.Property(e => e.AddressId)
                    .HasColumnName("AddressId")
                    .HasColumnType("int")
                    .IsRequired(true);

                entity
                    .HasOne(c => c.Address)
                    .WithMany(a => a.Clients)
                    .HasForeignKey(c => c.AddressId)
                    .IsRequired();

                entity
                    .HasMany(c => c.Accounts)
                    .WithOne(a => a.Client)
                    .HasForeignKey(a => a.ClientId)
                    .IsRequired(false);

                //entity
                //    .HasMany(c => c.Accounts);

            });

            //modelBuilder.Entity<Client>()
            //    .HasMany(c => c.Accounts)
            //    .WithOne(a => a.Client);

            modelBuilder.Entity<Account>(entity =>
            {
                entity
                    .HasKey(a => a.Id);

                entity
                    .Property(a => a.Name)
                    .HasMaxLength(400)
                    .IsUnicode()
                    .IsRequired();

                entity
                    .Property(a => a.Type)
                    .HasColumnName("AccountTypeId")
                    .HasColumnType("int")
                    .IsRequired();

                entity
                    .Property(a => a.Balance)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired(true);

                entity
                    .Property(a => a.IsActive)
                    .HasColumnType("bit")
                    .IsRequired(true);

                entity.Property(a => a.ClientId)
                    .HasColumnType("int")
                    .IsRequired(true);

                entity
                    .HasOne(a => a.Client)
                    .WithMany(c => c.Accounts)
                    .HasForeignKey(a => a.ClientId)
                    .IsRequired(true);


            });

            //modelBuilder.Entity<Account>()
            //    .HasOne(a => a.Client)
            //    .WithMany(c => c.Accounts);


            modelBuilder.Entity<Address>(entity =>
            {
                entity
                    .HasKey(a => a.Id);

                entity
                    .Property(a => a.Street)
                    .HasMaxLength(400)
                    .IsUnicode()
                    .IsRequired();

                entity
                    .Property(a => a.City)
                    .HasMaxLength(400)
                    .IsUnicode()
                    .IsRequired();

                entity
                    .Property(a => a.Country)
                    .HasMaxLength(400)
                    .IsUnicode()
                    .IsRequired();

                entity
                    .HasMany(a => a.Clients)
                    .WithOne(c => c.Address);

            });

            modelBuilder.Entity<Address>()
                .HasData(
                    new Address()
                    {
                        Id = 1,
                        Street = "Killdeer Pass",
                        City = "Stockholm",
                        Country = "Sweden"
                    },
                    new Address
                    {
                        Id = 2,
                        Street = "Ridgeway Parkway",
                        City = "London",
                        Country = "United Kingdom"
                    },
                    new Address
                    {
                        Id = 3,
                        Street = "Southridge Hill",
                        City = "New York",
                        Country = "United States"
                    },
                    new Address
                    {
                        Id = 4,
                        Street = "Forest Park",
                        City = "Tokyo",
                        Country = "Japan"
                    });

            modelBuilder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = 1,
                        Name = "Nicoline Abspoel",
                        PhoneNumber = "077-999-999",
                        Type = ClientType.Residential,
                        Email = "NicolineAbspoel@gmail.com",
                        AddressId = 1
                    },
                    new Client
                    {
                        Id = 2,
                        Name = "Andrew Kennard",
                        PhoneNumber = "+38976999999",
                        Type = ClientType.Residential,
                        Email = "akennard@firm.com",
                        AddressId = 2

                    },
                    new Client
                    {
                        Id = 3,
                        Name = "Google",
                        PhoneNumber = "1111111111111",
                        Type = ClientType.Business,
                        Email = "info@google.com",
                        AddressId = 3

                    },
                    new Client
                    {
                        Id = 4,
                        Name = "Microsoft",
                        PhoneNumber = "32-3231-354",
                        Type = ClientType.Business,
                        Email = "info@microsoft.com",
                        AddressId = 4
                    });

            modelBuilder.Entity<Account>()
                .HasData(
                    new Account
                    {
                        Id = 1,
                        Name = "Personal Account",
                        Type = AccountType.SavingsAccount,
                        Balance = 7900m,
                        IsActive = true,
                        ClientId = 1
                    },
                    new Account
                    {
                        Id = 2,
                        Name = "MasterCard",
                        Type = AccountType.CreditCard,
                        Balance = 1m,
                        IsActive = false,
                        ClientId = 2
                    },
                    new Account
                    {
                        Id = 3,
                        Name = "MasterCard",
                        Type = AccountType.CreditCard,
                        Balance = 5688.40m,
                        IsActive = true,
                        ClientId = 2
                    },
                    new Account
                    {
                        Id = 4,
                        Name = "Housing Load",
                        Type = AccountType.Loan,
                        Balance = -55000.40m,
                        IsActive = true,
                        ClientId = 2
                    },
                    new Account
                    {
                        Id = 7,
                        Name = "Salary Account",
                        Type = AccountType.CurrentAccount,
                        Balance = 200500.50m,
                        IsActive = true,
                        ClientId = 4
                    },
                    new Account
                    {
                        Id = 8,
                        Name = "Cash Management",
                        Type = AccountType.SavingsAccount,
                        Balance = 433833.23m,
                        IsActive = true,
                        ClientId = 4
                    },
                    new Account
                    {
                        Id = 5,
                        Name = "Salary Account",
                        Type = AccountType.CurrentAccount,
                        Balance = 240000.00m,
                        IsActive = true,
                        ClientId = 3
                    },
                    new Account
                    {
                        Id = 6,
                        Name = "Cash Management",
                        Type = AccountType.SavingsAccount,
                        Balance = 500000.70m,
                        IsActive = true,
                        ClientId = 3
                    });
        }
    }
}
