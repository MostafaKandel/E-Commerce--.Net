using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public static class ProductSpecificationHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams queryParams)
        {
            return p => (queryParams.brandId == null || p.BrandId == queryParams.brandId) && (queryParams.typeId == null || p.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower()));
        }
    }
}
