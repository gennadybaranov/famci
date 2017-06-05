using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.DomainModel.Exceptions
{
    public class NonExistGameException : Exception
    {
        public NonExistGameException()
        {

        }

        public NonExistGameException(string msg)
            : base(msg)
        {

        }
    }
}
