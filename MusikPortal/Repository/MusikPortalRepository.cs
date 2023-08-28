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
        public async Task AddUser(User user)
        {
            await db.AddAsync(user);
        }
        public async Task AddSalt(Salt s)
        {
            await db.AddAsync(s);
        }
     
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
