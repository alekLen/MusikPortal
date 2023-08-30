namespace MusikPortal.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Style style { get; set; }

        public Artist artist { get; set; }
        public string Year { get; set; }
        public string Album { get; set; }
        public string file { get; set; }
        public string? text { get; set; }
    }
}
