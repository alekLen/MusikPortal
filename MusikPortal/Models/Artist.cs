using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public ICollection<Song>? Songs { get; set; } = new List<Song>();
    }
}
