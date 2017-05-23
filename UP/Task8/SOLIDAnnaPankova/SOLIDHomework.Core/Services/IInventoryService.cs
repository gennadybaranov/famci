using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Services
{
    public interface IInventoryService
    {
        void Reserve(string identifier, int quantity);
    }
}
