using System.Collections.Concurrent;

namespace Vyg.Assessment.BE.Utility
{
    public static class JwtTokenHandler
    {
        private static readonly ConcurrentDictionary<string, bool> _blacklistedTokens = new ConcurrentDictionary<string, bool>();

        public static void AddToken(string token)
        {
            _blacklistedTokens[token] = true;
        }

        public static bool IsBlacklisted(string token)
        {
            return _blacklistedTokens.ContainsKey(token);
        }
    }
}
