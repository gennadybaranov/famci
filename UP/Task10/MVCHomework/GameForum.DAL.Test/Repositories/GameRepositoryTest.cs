using GameForum.DAL.Repositories;
using GameForum.DAL.Repositories.Abstractions;
using NUnit.Framework;

namespace GameForum.DAL.Test.Repositories
{
    [TestFixture]
    public class GameRepositoryTest
    {
        [Test]
        public void GameRepository_IsImplementIGameRepository_Implemented()
        {
            //Arrange
            GameRepository sut = new GameRepositoryBuilder()
                .Build();
            //Act
            //Assert
            Assert.IsInstanceOf<IGameRepository>(sut);
        }
    }
    public class GameRepositoryBuilder
    {
        public GameRepositoryBuilder()
        {

        }
        public GameRepository Build()
        {
            return new GameRepository();
        }
    }
}
