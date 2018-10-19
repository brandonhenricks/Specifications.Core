using Specifications.Core.Interfaces;
using System.Linq;

namespace Specifications.Core.Extensions
{
    public static class SpecificationExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> q, ISpecification<T> spec)
        {
            return q.Where(spec.Predicate);
        }

        public static T FirstOrDefault<T>(this IQueryable<T> q, ISpecification<T> spec)
        {
            return q.FirstOrDefault(spec.Predicate);
        }
    }
}