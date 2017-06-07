using System;

namespace GameForum.DomainModel.Exceptions
{
    public class GameExistException : Exception
    {
        public GameExistException()
        {

        }

        public GameExistException(string msg)
            : base(msg)
        {

        }
    }
}
