namespace WCRM.MODEL.AdminPanel
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ClientLogo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
