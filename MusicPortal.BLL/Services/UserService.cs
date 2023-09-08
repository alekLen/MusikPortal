using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal.BLL.Services
{
    public class UserService: IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<UserDTO> GetUser(string name)
        {
            var u = await Database.Users.GetUser(name);
            if (u == null)
                return null;
            return UserToUserDTO(u);
        }
        public async Task<UserDTO> GetEmail(string email)
        {
            var u = await Database.Users.GetEmail(email);
            if (u == null)
                return null;
            return UserToUserDTO(u);
        }
        public async Task<IEnumerable<UserDTO>> GetUsers(string n)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetUsers(n));
        }
        public async Task AddUser(UserDTO u)
        {
            var user = new User
            {
                Id = u.Id,
                Name = u.Name,
                Password = u.Password,
                Level = u.Level,
                email = u.email,
                Age = u.Age,
            };
            await Database.Users.AddItem(user);
            await Database.Save();
        }
        public async Task UpdateUser(int id, int l)
        {
            await Database.Users.Update(id, l);
            await Database.Save();
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var u = await Database.Users.Get(id);
            if (u == null)
                throw new ValidationException("Wrong artist!", "");
            return   UserToUserDTO( u);           
        }
        public UserDTO UserToUserDTO(User u)
        {
            return new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Password = u.Password,
                Level = u.Level,
                email = u.email,
                Age = u.Age,
            };
        }
        public async Task<bool> CheckEmail(string s)
        {
            return await Database.Users.CheckEmail(s);
        }
        public async Task<bool> GetLogins(string s)
        {
            return await Database.Users.GetLogins(s);
        }
    }
}
