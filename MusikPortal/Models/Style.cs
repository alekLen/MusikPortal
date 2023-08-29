namespace MusikPortal.Models
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }=new List<Song>();
    }
}
