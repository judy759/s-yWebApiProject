using Entities;

namespace Repository
{
    public interface IUserRepository
    {
         Task<List<User>> GetAllUsers();
         Task<User> Register(User user);
         Task<User> Update(int id, User user);

        Task<User> Login(User user);
    }
}
