using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services.Abstractions;
using GameForum.Web.Infrastructure.Controllers;
using GameForum.Web.Infrastructure.Models;
using GameForum.Web.Infrastructure.Services.Abstractions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using NUnit.Framework;

namespace GameForum.Web.Infrastructure.Test.Controllers
{
    [TestFixture]
    public class GameControllerTest
    {
        [Test]
        public void GameController_IsControllerImplemented()
        {
            var sut = new GameControllerBuilder().Build();

            Assert.IsInstanceOf<Controller>(sut);
        }

        [Test]
        [TestCase("key", "comment")]
        public void NewComment_ValidDataPassed_GameServiceCalledWithCorrectParams(string key, string comment)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            GameController sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            var viewModel = new NewCommentViewModel
            {
                Comment = comment
            };

            //Act
            sut.NewComment(viewModel);

            //Assert
            gameService.Verify(x => x.AddCommentToGame(It.Is<string>(item => item.Equals(key)),
                It.Is<string>(item => item.Equals(viewModel.Comment))), Times.Exactly(1));
        }

        [Test]
        [TestCase("key", "")]
        [TestCase("", "comment")]
        [TestCase("", "")]
        public void NewComment_InvalidDataPassed_ErrorReturned(string key, string comment)
        {
            //Arrange
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().Build();
            sut.ControllerContext = controllerContext.Object;
            var viewModel = new NewCommentViewModel { Comment = comment };
            string errMessage = "invalid input";

            //Act
            sut.ModelState.AddModelError("param", "invalid");
            var res = sut.NewComment(viewModel) as PartialViewResult;
            var data = res.Model as NewCommentResponseViewModel;

            //Assert
            Assert.IsTrue(data.ErrorMessage.Equals(errMessage));
        }

        [Test]
        [TestCase("key", "")]
        [TestCase("", "comment")]
        [TestCase("", "")]
        public void NewComment_InvalidDataPassed_GameServiceWasntCalled(string key, string comment)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;
            var viewModel = new NewCommentViewModel { Comment = comment };

            //Act
            sut.ModelState.AddModelError("param", "invalid");
            sut.NewComment(viewModel);

