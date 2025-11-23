using E_Commerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Exceptions
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }

    public sealed class ProductNotFoundException(int productId)
        : NotFoundException($"Product with ID {productId} was not found.")
    {
    }

    public sealed class BasketNotFoundException(string id): NotFoundException($"Product with ID {id} was not found.")
    {
        
    }
}
