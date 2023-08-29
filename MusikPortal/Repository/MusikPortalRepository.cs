using MusikPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace MusikPortal.Repository
{
    public class MusikPortalRepository :IRepository
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
        public async Task<Style> GetStyle(int id)
        {
            return await db.Styles.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Artist> GetArtist(int id)
        {
            return await db.Artists.FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task EditArtist(int id, string s)
        {
            var f = await db.Artists.FindAsync(id);
            if (f != null)
            {
                f.Name = s;
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
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
