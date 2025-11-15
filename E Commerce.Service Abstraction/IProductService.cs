using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service_Abstraction
{
    public interface IProductService
    {
        

        Task<ProductDTO?> GetProductByIdAsync(int id);

        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
