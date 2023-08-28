using MusikPortal.Models;

namespace MusikPortal.Repository
{
    public interface IRepository
    {
        Task<Salt> GetSalt(User user);       
        Task<User> GetUser(string name);
        Task AddUser(User user);
        Task AddSalt(Salt s);
        Task Save();
    }
}
