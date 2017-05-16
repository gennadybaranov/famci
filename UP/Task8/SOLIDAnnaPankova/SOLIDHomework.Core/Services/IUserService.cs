using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Services
{
    public interface IUserService
    {
        Account GetByUsername(string username);
    }
}
