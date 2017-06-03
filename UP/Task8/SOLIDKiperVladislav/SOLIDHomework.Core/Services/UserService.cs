using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Services
{
    public class UserService
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
}