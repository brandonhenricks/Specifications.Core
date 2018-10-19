using System;
using System.Linq.Expressions;

namespace Specifications.Core
{
    public class NullSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> Predicate { get; protected set; }

        public NullSpecification()
        {
        }
    }
}