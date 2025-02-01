using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string CartId { get; set; }

        public List<string>? SearchLogs { get; set; }
    }
}
