using Specifications.Core.Exceptions;
using Specifications.Core.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Specifications.Core
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public virtual Expression<Func<T, bool>> Predicate { get; protected set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>> Sort { get; set; }

        public Func<IQueryable<T>, IQueryable<T>> PostProcess { get; set; }

        protected Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            Validate(specification);
            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> And(Expression<Func<T, bool>> predicate)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(predicate));
        }

        public virtual bool IsSatisfiedBy(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (Predicate is null)
            {
                throw new InvalidSpecificationException(nameof(Predicate));
            }

            var predicate = Predicate.Compile();

            return predicate(entity);
        }

        public IQueryable<T> Prepare(IQueryable<T> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (Predicate is null)
            {
                throw new InvalidSpecificationException(nameof(Predicate));
            }

            var result = query.Where(Predicate);

            if (Sort != null)
            {
                var sorted = Sort(result);
                return PostProcess(sorted); 
            }

            if (PostProcess != null)
            {
                return PostProcess(result);
            }

            return result;
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            Validate(specification);
            return new OrSpecification<T>(this, specification);
        }

        public ISpecification<T> Or(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new OrSpecification<T>(this, new ExpressionSpecification<T>(predicate));
        }

        public ISpecification<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> property)
        {
            if (property is null)
            {
                return this;
            }

            var newSpecification = new ExpressionSpecification<T>(Predicate) { PostProcess = PostProcess };

            if (Sort != null)
            {
                newSpecification.Sort = items => Sort(items).ThenBy(property);
            }
            else
            {
                newSpecification.Sort = items => items.OrderBy(property);
            }

            return newSpecification;
        }

        public T SatisfyingItemFrom(IQueryable<T> query)
        {
            return Prepare(query).SingleOrDefault();
        }

        public IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query)
        {
            return Prepare(query);
        }

        public ISpecification<T> Skip(int amount)
        {
            var newSpecification = new ExpressionSpecification<T>(Predicate) { Sort = Sort };

            if (PostProcess is null)
            {
                newSpecification.PostProcess = items => PostProcess(items).Skip(amount);
            }
            else
            {
                newSpecification.PostProcess = items => items.Skip(amount);
            }

            return newSpecification;
        }

        public ISpecification<T> Take(int amount)
        {
            var newSpecification = new ExpressionSpecification<T>(Predicate) { Sort = Sort };

            if (PostProcess is null)
            {
                newSpecification.PostProcess = items => PostProcess(items).Take(amount);
            }
            else
            {
                newSpecification.PostProcess = items => items.Take(amount);
            }

            return newSpecification;
        }

        private void Validate(ISpecification<T> specification)
        {
            if (Predicate is null)
            {
                throw new InvalidSpecificationException(
                    "Cannot compose an empty specification with another specification.");
            }
            if (specification is null)
            {
                throw new ArgumentNullException(nameof(specification));
            }
        }
    }
}