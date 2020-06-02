using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Data.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ClientType Type { get; set; }
        public int AddressId { get; set; }

        public Address Address { get; set; }
        public virtual IEnumerable<Account> Accounts { get; set; } = new List<Account>();
    }
}
