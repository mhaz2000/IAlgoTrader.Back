using IAlgoTrader.Back.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IAlgoTrader.Back.Base.Helpers
{
    public static class ClaimHelper
    {
        public static T GetClaim<T>(string accessToken, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(accessToken) as JwtSecurityToken;

            var claim = token.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            var result = claim.ToType<T>();
            return result;
        }

        public static bool HasClaim(string accessToken, string claimType)
        {
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrWhiteSpace(accessToken))
                return false;

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;

            var claim = tokenS.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            return claim != null;
        }

        public static Guid GetUserId(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, "id");
            return hasClaim ? GetClaim<Guid>(accessToken, "id") : Guid.Empty;
        }

        public static string GetUserName(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, ClaimTypes.GivenName);
            return hasClaim ? GetClaim<string>(accessToken, ClaimTypes.GivenName) : "";
        }

        public static string GetName(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, ClaimTypes.Name);
            return hasClaim ? GetClaim<string>(accessToken, ClaimTypes.Name) : "";
        }

        public static Guid GetBusinessId(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, "businessId");
            return hasClaim ? GetClaim<Guid>(accessToken, "businessId") : Guid.Empty;
        }

        public static Guid GetClientId(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, "clientId");
            return hasClaim ? GetClaim<Guid>(accessToken, "clientId") : Guid.Empty;
        }
    }
}
