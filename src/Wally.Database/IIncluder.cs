using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Wally.Database
{
    public interface IIncluder<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Result
        {
            get;
        }

        //ThenIncluder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);

        //ThenIncluder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath);

        //ThenIncluder<TEntity, IEnumerable<TProperty>> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath);

        //ThenIncluder<TEntity, IEnumerable<TProperty>> Include<TPreviousProperty, TProperty>(Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)

        ThenIncluder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath);
    }
}