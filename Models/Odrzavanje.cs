using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models{
    public class Odrzavanje{
        [Key]
        public int Id { get; set; }
        
        [Required]
 
        public DateTime vreme{get; set;}
        [Required]
 
        public Mesto mesto{get; set;}

        public List<Akreditacija> Akreditacije { get; set; }
        
        public Tribina Tribina { get; set; }

        [JsonIgnore]
        public Kompanija Kompanija { get; set; }
    }
}