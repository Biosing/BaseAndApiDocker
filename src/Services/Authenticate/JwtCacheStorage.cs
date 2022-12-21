using Models;
using Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Authenticate
{
    public class JwtCacheStorage : IJwtCacheStorage
    {
        private const string KeyPrefix = "User_JwtToken_Id:";

        private readonly ICache _cache;

        public JwtCacheStorage(ICache cache)
        {
            _cache = cache;
        }

        private async Task<Jwt> JwtByUserIdOrNullAsync(long userId)
        {
            var serialized = await _cache.GetAsync(Key(userId));
            return serialized is not null ? JsonSerializer.Deserialize<JwtItem>(serialized)?.AsJwt() : null;
        }

        public async Task SaveJwtAsync(long userId, Jwt jwt)
        {
            var itemToCache = new JwtItem(jwt);
            await _cache.SetAsync(Key(userId), JsonSerializer.Serialize(itemToCache), TimeSpan.FromHours(24));
        }

        public async Task<bool> IsValidAsync(long userId, string token)
        {
            Jwt cached = await JwtByUserIdOrNullAsync(userId);
            if (cached == null)
            {
                return true;
            }

            if (DateTimeOffset.Now.Later(cached.ExpiresAt))
            {
                await _cache.RemoveAsync(Key(userId));
                return true;
            }

            return cached.ApiToken == token;
        }

        private string Key(long userId) => KeyPrefix + userId;

        private sealed class JwtItem
        {
            public string Token { get; init; }

            public DateTimeOffset ExpiredAt { get; init; }

            public JwtItem()
            {
            }

            public JwtItem(Jwt jwt)
            {
                Token = jwt.ApiToken;
                ExpiredAt = jwt.ExpiresAt;
            }

            public Jwt AsJwt()
            {
                return new Jwt(Token, ExpiredAt);
            }
        }
    }
}
