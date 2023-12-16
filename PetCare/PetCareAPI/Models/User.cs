using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonProperty("Pet")]
        public List<Pet> Pet { get; set; } = new List<Pet>();

    }
}
