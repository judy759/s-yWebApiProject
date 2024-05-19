using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
    public class UserRepository:IUserRepository
    {
        _214346710DbContext userContext;
        public UserRepository(_214346710DbContext userContext)
        {
            this.userContext = userContext;
        }
        public async Task<User> Register(User user)
        {
            try
            {
                await userContext.Users.AddAsync(user);
                await userContext.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<User>> GetAllUsers() {
            return await userContext.Users.ToListAsync();
        }


        public async Task<User> Update(int id, User user)
        {
            List<User> users = await userContext.Users.ToListAsync();
            User u = await userContext.Users.FirstOrDefaultAsync(uu => uu.Id == id);
            u.UserName = user.UserName;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Password = user.Password;
            Console.WriteLine("user", u.FirstName);
            await userContext.SaveChangesAsync();
            return u;
            
        }

        public async Task<User> Login(User user)
        {
            var users = await userContext.Users.ToListAsync();
            //User userFound3 = users.FirstOrDefault(u => u.UserName.Equals(user.UserName));
            //User userFound = users.Find(u => u.UserName.Equals(user.UserName));

            User userFound2 = await userContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(user.UserName.TrimEnd()));
            if (userFound2 != null)
            {
                if (userFound2.Password.TrimEnd().Equals(user.Password))
                {
                    return userFound2;
                }
            }
            return null;
        }

       
       
       

     
    }
}
