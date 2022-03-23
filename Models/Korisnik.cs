using System;
using System.ComponentModel.DataAnnotations;
namespace Models{
    public class Korisnik{
        [Key]
        public int Id { get; set; }
        
        [Required]
 
        public string Ime{get; set;}
        [Required]
 
        public string Prezime{get; set;}

        [Required]
        public string email{get; set;}

        [Required]
        [MinLength(8)]
        public string sifra{get;set;}
        
        [Required]
        public Boolean admin{get; set;}
    }
}