using System;
using System.Linq.Expressions;

namespace Specifications.Core
{
    public class ExpressionSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public override Expression<Func<T, bool>> Predicate => _predicate;

        public ExpressionSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate;
        }
    }
}