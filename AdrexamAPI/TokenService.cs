using AdrexamAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;

namespace AdrexamAPI
{
    public class TokenService
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _memoryCache;
        public TokenService(DataContext dataContext, IMemoryCache memoryCache)
        {
            _dataContext = dataContext;
            _memoryCache = memoryCache;
        }

        public async Task<bool> ValidateAsync(string apiKey, CancellationToken cancellationToken)
        {
            var getTokenFromCache = _memoryCache.Get(apiKey);

            if(getTokenFromCache == null)
            {
                var tokens = await _dataContext.Tokens.FirstOrDefaultAsync(d => d.Token == apiKey, cancellationToken);
                if (tokens == null)
                {
                    return false;
                }
                else
                {
                    if (tokens.Expired == null)
                    {
                        _memoryCache.Set(apiKey, tokens, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                        });
                        return true;
                    }
                    if (tokens.Expired > DateTime.Now)
                    {
                        _memoryCache.Set(apiKey, tokens, tokens.Expired.Value - DateTime.Now);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
    }
}