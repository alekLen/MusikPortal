using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class AddSong
    {
        [Required]
        [Display(Name = "put the Name ")]
        public string Name { get; set; }      
        public int StyleId  { get; set; }
        public int ArtistId { get; set; }
        [Required]
        [Display(Name = "put the Year ")]
        public string Year { get; set; }
        [Required]
        [Display(Name = "put the Album ")]
        public string Album{ get; set; }
        public string? text { get; set; }     
        public string? file { get; set; }
        public int? SongId { get; set; }
    }
}
