using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Data.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual IEnumerable<Client> Clients { get; set; } = new List<Client>();
    }
}