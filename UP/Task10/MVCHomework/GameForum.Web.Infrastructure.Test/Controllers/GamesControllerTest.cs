using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services.Abstractions;
using GameForum.Web.Infrastructure.Controllers;
using GameForum.Web.Infrastructure.Models;
using KellermanSoftware.CompareNetObjects;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameForum.Web.Infrastructure.Test.Controllers
{
    [TestFixture]
    public class GamesControllerTest
    {
        [Test]
        public void GamesController_IsImplementController_Implemented()
        {
            //Arrange
            GamesController sut = new GamesControllerBuilder().Build();
            //Act
            //Assert
            Assert.IsInstanceOf<Controller>(sut);

        }

        [Test]
        public void AllGames_AllGamesJSONData_DataIsJSON()
        {
            //Arrange
            GamesController sut = new GamesControllerBuilder().Build();
            //Act
            JsonResult jsonResult = sut.AllGames();
            //Assert
            Assert.IsNotNull(jsonResult);
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void AllGames_AllGamesData_DataMatchsExpected(string key, string name, string desc)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var mockService = new Mock<IGameService>();
            mockService.Setup(x => x.GetAllGames())
                       .Returns(new[]
                           {
                               new GameBuilder().WithKey(key).WithName(name).WithDescription(desc).Build()
                           });

            IEnumerable<GameViewModel> expected = new List<GameViewModel>()
                {
                    new GameViewModelBuilder().WithKey(key).WithName(name).WithDescription(desc).Build()
                };
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            //Act
            var data = sut.AllGames().Data as IEnumerable<GameViewModel>;
            //Assert

            Assert.That(compareObjects.Compare(
                    expected.ToArray(),
                    data.ToArray()), Is.True,
                    "data doesn't match");
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void New_ValidNewGame_GamePlacedToService(string key, string name, string desc)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService)
                                                                  .Build();
            GameViewModel newGame = new GameViewModelBuilder().WithKey(key).WithName(name).WithDescription(desc)
                .Build();

            Game expected = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc)
                .Build();
            //Act

            sut.New(newGame);
            //Assert
            mockService.Verify(x => x.CreateNewGame(It.Is<Game>(item => compareObjects.Compare(item, expected))), Times.Once);
        }

        [Test]
        public void New_InValidNewGame_ErrorStatusReturned()
        {
            //Arrange
            GamesController sut = new GamesControllerBuilder().Build();
            string expected = "invalid input";
            string wrongKey = string.Empty;

            //Act

            sut.ModelState.AddModelError("key", "invalid");
            var result = sut.New(new GameViewModelBuilder().WithKey(wrongKey).Build()) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(sut.ModelState.Values.Count > 0);
        }

        [Test]
        public void New_InValidNewGame_NewGameWasntCalled()
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            string wrongKey = string.Empty;

            //Act
            sut.ModelState.AddModelError("key", "invalid");
            sut.New(new GameViewModelBuilder().WithKey(wrongKey).Build());

            //Assert
            mockService.Verify(x => x.CreateNewGame(It.IsAny<Game>()), Times.Exactly(0));
        }

        [Test]
        public void New_ErrorInNewGameService_ErrorInResult()
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService)
                                                                  .Build();
            string errorMessage = "Error on new game";
            mockService.Setup(x => x.CreateNewGame(It.IsAny<Game>()))
                       .Throws(new Exception(errorMessage));

            //Act

            var result = sut.New(new GameViewModelBuilder().Build()) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(sut.ModelState.Values.Count > 0);
        }

        [Test]
        public void NewGet_ViewResult_AsResult()
        {
            var sut = new GamesControllerBuilder().Build();

            var result = sut.New() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("key1", "game1", "desc1")]
        public void Update_ValidEditedGame_EditedGamePlacedToService(string key, string name, string desc)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var mockService = new Mock<IGameService>();
            var sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            var editedGame = new GameViewModelBuilder().WithKey(key).WithName(name).WithDescription(desc)
                .Build();
            var expected = new GameBuilder().WithKey(key).WithName(name).WithDescription(desc)
                .Build();

            //Act
            sut.Update(editedGame);

            //Assert
            mockService.Verify(x => x.EditGame(It.Is<Game>(item => compareObjects.Compare(item, expected))),
                Times.Once);
        }

        [Test]
        public void Update_ValidEditedGame_NoErrorMessage()
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            var sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            var editedGame = new GameViewModelBuilder().Build();

            //Act
            JsonResult result = sut.Update(editedGame);
            var model = result.Data as UpdateGameResponseViewModel;

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(model.ErrorMessage));
        }

        [Test]
        public void Update_InValidEditedGame_ErrorStatusReturnedInJSON()
        {
            //Arrange
            GamesController sut = new GamesControllerBuilder().Build();
            string expected = "invalid input";
            string wrongKey = null;

            //Act

            sut.ModelState.AddModelError("key", "invalid");
            JsonResult result = sut.Update(new GameViewModelBuilder().WithKey(wrongKey).Build());

            var data = result.Data as UpdateGameResponseViewModel;
            string message = data.ErrorMessage;
            //Assert
            Assert.AreEqual(expected, message);
        }

        [Test]
        public void Update_InValidEditedGame_EditGameWasntCalled()
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            string wrongKey = string.Empty;

            //Act
            sut.ModelState.AddModelError("key", "invalid");
            sut.Update(new GameViewModelBuilder().WithKey(wrongKey).Build());

            //Assert
            mockService.Verify(x => x.EditGame(It.IsAny<Game>()), Times.Exactly(0));
        }

        [Test]
        public void Update_ErrorInEditGameService_ErrorInJSONResult()
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService)
                                                                  .Build();
            string errorMessage = "Error on edit game";
            mockService.Setup(x => x.EditGame(It.IsAny<Game>()))
                       .Throws(new Exception(errorMessage));

            //Act

            JsonResult result = sut.Update(new GameViewModelBuilder().Build());
            var data = result.Data as UpdateGameResponseViewModel;

            //Assert
            Assert.AreEqual(errorMessage, data.ErrorMessage);
        }

        [Test]
        [TestCase("key1")]
        public void Remove_ValidRemovingGame_RemoveGamePlacedToService(string key)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var mockService = new Mock<IGameService>();
            var sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            var removedGame = key;
            var expected = key;

            //Act
            sut.Remove(removedGame);

            //Assert
            mockService.Verify(x => x.DeleteGame(It.Is<string>(item => compareObjects.Compare(item, expected))),
                Times.Once);
        }

        [Test]
        [TestCase("key1")]
        public void Remove_ValidRemovingGame_NoErrorMessage(string key)
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            var sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            var removingGame = key;

            //Act
            JsonResult result = sut.Remove(removingGame);
            var model = result.Data as RemoveGameResponseViewModel;

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(model.ErrorMessage));
        }

        [Test]
        public void Remove_InValidRemovingGame_ErrorStatusReturnedInJSON()
        {
            //Arrange
            GamesController sut = new GamesControllerBuilder().Build();
            string expected = "invalid input";
            string wrongKey = null;

            //Act

            sut.ModelState.AddModelError("key", "invalid");
            JsonResult result = sut.Remove(wrongKey);

            var data = result.Data as RemoveGameResponseViewModel;
            string message = data.ErrorMessage;
            //Assert
            Assert.AreEqual(expected, message);
        }

        [Test]
        public void Remove_InValidRemovingGame_RemoveGameWasntCalled()
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService).Build();
            string wrongKey = string.Empty;

            //Act
            sut.ModelState.AddModelError("key", "invalid");
            sut.Remove(wrongKey);

            //Assert
            mockService.Verify(x => x.DeleteGame(It.IsAny<string>()), Times.Exactly(0));
        }

        [Test]
        [TestCase("key1")]
        public void Remove_ErrorInRemovingGameService_ErrorInJSONResult(string key)
        {
            //Arrange
            var mockService = new Mock<IGameService>();
            GamesController sut = new GamesControllerBuilder().WithGameService(mockService)
                                                                  .Build();
            string errorMessage = "Error on remove game";
            mockService.Setup(x => x.DeleteGame(It.IsAny<string>()))
                       .Throws(new Exception(errorMessage));

            //Act

            JsonResult result = sut.Remove(key);
            var data = result.Data as RemoveGameResponseViewModel;

            //Assert
            Assert.AreEqual(errorMessage, data.ErrorMessage);
        }

        [Test]
        public void CheckName_ExistingGameNamePassed_ErrorMessageReturned()
        {
            //Assert
            var gameName = "name";
            var expected = "Such game already exists. Please, select another one";
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(gameName)).Returns(true);

            var sut = new GamesControllerBuilder().WithGameService(gameService).Build();

            //Act
            var result = sut.CheckName(gameName) as JsonResult;

            //Assert
            StringAssert.AreEqualIgnoringCase(result.Data as string, expected);
        }

        [Test]
        public void CheckName_NonExistingGameNamePassed_TrueReturned()
        {
            //Assert
            var gameName = "name";
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(gameName)).Returns(false);

            var sut = new GamesControllerBuilder().WithGameService(gameService).Build();

            //Act
            var result = sut.CheckName(gameName) as JsonResult;

            //Assert
            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        [TestCase(true, "name")]
        [TestCase(false, "name")]
        public void CheckName_AnyData_GameServiceWasCalled(bool isExist, string name)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(name)).Returns(isExist);

            var sut = new GamesControllerBuilder().WithGameService(gameService).Build();

            //Act
            sut.CheckName(name);

            //Assert
            gameService.Verify(x => x.IsGameExist(name), Times.Once);
        }

        [Test]
        public void Genres_JsonReturnedAsResult()
        {
            //Arrange
            GamesController sut = new GamesControllerBuilder().Build();

            //Act
            var result = sut.Genres();

            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void Genres_ResultAsExpected()
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetAllGenres()).Returns(
                new[]
                    {
                        new Genre
                            {
                                GenreId = 1,
                                GenreValue = "Shooter"
                            },
                        new Genre
                            {
                                GenreId = 2,
                                GenreValue = "RPG"
                            }
                    }
                );

            var expected = new[]
                {
                    new SelectListItem
                        {
                             Text = "Shooter",
                             Value = "1",
                             Selected = false
                        },
                    new SelectListItem()
                        {
                             Text = "RPG",
                             Value = "2",
                             Selected = false  
                        }
                };

            //Act
            GamesController sut = new GamesControllerBuilder().WithGameService(gameService).Build();

            var result = sut.Genres() as JsonResult;
            var data = result.Data as IEnumerable<SelectListItem>;

            //Assert
            Assert.IsTrue(compareObjects.Compare(expected.ToArray(),data.ToArray()));
        }

        [Test]
        public void Genres_GameServiceCalled()
        {
             //Arrange
            var gameService = new Mock<IGameService>();

            //Act
            GamesController sut = new GamesControllerBuilder().WithGameService(gameService).Build();

            sut.Genres();

            //Assert
            gameService.Verify( x => x.GetAllGenres(), Times.Once);
        }
    }

    internal class GamesControllerBuilder
    {
        private Mock<IGameService> mockService;
        private Mock<IWrappedMapper> mockMapper;

        public GamesControllerBuilder()
        {
            mockService = new Mock<IGameService>();
            mockMapper = new Mock<IWrappedMapper>();
            mockMapper.Setup(x => x.Map<GameViewModel>(It.IsAny<object>())).Returns((object item) =>
            {
                var game = (Game)item;
                return new GameViewModel
                {
                    Key = game.Key,
                    Name = game.Name,
                    Description = game.Description
                };
            });
            mockMapper.Setup(x => x.Map<Game>(It.IsAny<object>())).Returns((object item) =>
            {
                var game = (GameViewModel)item;
                return new Game
                {
                    Key = game.Key,
                    Name = game.Name,
                    Description = game.Description
                };
            });
        }

        internal GamesController Build()
        {
            return new GamesController(mockService.Object, mockMapper.Object);
        }

        public GamesControllerBuilder WithGameService(Mock<IGameService> mockService)
        {
            this.mockService = mockService;
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

    internal class GameViewModelBuilder
    {
        private string _key;
        private string _name;
        private string _description;

        public GameViewModelBuilder()
        {
            var autoFixture = new Fixture();
            this._key = autoFixture.Create<string>();
            this._name = autoFixture.Create<string>();
            this._name = autoFixture.Create<string>();
        }

        public GameViewModel Build()
        {
            return new GameViewModel
            {
                Key = this._key,
                Name = this._name,
                Description = this._description
            };
        }

        public GameViewModelBuilder WithKey(string key)
        {
            this._key = key;
            return this;
        }

        public GameViewModelBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public GameViewModelBuilder WithDescription(string desc)
        {
            this._description = desc;
            return this;
        }
    }
}
