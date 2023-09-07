using MusikPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusikPortal.Repository
{
  /*  public class MusikPortalRepository :IRepository
    {
        MusikPortalContext db;
        public MusikPortalRepository(MusikPortalContext context)
        {
            db = context;
        }
       
        public async Task<Salt> GetSalt(User u)
        {
            return await db.Salts.FirstOrDefaultAsync(m => m.user == u);
        }

        public async Task<User> GetUser(string name)
        {
            return await db.Users.FirstOrDefaultAsync(m => m.Name == name);
        }
        public async Task<User> GetEmail(string email)
        {
            return await db.Users.FirstOrDefaultAsync(m => m.email == email);
        }
        public async Task<Style> GetStyle(int id)
        {
            return await db.Styles.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Artist> GetArtist(int id)
        {
            return await db.Artists.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Song> GetSong(int id)
        {
            return await db.Songs.Include((p) => p.artist).Include((p) => p.style).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<int> GetArtistId(Song s)
        {         
            Artist a = await db.Artists.FirstOrDefaultAsync(m => m == s.artist);           
            return a.Id;
        }

        public async Task<int> GetStyleId(Song s)
        {
            Style a = s.style;
            return a.Id;
        }
        public async Task<List<Style>> GetStylesList()
        {
            return await db.Styles.ToListAsync(); 
        }
        public async Task<List<Artist>> GetArtistsList()
        {
            return await db.Artists.ToListAsync();
        }
        public async Task<List<Song>> GetSongs()
        {
            return await db.Songs.Include((p) => p.artist ).Include((p) => p.style).ToListAsync();
        }
        public async Task<List<Song>> FindSongs( string str)
        {
            Artist a = await db.Artists.FirstOrDefaultAsync(m => m.Name == str);
            if (a == null)
               return await db.Songs.Where(son => son.Name == str).Include((p) => p.artist).Include((p) => p.style).ToListAsync();
            else
                return await db.Songs.Where(son => son.artist == a).Include((p) => p.artist).Include((p) => p.style).ToListAsync();
        }
        public async Task<List<User>> GetUsers(string n)
        {
            return await db.Users.Where(user => user.Name != n).ToListAsync();
        }
        public async Task AddUser(User user)
        {
            await db.AddAsync(user);
        }
        public async Task AddSalt(Salt s)
        {
            await db.AddAsync(s);
        }
        public async Task AddStyle(Style s)
        {
            await db.AddAsync(s);
        }
        public async Task AddArtist(Artist s)
        {
            await db.AddAsync(s);
        }
        public async Task AddSong(Song s)
        {
            await db.AddAsync(s);
        }
        public async Task AddSongToArtist(int id,Song s)
        {
         
            var f = await db.Artists.FindAsync(id);
           Song s1 =await db.Songs.Where(son => son.artist == f).FirstOrDefaultAsync(m => m.Name == s.Name);
            if (f != null && s1!=null)
            {
                f.Songs.Add(s1);
                db.Artists.Update(f);
            }          
        }
        public async Task DeleteStyle(int id)
        {
            var f = await db.Styles.FindAsync(id);
            if (f != null)
            {
                db.Styles.Remove(f);

            }
        }
        public async Task EditStyle(int id,string s)
        {
            var f = await db.Styles.FindAsync(id);          
            if (f != null)
            {
                f.Name = s;
                db.Styles.Update(f);

            }
        }
        public async Task DeleteArtist(int id)
        {
            var f = await db.Artists.FindAsync(id);
            if (f != null)
            {
                db.Artists.Remove(f);

            }
        }
        public async Task EditArtist(int id, string s, string p)
        {
            var f = await db.Artists.FindAsync(id);
            if (f != null)
            {
                f.Name = s;
                f.photo = p;
                db.Artists.Update(f);

            }
        }
        public async Task EditUser(int id, int l)
        {
            var f = await db.Users.FindAsync(id);
            if (f != null)
            {
                f.Level = l;
                db.Users.Update(f);

            }
        }
        public async Task DeleteSong(int id)
        {
            var f = await db.Songs.FindAsync(id);
            if (f != null)
            {
                db.Songs.Remove(f);

            }
        }
        public async Task UpdateSong(Song s)
        {
           
                db.Songs.Update(s);
        }
        public async Task<bool> CheckEmail(string  s)
        {
            return await db.Users.AllAsync(u => u.email == s);
        }
        public async Task< bool> GetLogins(string s)
        {
            return   await db.Users.AllAsync(u => u.Name != s);
             
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }*/
}
