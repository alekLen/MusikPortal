using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class AddSong
    {
        [Required]
        [Display(Name = "name ")]
        public string Name { get; set; }      
        public int StyleId  { get; set; }
        public int ArtistId { get; set; }
        [Required]
        [Display(Name = "year ")]
        public string Year { get; set; }
        [Required]
        [Display(Name = "album ")]
        public string Album{ get; set; }
        [Required]
        [Display(Name = "text ")]
        public string text { get; set; }     
        public string? file { get; set; }
        public int? SongId { get; set; }
    }
}
