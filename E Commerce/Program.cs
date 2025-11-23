
using E_Commerce.Custom_Middlewares;
using E_Commerce.Domain.Contracts;
using E_Commerce.Extensions;
using E_Commerce.Factories;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Service_Abstraction;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataIntializer, DataIntializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(x => x.AddProfile<BasketProfile>());
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<ProductPictureUrlResolver>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
              
            });
            #endregion

            var app = builder.Build();

            #region Data Seed - Apply Migrations

            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync();


            #endregion

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseStaticFiles();

            await app.RunAsync();
        }
    }
}
