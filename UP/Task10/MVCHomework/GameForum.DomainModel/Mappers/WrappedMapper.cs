using AutoMapper;

namespace GameForum.DomainModel.Mappers
{
    public class WrappedMapper : IWrappedMapper
    {
        public IMappingExpression<TSource, TTarget> CreateMap<TSource, TTarget>()
        {
            return Mapper.CreateMap<TSource, TTarget>();
        }

        public TTarget Map<TTarget>(object item)
        {
            return Mapper.Map<TTarget>(item);
        }
    }
}
