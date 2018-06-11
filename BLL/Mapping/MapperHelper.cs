using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public static class MapperHelper
    {
        public static IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            //Mapper.Initialize(cfg=>cfg.CreateMap<TSource, TDestination>());

            var destination = new List<TDestination>();
            if (source != null)
            {
                foreach (var s in source)
                {
                    destination.Add(Mapper.Map<TSource, TDestination>(s));
                }
            }
            return destination;
        }

        public static TDestination ConvertoDto<TSource,TDestination>(this TSource model)  => Mapper.Map<TSource, TDestination>(model);

        public static TDestination GetPrototype<TSource, TDestination>(this TSource model) => Mapper.Map<TSource, TDestination>(model);

        public static IEnumerable<TDestination> ConvertoDto<TSource, TDestination>(this IEnumerable<TSource> models) => MapperHelper.Map<TSource, TDestination>(models);

        public static IEnumerable<TDestination> GetPrototype<TSource, TDestination>(this IEnumerable<TSource> models) => MapperHelper.Map<TSource, TDestination>(models);
    }
}
