using System.ComponentModel.DataAnnotations;

namespace SeamRipperData.Models
{
    public class ClientInfo
    {
        [Key] // Primary Key for EF Core (optional for now)
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = "";


        public List<ClientMeasurements> Measurements { get; set; } = new List<ClientMeasurements>();
    }
}
