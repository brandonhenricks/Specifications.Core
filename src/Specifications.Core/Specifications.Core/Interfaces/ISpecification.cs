using System;
using System.Linq;
using System.Linq.Expressions;

namespace Specifications.Core.Interfaces
{
    public interface ISpecification<T>
    {
        Func<IQueryable<T>, IOrderedQueryable<T>> Sort { get; set; }

        Func<IQueryable<T>, IQueryable<T>> PostProcess { get; }

        Expression<Func<T, bool>> Predicate { get; }

        ISpecification<T> And(ISpecification<T> specification);

        ISpecification<T> And(Expression<Func<T, bool>> predicate);

        ISpecification<T> Not();

        ISpecification<T> Or(ISpecification<T> specification);

        ISpecification<T> Or(Expression<Func<T, bool>> predicate);

        ISpecification<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> property);

        ISpecification<T> Take(int amount);

        ISpecification<T> Skip(int amount);

        bool IsSatisfiedBy(T entity);

        T SatisfyingItemFrom(IQueryable<T> query);

        IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query);

        IQueryable<T> Prepare(IQueryable<T> query);
    }
}