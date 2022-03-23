using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Models{
    public class Kompanija{
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Naziv{get; set;}


        public List<Mesto> Mesta { get; set; }

        public List<KompanijaTribina> Tribine { get; set; }
        public List<Odrzavanje> Odrzavanje { get; set; }
    }
}