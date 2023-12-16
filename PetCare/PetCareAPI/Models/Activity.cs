using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PetCareAPI.Models
{
    public class Activity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityId { get; set; }
        [JsonProperty("PetId")]
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public string ActivityName { get; set; }
    }
}
