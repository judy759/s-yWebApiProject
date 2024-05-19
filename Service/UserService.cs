using Entities;
using Repository;
using System.Collections.Specialized;

namespace Service
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> Register(User user)
        {
            
            return await userRepository.Register(user);

        }
        public async Task<User> Login(User user)
        {
           User u=await userRepository.Login(user);

            return u;
        }

        public async Task<User> Update(int id, User user)
        {

           return await userRepository.Update(id, user);
        }

        public int checkPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            int score = result.Score;
            return score;
        }

       
       
    }
}
