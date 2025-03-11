using System.ComponentModel.DataAnnotations;

namespace WCRM.MODEL.AdminPanel
{
    public class OurTeam
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public required string Image { get; set; }
        public required string Designation { get; set; }
    }
}
