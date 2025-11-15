using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpecifications: BaseSpecifications<Product,int>
    {
        public ProductWithTypeAndBrandSpecifications(ProductQueryParams queryParams)
            :base(ProductSpecificationHelper.GetProductCriteria(queryParams))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch(queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                        AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                        AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        } 
        public ProductWithTypeAndBrandSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