            //Assert
            gameService.Verify(x => x.AddCommentToGame(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));
        }

        [Test]
        [TestCase("key", "comment")]
        public void NewComment_ValidDataPassed_ErrorInGameService_ErrorReturnedInJSON(string key, string comment)
        {
            //Arrange
            string errMessage = "Error on add comment";
            var viewModel = new NewCommentViewModel { Comment = comment };
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.AddCommentToGame(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception(errMessage));
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var res = sut.NewComment(viewModel) as PartialViewResult;
            var data = res.Model as NewCommentResponseViewModel;

            //Assert
            Assert.IsTrue(errMessage.Equals(data.ErrorMessage));
        }

        [Test]
        [TestCase("key", "comment", 1)]
        public void Comments_ValidDataPassed_CommentsDataAsExpected(string key, string comment, int commentId)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetCommentsByGameKey(It.IsAny<string>())).Returns(
                new [] { new Comment {Body = comment, CommentId = commentId, GameKey = key} });
            var expected = new[] { new CommentViewModel { Body = comment, CommentId = commentId, GameKey = key } };
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var res = sut.Comments() as ViewResult;
            var data = (res.Model as CommentsViewModel).Comments;

            //Assert
            Assert.IsTrue(compareObjects.Compare(data.ToArray(), expected.ToArray()));
        }

        [Test]
        [TestCase("key", "comment", 1)]
        public void Comments_ValidDataPassed_GameServiceCalledWithCorrectParam(string key, string comment, int commentId)
        {
            //Arrange
            
            var model = new CommentsByGameViewModel { GameKey = key };
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetCommentsByGameKey(key)).Returns(
                new[] { new Comment() { Body = comment, CommentId = commentId, GameKey = key } });
            
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            sut.Comments();
            
            //Assert
            gameService.Verify(x => x.GetCommentsByGameKey(It.Is<string>(item => item.Equals(model.GameKey))), Times.Once);
        }
        
        [Test]
        [TestCase("key")]
        public void Comments_ValidDataPassed_ErrorInGameService_ErrorReturned(string key)
        {
            //Arrange
            string errMessage = "Error on get comments";
            var viewModel = new CommentsByGameViewModel { GameKey = key };
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetCommentsByGameKey(It.IsAny<string>()))
                .Throws(new Exception(errMessage));
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var res = sut.Comments() as ViewResult;
            
            //Assert
            Assert.IsNotNull(res);
            Assert.IsTrue(sut.ModelState.Values.Count > 0);
        }

        [Test]
        [TestCase("key", "desc", "name")]
        public void GameDetails_GameDetailsAsExpected(string key, string desc, string name)
        {
            //Arrange
            var compareObjects = new CompareObjects();
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetGameByKey(key)).Returns(
                new Game { Description = desc, Key = key, Name = name });
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            var expected = new GameViewModel() { Name = name, Key = key, Description = desc };
            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var result = sut.GameDetails() as ViewResult;
            var data = result.Model as GameViewModel;

            //Assert
            Assert.IsTrue(compareObjects.Compare(expected, data));
        }

        [Test]
        [TestCase("key")]
        public void GameDetails_GameServiceCallWithCorrectParam(string key)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            
            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            sut.GameDetails();
            
            //Assert
            gameService.Verify( x => x.GetGameByKey(It.Is<string>(item => item.Equals(key))), Times.Once);
        }

        [Test]
        [TestCase("key")]
        public void GameDetails_ErrorInGameService_ErrorReturned(string key)
        {
            //Arrange
            string errMessage = "Error on get game details";
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Throws(new Exception(errMessage));
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });

            var sut = new GameControllerBuilder().WithGameService(gameService).Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            sut.GameDetails();
            

            //Assert
            Assert.IsTrue(errMessage.Equals(sut.ModelState["ServiceError"].Errors[0].ErrorMessage));
        }

        [Test]
        [TestCase("key1", @"D:\Logs\")]
        public void Download_ReturnedResultIsFilePathResult(string key, string path)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(key)).Returns(true);
            var pathHelper = new Mock<IPathHelper>();
            pathHelper.Setup(x => x.MapPathToGame(path, key))
                .Returns(string.Concat(path, key));
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request.PhysicalPath).Returns(path);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);
            var sut = new GameControllerBuilder().WithPathHelper(pathHelper).WithGameService(gameService)
                .Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var result = sut.Download() as FilePathResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("key1", @"D:\Logs\")]
        public void Download_PathHelperCalled(string key, string gamePath)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(key)).Returns(true);
            var pathHelper = new Mock<IPathHelper>();
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request.PhysicalPath).Returns(gamePath);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var sut = new GameControllerBuilder().WithPathHelper(pathHelper).WithGameService(gameService)
                .Build();
            sut.ControllerContext = controllerContext.Object;
            
            //Act
            sut.Download();

            //Assert
            pathHelper.Verify(x => x.MapPathToGame(
                It.Is<string>(item => item.Equals(gamePath)),
                It.Is<string>(item => item.Equals(key))), Times.Once);
        }

        [Test]
        [TestCase("key1", @"D:\Logs\")]
        public void Download_ErrorOnHelperCalled(string key, string gamePath)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(key)).Returns(true);
            var errMessage = "Error on download game";
            var pathHelper = new Mock<IPathHelper>();
            pathHelper.Setup(x => x.MapPathToGame(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception(errMessage));
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request.PhysicalPath).Returns(gamePath);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var sut = new GameControllerBuilder().WithPathHelper(pathHelper).WithGameService(gameService)
                .Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var res = sut.Download() as JsonResult;
            var data = res.Data as GameResponseViewModel;

            //Assert
            Assert.IsTrue(errMessage.Equals(data.ErrorMessage));
        }

        [Test]
        [TestCase("key1", @"D:\Logs\")]
        public void Download_GameNonExist_InValidInputReturned(string key, string gamePath)
        {
            //Arrange
            var gameService = new Mock<IGameService>();
            gameService.Setup(x => x.IsGameExist(key)).Returns(false);
            var errMessage = "invalid input";
            var pathHelper = new Mock<IPathHelper>();
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request.PhysicalPath).Returns(gamePath);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.RouteData).Returns(
                () =>
                {
                    var rd = new RouteData();
                    rd.Values.Add("gameKey", key);
                    return rd;
                });
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var sut = new GameControllerBuilder().WithPathHelper(pathHelper).WithGameService(gameService)
                .Build();
            sut.ControllerContext = controllerContext.Object;

            //Act
            var res = sut.Download() as JsonResult;
            var data = res.Data as GameResponseViewModel;

            //Assert
            Assert.IsTrue(errMessage.Equals(data.ErrorMessage));
        }
    }

    internal class GameControllerBuilder
    {
        private Mock<IGameService> gameService;
        private Mock<IWrappedMapper> mapper;
        private Mock<IPathHelper> pathHelper;

        public GameControllerBuilder()
        {
            this.gameService = new Mock<IGameService>();
            this.mapper = new Mock<IWrappedMapper>();

            mapper.Setup(x => x.Map<GameViewModel>(It.IsAny<object>())).Returns(
                (object item) =>
                {
                    var game = (Game)item;
                    return new GameViewModel
                    {
                        Key = game.Key,
                        Description = game.Description,
                        Name = game.Name
                    };
                });

            mapper.Setup(x => x.Map<CommentViewModel>(It.IsAny<object>())).Returns(
                (object item) =>
                {
                    var comment = (Comment)item;
                    return new CommentViewModel
                    {
                        CommentId = comment.CommentId,
                        Body = comment.Body,
                        GameKey = comment.GameKey
                    };
                });

            this.pathHelper = new Mock<IPathHelper>();
        }

        public GameController Build()
        {
            return new GameController(this.gameService.Object, this.mapper.Object, this.pathHelper.Object);
        }

        public GameControllerBuilder WithGameService(Mock<IGameService> gameMock)
        {
            this.gameService = gameMock;
            return this;
        }

        public GameControllerBuilder WithPathHelper(Mock<IPathHelper> helpMock)
        {
            this.pathHelper = helpMock;
            return this;
        }
    }
}
