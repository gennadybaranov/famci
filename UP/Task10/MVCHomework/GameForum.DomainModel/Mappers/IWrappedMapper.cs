using AutoMapper;
namespace GameForum.DomainModel.Mappers
{
    public interface IWrappedMapper
    {
        IMappingExpression<TSource, TTarget> CreateMap<TSource, TTarget>();
        TTarget Map<TTarget>(object item);
    }
}
