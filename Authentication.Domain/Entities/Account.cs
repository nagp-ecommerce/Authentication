using System.ComponentModel.DataAnnotations;
using ProductService.Core.Entities;

namespace Authentication.Domain
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? AccountStatus { get; set; }
        //public List<Address> AddressList { get; set; }
        public List<Product>? WishList { get; set; }
        //public List<CreditCard> CreditCards { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}