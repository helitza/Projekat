using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class Mesto
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public int BrRedova { get; set; }
        [Required]

        public int BrSedistaPoRedu { get; set; }

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

    }
}