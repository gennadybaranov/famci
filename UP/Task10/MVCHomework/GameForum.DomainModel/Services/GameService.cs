using System;
using System.Collections.Generic;
using System.Linq;
using GameForum.DAL.DAO;
using GameForum.DAL.Repositories.Abstractions;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Exceptions;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services.Abstractions;

namespace GameForum.DomainModel.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IWrappedMapper mapper;

        public GameService(IGameRepository gameRepository, ICommentRepository commentRepository, IGenreRepository genreRepository, IWrappedMapper mapper)
        {
            if (gameRepository == null)
            {
                throw new ArgumentNullException("IGameRepository is null.");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("IWrappedMapper is null.");
            }

            if (commentRepository == null)
            {
                throw new ArgumentNullException("ICommentRepository is null.");
            }

            if (genreRepository == null)
            {
                throw new ArgumentNullException("IGenreRepository is null.");
            }

            this.gameRepository = gameRepository;
            this.commentRepository = commentRepository;
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        public IEnumerable<Game> GetAllGames()
        {
            IEnumerable<GameItem> res = this.gameRepository.GetAllGames();
            return res.Select(item => mapper.Map<Game>(item)).AsEnumerable();
        }

        public void CreateNewGame(Game newGame)
        {
            if (string.IsNullOrEmpty(newGame.Key))
            {
                throw new ArgumentException("Game key is invalid value.");
            }

            if (this.gameRepository.IsGameExist(newGame.Key))
            {
                throw new GameExistException("Such game already exists.");
            }

            var gameItem = mapper.Map<GameItem>(newGame);

            this.gameRepository.CreateNewGame(gameItem);
            this.gameRepository.Save();
        }

        public void EditGame(Game editedGame)
        {
            if (!this.gameRepository.IsGameExist(editedGame.Key))
            {
                throw new NonExistGameException(string.Format("Game {0} non exist", editedGame.Key));
            }

            var gameItem = this.mapper.Map<GameItem>(editedGame);
            this.gameRepository.EditGame(gameItem);
            this.gameRepository.Save();
        }

        public void DeleteGame(string key)
        {
            if (!this.gameRepository.IsGameExist(key))
            {
                throw new NonExistGameException(string.Format("Game {0} non exist", key));
            }

            this.gameRepository.DeleteGame(key);
            this.gameRepository.Save();
        }

        public Game GetGameByKey(string key)
        {
            if (!this.gameRepository.IsGameExist(key))
            {
                throw new NonExistGameException(string.Format("Game {0} non exist", key));
            }

            GameItem game = this.gameRepository.GetGameByKey(key);
            return this.mapper.Map<Game>(game);
        }

        public bool IsGameExist(string key)
        {
            return this.gameRepository.IsGameExist(key);
        }

        public void AddCommentToGame(string key, string comment)
        {
            if (!this.gameRepository.IsGameExist(key))
            {
                throw new NonExistGameException(string.Format("Game {0} not exist", key));
            }

            this.commentRepository.AddCommentToGame(key, comment);
            this.commentRepository.Save();
        }

        public IEnumerable<Comment> GetCommentsByGameKey(string key)
        {
            if (!this.gameRepository.IsGameExist(key))
            {
                throw new NonExistGameException(string.Format("Game {0} not exist", key));
            }

            IEnumerable<CommentItem> res = this.commentRepository.GetCommentsByGameKey(key);
            return res.Select(item => this.mapper.Map<Comment>(item)).AsEnumerable();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            IEnumerable<GenreItem> res = this.genreRepository.GetAllGenres();
            return res.Select(item => mapper.Map<Genre>(item)).AsEnumerable();
        }
    }
}
