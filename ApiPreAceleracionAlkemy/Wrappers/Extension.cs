using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Wrappers
{
    public static class Extension
    {
        public static Task<Pagination<TDestination>> PaginatedResponseAsync<TDestination>(this IQueryable<TDestination> queryable,int pageNumber,int pageSize)
            => Pagination<TDestination>.CreatePage(queryable, pageNumber == 0 ? 1 : pageNumber, pageSize == 0 ? 10 :pageSize);
    }
}
