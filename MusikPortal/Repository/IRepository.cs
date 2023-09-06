using MusikPortal.Models;

namespace MusikPortal.Repository
{
    public interface IRepository
    {
        Task<Salt> GetSalt(User user);       
        Task<User> GetUser(string name);
        Task<User> GetEmail(string email);
        Task<Style> GetStyle(int id);
        Task<Artist> GetArtist(int id);
        Task<Song> GetSong(int id);
        Task<int> GetArtistId(Song s);
        Task<int> GetStyleId(Song s);
        Task AddUser(User user);
        Task AddSalt(Salt s);
        Task AddSong(Song s);
        Task AddArtist(Artist s);
        Task<List<Style>> GetStylesList();
        Task<List<Artist>> GetArtistsList();
        Task<List<Song>> GetSongs();
        Task<List<User>> GetUsers(string n);
        Task AddStyle(Style s);
        Task DeleteStyle(int id);
        Task EditStyle(int id, string s);
        Task DeleteArtist(int id);
        Task EditArtist(int id, string s, string p);
        Task EditUser(int id, int l);
        Task DeleteSong(int id);
        Task UpdateSong(Song s);
        Task AddSongToArtist(int id, Song s);
        Task<List<Song>> FindSongs(string str);
        Task<bool> CheckEmail(string s);
        Task<bool> GetLogins(string s);
        Task Save();
    }
}
