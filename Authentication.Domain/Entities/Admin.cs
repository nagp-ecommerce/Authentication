using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Authentication.Domain.Entities;

namespace Authentication.Domain
{
    public class Admin: User
    {
        [Required]
        public Account Account { get; set; }
        public List<string>? ProductLogs { get; set; }
    }
}
