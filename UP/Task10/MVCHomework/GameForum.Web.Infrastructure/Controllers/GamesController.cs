using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services.Abstractions;
using GameForum.Web.Infrastructure.Models;

namespace GameForum.Web.Infrastructure.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService gameService;
        private readonly IWrappedMapper mapper;

        public GamesController(IGameService gameService, IWrappedMapper mapper)
        {
            if (gameService == null)
            {
                throw new ArgumentNullException("IGameService is null");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("IWrappedMapper is null");
            }

            this.gameService = gameService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ViewResult New()
        {
            return View("New", new GameViewModel());
        }

        [HttpPost]
        public ActionResult New(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGame = this.mapper.Map<Game>(game);
                    this.gameService.CreateNewGame(newGame);
                    return RedirectToAction("AllGames");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ServiceError", ex.Message);
                }
            }

            return View("new", game);
        }

        [HttpPost]
        public JsonResult Update(GameViewModel game)
        {
            var response = new UpdateGameResponseViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var editedGame = this.mapper.Map<Game>(game);
                    this.gameService.EditGame(editedGame);
                }
                catch (Exception)
                {
                    response.ErrorMessage = "Error on edit game";
                }
            }
            else
            {
                response.ErrorMessage = "invalid input";
            }

            return Json(response);
        }

        [HttpGet]
        public JsonResult AllGames()
        {
            IEnumerable<GameViewModel> games =
                this.gameService.GetAllGames().Select(item => this.mapper.Map<GameViewModel>(item)).AsEnumerable();

            return new JsonResult { Data = games, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [OutputCache(Duration = 60)]
        public JsonResult GamesCount()
        {
            var response = new GamesCountViewModel();
            IEnumerable<GameViewModel> games =
                this.gameService.GetAllGames().Select(item => this.mapper.Map<GameViewModel>(item)).AsEnumerable();
            response.Count = games.Count();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(string gameKey)
        {
            var response = new RemoveGameResponseViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    this.gameService.DeleteGame(gameKey);
                }
                catch (Exception)
                {
                    response.ErrorMessage = "Error on remove game";
                }
            }
            else
            {
                response.ErrorMessage = "invalid input";
            }

            return Json(response);
        }

        [HttpGet]
        public JsonResult CheckName(string name)
        {
            if (this.gameService.IsGameExist(name))
            {
                return Json("Such game already exists. Please, select another one", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Genres()
        {
            var genres = this.gameService.GetAllGenres().Select
                (x => new SelectListItem
                {
                    Text = x.GenreValue,
                    Value = x.GenreId.ToString(),
                    Selected = false
                });
            return Json(genres, JsonRequestBehavior.AllowGet);
        }
    }


}
