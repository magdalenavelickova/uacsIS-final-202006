using System.ComponentModel.DataAnnotations;

using BankApplication.Data.Models;

namespace BankApplication.Data.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to enter a Name")]
        [StringLength(200)]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        [Required(ErrorMessage = "You have to enter a type")]
        public AccountType Type { get; set; }

        public bool IsActive { get; set; }


        [Required(ErrorMessage = "You have to add an Client")]
        public int ClientId { get; set; }


        public virtual ClientDTO Exam { get; set; }
    }
}
