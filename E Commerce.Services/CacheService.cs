using E_Commerce.Domain.Contracts;
using E_Commerce.Service_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository) {
            _cacheRepository = cacheRepository;
        }
        public async Task<string?> GetAsync(string key)
        {
            return await _cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string Cachekey, object CacheValue, TimeSpan TimeToLive)
        {
            var Values= JsonSerializer.Serialize(CacheValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy= JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.SetAsync(Cachekey, Values, TimeToLive);

        }
    }
}
