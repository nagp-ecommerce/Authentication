namespace Authentication.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Cart { get; set; }

        public List<string> SearchLogs { get; set; }
    }
}
