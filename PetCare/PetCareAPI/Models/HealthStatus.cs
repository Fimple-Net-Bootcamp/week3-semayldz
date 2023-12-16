using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models
{
    public class HealthStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HealthStatusId { get; set; }
        public int PetId { get; set; }
        public string Status { get; set; }
    }
}
