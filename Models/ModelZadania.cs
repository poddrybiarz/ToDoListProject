using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace projekt_v1._0.Models
{
    public class ModelZadania
    {
        [Key]
        [Display(Name = "Id zadania")]
        public int ZadanieId { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime Data { get; set; }

        [Display(Name = "Start")]
        public TimeSpan? Start { get; set; }

        [Display(Name = "Koniec")]
        public TimeSpan? End { get; set; }

        [Display(Name = "Czy godzina wykonania zadania jest istotna?")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public bool Hour { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Display(Name = "Gotowe?")]
        public bool Done { get; set; }

        public string? UserId { get; set; }

        public virtual IdentityUser? User { get; set; }
    }
}
