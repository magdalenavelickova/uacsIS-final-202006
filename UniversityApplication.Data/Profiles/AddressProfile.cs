using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;

namespace BankApplication.Data.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>()
                .ReverseMap();
        }
    }
}