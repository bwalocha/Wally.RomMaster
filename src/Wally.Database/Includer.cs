using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Wally.Database
{
    public class Includer<TEntity> : IIncluder<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Result
        {
            get;
            private set;
        }

        public Includer(IQueryable<TEntity> querable)
        {
            Result = querable;
        }

        public ThenIncluder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath)
        {
            IQueryable<TEntity> source = Result;
            IIncludableQueryable<TEntity, IEnumerable<TProperty>> q = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(source, navigationPropertyPath);

            return new ThenIncluder<TEntity, TProperty>(q);
        }
    }
}