using System.ComponentModel.DataAnnotations;

using BankApplication.Data.Validators;

namespace BankApplication.Data.DTOs
{
    public class AddressDTO
    {
        [IdNotSend(ErrorMessage = "You cannot input an Id of a address")]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

    }
}
