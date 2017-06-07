using System.Web.Mvc;
using GameForum.DAL.DAO;
using GameForum.DAL.Repositories;
using GameForum.DAL.Repositories.Abstractions;
using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Mappers;
using GameForum.DomainModel.Services;
using GameForum.DomainModel.Services.Abstractions;
using GameForum.Web.Infrastructure.Controllers;
using GameForum.Web.Infrastructure.Filters;
using GameForum.Web.Infrastructure.Models;
using GameForum.Web.Infrastructure.Services;
using GameForum.Web.Infrastructure.Services.Abstractions;
using Microsoft.Practices.Unity;

namespace GameForum.Web.Infrastructure.Bootstrapping
{
    public class Bootstrapper
    {
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IWrappedMapper, WrappedMapper>(new ContainerControlledLifetimeManager())
                .RegisterType<IGameRepository, GameRepository>()
                .RegisterType<ICommentRepository, CommentRepository>()
                .RegisterType<IGenreRepository, GenreRepository>()
                .RegisterType<IGameService, GameService>()
                .RegisterType<IConfigurationManager, DefaultConfigurationManager>()
                .RegisterType<IRequestIpLoggingService, RequestIpLoggingService>()
                .RegisterType<IPathHelper, PathHelper>()
                .RegisterType<LogRequestIpAttribute, LogRequestIpAttribute>(
                    new InjectionProperty("LoggingService", new ResolvedParameter<IRequestIpLoggingService>()))
                .RegisterType<GameController, GameController>()
                .RegisterType<GamesController, GamesController>();
            return container;
        }

        private static void BuildAutoMapper()
        {
            var mapper = DependencyResolver.Current.GetService<IWrappedMapper>();
            mapper.CreateMap<GameItem, Game>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Genre));
            mapper.CreateMap<Game, GameItem>();

            mapper.CreateMap<Genre, GenreItem>();
            mapper.CreateMap<GenreItem, Genre>().ForMember(dest => dest.GenreValue, opt => opt.MapFrom(src => src.Genre));

            mapper.CreateMap<Game, GameViewModel>();
            mapper.CreateMap<GameViewModel, Game>();
            
            mapper.CreateMap<CommentItem, Comment>();
            mapper.CreateMap<Comment, CommentItem>();

            mapper.CreateMap<Comment, CommentViewModel>();
            mapper.CreateMap<CommentViewModel, Comment>();            
        }

        public static void Initialise()
        {
            var container = BuildUnityContainer();
            IDependencyResolver resolver = DependencyResolver.Current;
            IDependencyResolver newResolver = new UnityDependencyResolver(container, resolver);
            DependencyResolver.SetResolver(newResolver);
            BuildAutoMapper();
        }

    }
}
