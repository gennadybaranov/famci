using GameForum.DAL.Repositories;
using GameForum.DAL.Repositories.Abstractions;
using NUnit.Framework;

namespace GameForum.DAL.Test.Repositories
{
    [TestFixture]
    public class CommentRepositoryTest
    {
        [Test]
        public void CommentRepository_IsImplementICommentRepository_Implemented()
        {
            //Arrange
            CommentRepository sut = new CommentRepositoryBuilder()
                .Build();
            //Act
            //Assert
            Assert.IsInstanceOf<ICommentRepository>(sut);
        }
    }
    public class CommentRepositoryBuilder
    {
        public CommentRepositoryBuilder()
        {

        }
        public CommentRepository Build()
        {
            return new CommentRepository();
        }
    }
}
