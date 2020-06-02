using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;

namespace BankApplication.Data.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>()
                .ReverseMap();
        }
    }
}