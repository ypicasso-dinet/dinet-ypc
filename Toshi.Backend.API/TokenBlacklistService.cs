//using StackExchange.Redis;

//namespace Toshi.Backend.API
//{
//    public class TokenBlacklistService : ITokenBlacklistService
//    {
//        private readonly IDatabase _database;

//        public TokenBlacklistService(IConnectionMultiplexer redis)
//        {
//            _database = redis.GetDatabase();
//        }

//        public void InvalidateToken(string token, DateTime expiry)
//        {
//            try
//            {
//                _database.StringSet(token, "invalid", expiry - DateTime.UtcNow);
//            }
//            catch (Exception ex)
//            {
//                //
//            }
//        }

//        public bool IsTokenBlacklisted(string token)
//        {
//            return _database.StringGet(token) == "invalid";
//        }
//    }
//}
