using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameForum.DAL.DAO;
using GameForum.DAL.Repositories;
using GameForum.DAL.Repositories.Abstractions;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Exceptions;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services;
using GameForum.DomainModel.Services.Abstractions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameForum.Domain.Test.Services
{
    [TestFixture]
    public class GameServiceTest
    {
        [Test]
        public void GameService_ImplentIGameService_Implemented()
        {
            GameService sut = new GameServiceBuilder().Build();

            Assert.IsInstanceOf<IGameService>(sut);
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        [TestCase("key2", "game2", "desc2")]
        public void GetAllGames_GetAllGamesFromRepository_CorrectDataReturned(string key, string name, string desc)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.GetAllGames()).Returns(
                new[]
                    {
                        new GameItemBuilder().WithKey(key).WithName(name).WithDescription(desc)
                        .WithComments(null).Build()
                    }
                );
            IEnumerable<Game> expected = new Game[]
                {
                    new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build()
                };
            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            IEnumerable<Game> result = sut.GetAllGames();

            //Assert
            Assert.That(compareObjects.Compare(expected.ToArray(), result.ToArray()), Is.True);
            mockGames.Verify(x => x.GetAllGames(), Times.Once);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [TestCase(null)]
        [TestCase("")]
        public void CreateNewGame_CallWithIncorrectData_ArgumentExceptionThrown(string key)
        {
            //Arrange
            Game game = new GameBuilder().WithKey(key).Build();
            GameService sut = new GameServiceBuilder().Build();

            //Act
            sut.CreateNewGame(game);
            //Assert
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void CreateNewGame_CallWithCorrectDataForNonExistGame_RepositoryCreateCalled(string key, string name, string desc)
        {
            //Arrange

            var compareObjects = new CompareObjects();
            Game newGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            GameItem gameItemExpected = new GameItemBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.CreateNewGame(newGame);

            //Assert
            mockGames.Verify(x => x.CreateNewGame(
                It.Is<GameItem>(item => compareObjects.Compare(item, gameItemExpected))),
                Times.Exactly(1));
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void CreateNewGame_CallWithCorrectDataForNonExistGame_RepositorySaveCalled(string key, string name, string desc)
        {
            //Arrange

            Game newGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.CreateNewGame(newGame);

            //Assert
            mockGames.Verify(x => x.Save(), Times.Exactly(1));
        }

        [Test]
        [ExpectedException(typeof(GameExistException))]
        [TestCase("key1", "game1", "desc1")]
        public void CreateNewGame_CallWithCorrectDataForExistedGame_ExceptionRaised(string key, string name, string desc)
        {
            //Arrange

            Game newGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.CreateNewGame(newGame);

            //Assert
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void CreateNewGame_CallWithCorrectDataForExistedGame_RepositoryWasntCalled(string key, string name, string desc)
        {
            //Arrange

            Game newGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            Assert.Throws<GameExistException>(() => sut.CreateNewGame(newGame));

            //Assert
            mockGames.Verify(x => x.Save(), Times.Exactly(0));
            mockGames.Verify(x => x.CreateNewGame(It.IsAny<GameItem>()), Times.Exactly(0));
        }

        [Test]
        [ExpectedException(typeof(NonExistGameException))]
        [TestCase("key1", "game1", "desc1")]
        public void EditGame_CallForNonExisingtGame_ExceptionRaised(string key, string name, string desc)
        {
            //Arrange
            Game editedGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.EditGame(editedGame);

            //Assert
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void EditGame_CallForNonExisingtGame_RepositoryWasntCalled(string key, string name, string desc)
        {
            //Arrange
            Game editedGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            Assert.Throws<NonExistGameException>(() => sut.EditGame(editedGame));

            //Assert

            mockGames.Verify(x => x.EditGame(It.IsAny<GameItem>()), Times.Exactly(0));
            mockGames.Verify(x => x.Save(), Times.Exactly(0));
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void EditGame_CallForExistingGame_RepositorySaveCalled(string key, string name, string desc)
        {
            //Arrange

            Game editedGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.EditGame(editedGame);

            //Assert
            mockGames.Verify(x => x.Save(), Times.Exactly(1));
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void EditGame_CallForExistingGame_RepositoryEditGameCalled_WithCorrectData(string key, string name, string desc)
        {
            //Arrange

            var compareObjects = new CompareObjects();
            Game editedGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            GameItem gameItemExpected = new GameItemBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.EditGame(editedGame);

            //Assert
            mockGames.Verify(x => x.EditGame(
                It.Is<GameItem>(item => compareObjects.Compare(item, gameItemExpected))),
                Times.Exactly(1));
        }

        [Test]
        [TestCase("key1")]
        public void DeleteGame_CallForNonExistedGame_RepositoryWasntCalled(string key)
        {
            //Arrange
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            Assert.Throws<NonExistGameException>(() => sut.DeleteGame(key));

            //Assert

            mockGames.Verify(x => x.EditGame(It.IsAny<GameItem>()), Times.Exactly(0));
            mockGames.Verify(x => x.Save(), Times.Exactly(0));
        }

        [Test]
        [TestCase("key1")]
        public void DeleteGame_CallForNonExistedGame_ExceptionRaised(string key)
        {
            //Arrange
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Assert
            Assert.Throws<NonExistGameException>(() => sut.DeleteGame(key));
        }

        [Test]
        [TestCase("key1")]
        public void DeleteGame_CallForExistingGame_RepositoryDeleteCalled(string key)
        {
            //Arrange

            var compareObjects = new CompareObjects();
            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.DeleteGame(key);

            //Assert
            mockGames.Verify(x => x.DeleteGame(
                It.Is<string>(item => compareObjects.Compare(item, key))),
                Times.Exactly(1));
        }

        [Test]
        [TestCase("key1")]
        public void DeleteGame_CallForExistingGame_RepositorySaveCalled(string key)
        {
            //Arrange

            var mockGames = new Mock<IGameRepository>();
            mockGames.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGames).Build();

            //Act
            sut.DeleteGame(key);

            //Assert
            mockGames.Verify(x => x.Save(), Times.Exactly(1));
        }

        [Test]
        [ExpectedException(typeof(NonExistGameException))]
        [TestCase("key1")]
        public void GetGameByKey_CallForNonExistedGame_ExceptionRaised(string key)
        {
            //Assert
            var mockgames = new Mock<IGameRepository>();
            mockgames.Setup(x => x.IsGameExist(key)).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockgames).Build();

            //Act
            sut.GetGameByKey(key);
        }

        [Test]
        [TestCase("key1", "name1", "desc1")]
        public void GetGameByKey_CallForExistedGame_CorrectDataReturned(string key, string name, string desc)
        {
            //Assert
            var compareObjects = new CompareObjects();
            var expectedGame = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build();
            var mockgames = new Mock<IGameRepository>();
            mockgames.Setup(x => x.IsGameExist(key)).Returns(true);
            mockgames.Setup(x => x.GetGameByKey(key)).Returns(
                new GameItemBuilder().WithKey(key).WithName(name).WithDescription(desc).Build()).Verifiable();

            GameService sut = new GameServiceBuilder().WithGameRepository(mockgames).Build();

            //Act
            var result = sut.GetGameByKey(key);
            Assert.IsTrue(compareObjects.Compare(expectedGame, result));
        }

        [Test]
        [TestCase("key1", "comment")]
        public void AddCommentToGame_CallForNonExistedGame_ExceptionRaised(string key, string comment)
        {
            //Arrange
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGame).Build();

            //Act

            //Assert

            Assert.Throws<NonExistGameException>(() => sut.AddCommentToGame(key, comment));
        }

        [Test]
        [TestCase("key1", "comment")]
        public void AddCommentToGame_CallForNonExistedGame_RepositoryWasntCalled(string key, string comment)
        {
            //Arrange
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            var mockComment = new Mock<ICommentRepository>();

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGame).WithCommentsRepository(mockComment)
                .Build();

            //Act

            //Assert

            Assert.Throws<NonExistGameException>(() => sut.AddCommentToGame(key, comment));
            mockComment.Verify(x => x.Save(), Times.Exactly(0));
            mockComment.Verify(x => x.AddCommentToGame(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));
        }

        [Test]
        [TestCase("key1", "comment")]
        public void AddCommentToGame_CallForExistingGame_RepositorySaveCalled(string key, string comment)
        {
            //Arrange
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            var mockComment = new Mock<ICommentRepository>();

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGame).WithCommentsRepository(mockComment)
                .Build();

            //Act
            sut.AddCommentToGame(key, comment);
            //Assert

            mockComment.Verify(x => x.Save(), Times.Exactly(1));

        }

        [Test]
        [TestCase("key1", "comment")]
        public void AddCommentToGame_CallForExistingGame_RightDataPassed(string key, string comment)
        {
            //Arrange
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(true);

            var mockComment = new Mock<ICommentRepository>();

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGame).WithCommentsRepository(mockComment)
                .Build();

            //Act
            sut.AddCommentToGame(key, comment);
            //Assert

            mockComment.Verify(x => x.AddCommentToGame(
                It.Is<string>(item => item.Equals(key)), It.Is<string>(item => item.Equals(comment))), Times.Exactly(1));
        }

        [Test]
        [TestCase("key1")]
        public void GetCommentsByGameKey_CallForNonExistedGame_ExceptionRaised(string key)
        {
            //Arrange
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGame).Build();

            //Act

            //Assert

            Assert.Throws<NonExistGameException>(() => sut.GetCommentsByGameKey(key));
        }

        [Test]
        [TestCase("key1")]
        public void GetCommentsByGameKey_CallForNonExistedGame_RepositoryWasntCalled(string key)
        {
            //Arrange
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(It.IsAny<string>())).Returns(false);
            var mockComment = new Mock<ICommentRepository>();

            GameService sut = new GameServiceBuilder().WithGameRepository(mockGame).WithCommentsRepository(mockComment)
                .Build();

            //Act

            //Assert

            Assert.Throws<NonExistGameException>(() => sut.GetCommentsByGameKey(key));
            mockComment.Verify(x => x.GetCommentsByGameKey(It.IsAny<string>()), Times.Exactly(0));
        }

        [Test]
        [TestCase("key1", 1, "commentBody")]
        public void GetCommentsByGameKey_CallForExistedGame_CorrectDataReturned(string key, int commentId, string commentBody)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var mockComments = new Mock<ICommentRepository>();
            mockComments.Setup(x => x.GetCommentsByGameKey(key)).Returns(
                new[]
                    {
                        new CommentItemBuilder().WithId(commentId).WithBody(commentBody).WithGameKey(key)
                        .Build()
                    }
                );
            var mockGame = new Mock<IGameRepository>();
            mockGame.Setup(x => x.IsGameExist(key)).Returns(true);

            IEnumerable<Comment> expected = new Comment[]
                {
                    new CommentBuilder().WithId(commentId).WithBody(commentBody).WithGameKey(key)
                    .Build()
                };

            GameService sut = new GameServiceBuilder().WithCommentsRepository(mockComments)
                .WithGameRepository(mockGame).Build();

            //Act
            IEnumerable<Comment> result = sut.GetCommentsByGameKey(key);

            //Assert
            Assert.That(compareObjects.Compare(expected.ToArray(), result.ToArray()), Is.True);
            mockComments.Verify(x => x.GetCommentsByGameKey(It.Is<string>(item => item.Equals(key))), Times.Once);
        }

        [Test]
        public void GetAllGenres_ResultAsExpected()
        {
            //Assert
            var compareObjects = new CompareObjects();
            var genreRepository = new Mock<IGenreRepository>();
            genreRepository.Setup(x => x.GetAllGenres()).Returns(
                new[]
                    {
                        new GenreItem
                            {
                                GenreId = 1, Genre = "Shooter"
                            },
                        new GenreItem
                            {
                                GenreId = 2, Genre = "RPG"
                            }
                    }
                );

            IEnumerable<Genre> expected = new[]
                {
                    new Genre
                            {
                                GenreId = 1, GenreValue = "Shooter"
                            },
                    new Genre
                            {
                                GenreId = 2, GenreValue = "RPG"
                            }
                };

            IGameService sut = new GameServiceBuilder().WithGenresRepository(genreRepository).Build();

            // Act
            var result = sut.GetAllGenres();

            //Assert
            Assert.IsTrue(compareObjects.Compare(expected.ToArray(), result.ToArray()));
        }

        [Test]
        public void GetAllGernres_GenresRepositoryCalled()
        {
            //Assert
            var genreRepository = new Mock<IGenreRepository>();
            
            IGameService sut = new GameServiceBuilder().WithGenresRepository(genreRepository).Build();

            // Act
            sut.GetAllGenres();

            //Assert
            genreRepository.Verify( x => x.GetAllGenres(), Times.Once);
        }
    }

    internal class GameServiceBuilder
    {
        private Mock<IGameRepository> mokegames;
        private Mock<IWrappedMapper> mockMapper;
        private Mock<ICommentRepository> mockComments;
        private Mock<IGenreRepository> mockGenre;

        public GameServiceBuilder()
        {
            mokegames = new Mock<IGameRepository>();
            mockComments = new Mock<ICommentRepository>();
            mockGenre = new Mock<IGenreRepository>();
            mockMapper = new Mock<IWrappedMapper>();
            mockMapper.Setup(x => x.Map<GameItem>(It.IsAny<object>())).Returns((object item) =>
                {
                    var game = (Game)item;
                    return new GameItem
                        {
                            Key = game.Key,
                            Name = game.Name,
                            Description = game.Description
                        };
                });
            mockMapper.Setup(x => x.Map<Game>(It.IsAny<object>())).Returns((object item) =>
            {
                var game = (GameItem)item;
                return new Game
                {
                    Key = game.Key,
                    Name = game.Name,
                    Description = game.Description
                };
            });
            mockMapper.Setup(x => x.Map<Comment>(It.IsAny<object>())).Returns((object item) =>
            {
                var comment = (CommentItem)item;
                return new Comment()
                {
                    CommentId = comment.CommentId,
                    Body = comment.Body,
                    GameKey = comment.GameKey
                };
            });
            mockMapper.Setup(x => x.Map<Genre>(It.IsAny<object>())).Returns((object item) =>
                {
                    var genre = (GenreItem)item;
                    return new Genre()
                    {
                        GenreId = genre.GenreId,
                        GenreValue = genre.Genre
                    };
                });

        }

        public GameService Build()
        {
            return new GameService(mokegames.Object, mockComments.Object, mockGenre.Object, mockMapper.Object);
        }

        public GameServiceBuilder WithGameRepository(Mock<IGameRepository> mokegames)
        {
            this.mokegames = mokegames;
            return this;
        }

        public GameServiceBuilder WithMapper(Mock<IWrappedMapper> mapper)
        {
            this.mockMapper = mapper;
            return this;
        }

        public GameServiceBuilder WithCommentsRepository(Mock<ICommentRepository> comments)
        {
            this.mockComments = comments;
            return this;
        }

        public GameServiceBuilder WithGenresRepository(Mock<IGenreRepository> genres)
        {
            this.mockGenre = genres;
            return this;
        }
    }

    internal class GameItemBuilder
    {
        private string _key;
        private string _name;
        private string _description;
        private ICollection<CommentItem> _comments;

        public GameItemBuilder()
        {
            var autoFixture = new Fixture();
            this._key = autoFixture.Create<string>();
            this._name = autoFixture.Create<string>();
            this._name = autoFixture.Create<string>();
        }

        public GameItem Build()
        {
            return new GameItem
                {
                    Key = this._key,
                    Name = this._name,
                    Description = this._description,
                    Comments = _comments
                };
        }

        public GameItemBuilder WithKey(string key)
        {
            this._key = key;
            return this;
        }

        public GameItemBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public GameItemBuilder WithDescription(string desc)
        {
            this._description = desc;
            return this;
        }

        public GameItemBuilder WithComments(ICollection<CommentItem> comments)
        {
            this._comments = comments;
            return this;
        }
    }

    internal class GameBuilder
    {
        private string _key;
        private string _name;
        private string _description;

        public GameBuilder()
        {
            var autoFixture = new Fixture();
            this._key = autoFixture.Create<string>();
            this._name = autoFixture.Create<string>();
            this._name = autoFixture.Create<string>();
        }

        public Game Build()
        {
            return new Game
                {
                    Key = this._key,
                    Name = this._name,
                    Description = this._description
                };
        }

        public GameBuilder WithKey(string key)
        {
            this._key = key;
            return this;
        }

        public GameBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public GameBuilder WithDescription(string desc)
        {
            this._description = desc;
            return this;
        }
    }

    internal class CommentItemBuilder
    {
        private int _id;
        private string _body;
        private string _gameKey;
        private GameItem _game;

        public CommentItemBuilder()
        {
            var autoFixture = new Fixture();
            this._id = autoFixture.Create<int>();
            this._body = autoFixture.Create<string>();
            this._gameKey = autoFixture.Create<string>();
        }

        public CommentItem Build()
        {
            return new CommentItem()
            {
                Body = this._body,
                GameKey = this._gameKey,
                CommentId = this._id
            };
        }

        public CommentItemBuilder WithId(int id)
        {
            this._id = id;
            return this;
        }

        public CommentItemBuilder WithBody(string body)
        {
            this._body = body;
            return this;
        }

        public CommentItemBuilder WithGameKey(string gameKey)
        {
            this._gameKey = gameKey;
            return this;
        }
    }

    internal class CommentBuilder
    {
        private int _id;
        private string _body;
        private string _gameKey;
        private GameItem _game;

        public CommentBuilder()
        {
            var autoFixture = new Fixture();
            this._id = autoFixture.Create<int>();
            this._body = autoFixture.Create<string>();
            this._gameKey = autoFixture.Create<string>();
        }

        public Comment Build()
        {
            return new Comment()
            {
                Body = this._body,
                GameKey = this._gameKey,
                CommentId = this._id
            };
        }

        public CommentBuilder WithId(int id)
        {
            this._id = id;
            return this;
        }

        public CommentBuilder WithBody(string body)
        {
            this._body = body;
            return this;
        }

        public CommentBuilder WithGameKey(string gameKey)
        {
            this._gameKey = gameKey;
            return this;
        }
    }
}
