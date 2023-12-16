using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models
{
    public class Pet
    {
        //id yi  veri eklerken kendim girmek istiyorum = [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PetId { get; set; }

        //Foreign Keys
        
        [JsonProperty("UserId")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public User? User { get; set; }


        [JsonProperty("HealthStatusId")]
        [ForeignKey("HealthStatus")]
        public int HealthStatusId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public HealthStatus? HealthStatus { get; set; }

        
        public string Type { get; set; }
        public string Name { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Food> Foods { get; set; } = new List<Food>();
    }
}
