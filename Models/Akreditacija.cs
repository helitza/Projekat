using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models{
    public class Akreditacija{
        [Key]
        public int Id { get; set; }
      
        [Required]
        public Sediste sediste{get; set;}

        
        public Korisnik korisnik{get; set;}
        
        [JsonIgnore]
        public Odrzavanje Odrzavanje { get; set; }
    }
}