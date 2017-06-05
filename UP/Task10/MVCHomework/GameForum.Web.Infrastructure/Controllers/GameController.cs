using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services.Abstractions;
using GameForum.Web.Infrastructure.Models;
using GameForum.Web.Infrastructure.Services.Abstractions;

namespace GameForum.Web.Infrastructure.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IWrappedMapper mapper;
        private readonly IPathHelper pathHelper;

        public GameController(IGameService gameService, IWrappedMapper mapper,
            IPathHelper pathHelper)
        {
            if (gameService == null)
            {
                throw new ArgumentNullException("IGameService is null");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("IWrappedMapper is null");
            }

            if (pathHelper == null)
            {
                throw new ArgumentNullException("IPathHelper is null");
            }

            this.gameService = gameService;
            this.mapper = mapper;
            this.pathHelper = pathHelper;
        }

        [HttpGet]
        public ActionResult GameDetails()
        {
            var key = RouteData.Values["gameKey"].ToString();
            try
            {
                return View(this.mapper.Map<GameViewModel>(this.gameService.GetGameByKey(key)));
            }
            catch (Exception)
            {
                ModelState.AddModelError("ServiceError", "Error on get game details");
            }

            return View();
        }

        [HttpPost]
        public PartialViewResult NewComment(NewCommentViewModel newComment)
        {
            var key = RouteData.Values["gameKey"].ToString();
            var response = new NewCommentResponseViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    response.NewComment = newComment.Comment;
                    this.gameService.AddCommentToGame(key, newComment.Comment);
                }
                catch (Exception)
                {
                    response.ErrorMessage = "Error on add comment";
                }
            }
            else
            {
                response.ErrorMessage = "invalid input";
            }

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                return PartialView("CommentLine", response);
            }

            return PartialView("ErrorLine", response);
        }

        [HttpGet]
        public ActionResult Comments()
        {
            var key = RouteData.Values["gameKey"].ToString();
            try
            {
                IEnumerable<CommentViewModel> comments = this.gameService.GetCommentsByGameKey(key)
                    .Select(item => mapper.Map<CommentViewModel>(item)).AsEnumerable();
                return View(new CommentsViewModel { Comments = comments });
            }
            catch (Exception)
            {
                ModelState.AddModelError("ServiceError", "Error on get comments");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Download()
        {
            var key = RouteData.Values["gameKey"].ToString();
            string path = HttpContext.Request.PhysicalPath;
            var response = new GameResponseViewModel();
            try
            {
                if (this.gameService.IsGameExist(key))
                {
                    string filePath = this.pathHelper.MapPathToGame(path, key);
                    return new FilePathResult(filePath, "application/octet-stream");
                }
                else
                {
                    response.ErrorMessage = "invalid input";
                }
            }
            catch (Exception)
            {
                response.ErrorMessage = "Error on download game";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }        
    }
}
