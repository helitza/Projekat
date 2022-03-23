using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models{


    public class KompanijaTribina{

        [JsonIgnore]

        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public  Kompanija Kompanija { get; set; }
        
        public Tribina Tribina { get; set; }

        
    }
}