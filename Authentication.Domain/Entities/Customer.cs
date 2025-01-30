using System.Security.Principal;
using Authentication.Domain.Entities;

namespace Authentication.Domain
{
    public class Customer: User
    {
        public Account Account { get; set; }
        //public Order order { get; set; }
    }
}
