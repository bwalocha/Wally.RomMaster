using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Wally.Database
{
    public class ThenIncluder<TEntity, TPreviousProperty> : Includer<TEntity>, IThenIncluder<TEntity, TPreviousProperty> where TEntity : class
    {
        private readonly IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> _querable;

        public ThenIncluder(IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> querable)
            : base(querable)
        {
            _querable = querable;
        }

        public ThenIncluder(IIncludableQueryable<TEntity, TPreviousProperty> querable)
            : base(querable)
        {
        }

        public ThenIncluder<TEntity, TProperty> ThenInclude<TProperty>(Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
        {
            IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source = _querable;
            IIncludableQueryable<TEntity, TProperty> q = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ThenInclude(source, navigationPropertyPath);

            return new ThenIncluder<TEntity, TProperty>(q);
        }
    }
}