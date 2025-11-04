using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Intialize()
        {
            try
            {
                var HasProducts = _dbContext.Products.Any();
                var HasBrands = _dbContext.ProductBrands.Any();
                var HasTypes = _dbContext.ProductTypes.Any();
                if(HasBrands && HasProducts && HasTypes) return;
                if(!HasBrands)
                {
                    SeedDataFromJson<ProductBrand,int>("brands.json", _dbContext.ProductBrands);
                }
                if(!HasTypes)
                {
                    SeedDataFromJson<ProductType,int>("types.json", _dbContext.ProductTypes);
                }
                _dbContext.SaveChanges();
                if (!HasProducts)
                {
                    SeedDataFromJson<Product,int>("products.json", _dbContext.Products);
                }
                _dbContext.SaveChanges();



            }
            catch(Exception ex)
            {
                Console.WriteLine($"Data Seed is Failed {ex}");
            }
        }

        private void SeedDataFromJson<T,TKey>(string FileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {

            var FilePath=  @"..\E-Commerce.Persistence\Data\DataSeed\JSONFiles\"+ FileName;
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {FileName} is not Exist");
            try
            {
               using var dataStreams= File.OpenRead(FilePath);
                var data= JsonSerializer.Deserialize<List<T>>(dataStreams, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive= true
                });
                if (data is not null)
                {
                    dbset.AddRange(data);

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while Reading Json File: {ex} ");

            }
        }
    }
}
