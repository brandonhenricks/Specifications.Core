using Specifications.Core.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Specifications.Core
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;

        public override Expression<Func<T, bool>> Predicate => Not(_left.Predicate);

        public NotSpecification(ISpecification<T> left)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
        }

        private static Expression<Func<T, bool>> Not(Expression<Func<T, bool>> left)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            var notExpression = Expression.Not(left.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(notExpression, left.Parameters.Single());
            return lambda;
        }

        public override bool IsSatisfiedBy(T target)
        {
            return !_left.IsSatisfiedBy(target);
        }
    }
}