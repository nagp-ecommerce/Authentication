using System.Security.Principal;
using Authentication.Domain.Entities;

namespace Authentication.Domain
{
    public class Admin: User
    {
        public Account Account { get; set; }
        public List<string> ProductLogs { get; set; }
    }
}
