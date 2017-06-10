using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Services
{
    public interface IUserService
    {
        Account GetByUsername(string username);
    }
}