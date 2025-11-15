using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
 public static class SpecificationEvaluate
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
           var query = EntryPoint;
            if (specifications is not null)

            {
                if (specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }


                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    query = specifications.IncludeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
                }

                if (specifications.OrderBy is not null)
                {
                    query = query.OrderBy(specifications.OrderBy);
                }
                if (specifications.OrderByDescending is not null)
                {
                    query = query.OrderByDescending(specifications.OrderByDescending);
                }
                
                if(specifications.IsPaginated)
                {
                    query = query.Skip(specifications.Skip).Take(specifications.Take);
                    //if (specifications.Skip.HasValue)
                    //{
                    //    query = query.Skip(specifications.Skip.Value);
                    //}
                    //if (specifications.Take.HasValue)
                    //{
                    //    query = query.Take(specifications.Take.Value);
                    //}
                }

            }
            
            
            
            return query;
        }
    }
}
