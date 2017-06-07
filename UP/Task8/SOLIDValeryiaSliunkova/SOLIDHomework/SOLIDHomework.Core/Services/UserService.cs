using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Services
{
    public class UserService : IUserService
    {
        // that is Database-based service 
        public Account GetByUsername(string username)
        {
            return new Account()
                {
                    Username = "TestUser",
                    Email =  "test@test.com"
                };
        }
    }

    public interface IUserService
    {
        Account GetByUsername(string username);
    }
}