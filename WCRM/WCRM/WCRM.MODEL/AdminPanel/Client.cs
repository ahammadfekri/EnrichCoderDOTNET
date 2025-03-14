namespace WCRM.MODEL.AdminPanel
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public string ClientLogo { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //SEO Info
        public string SEO_Title { get; set; }
        public string SEO_Description { get; set; }
        public string SEO_Keywords { get; set; }
    }
}
