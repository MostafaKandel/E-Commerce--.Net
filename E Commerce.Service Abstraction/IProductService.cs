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

        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
