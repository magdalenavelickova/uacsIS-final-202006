using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Data.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
