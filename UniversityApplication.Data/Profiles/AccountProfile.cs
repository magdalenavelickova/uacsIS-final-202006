using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;

namespace BankApplication.Data.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>()
                .ReverseMap();
        }
    }
}
