using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Tribina
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string naziv { get; set; }
        [Required]
        [MaxLength(50)]

        public string sektor { get; set; }


        [Required]

        public DateTime datumPocetka { get; set; }

        [Required]

        public DateTime datumKraja { get; set; }


        [Required]
        public int Kotizacija { get; set; }


        [Required]
        [JsonIgnore]
        public List<KompanijaTribina> Kompanije { get; set; }
        
    }
}