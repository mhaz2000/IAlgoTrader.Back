using IAlgoTrader.Back.Domain.Entities;
using System.Security.Claims;

namespace IAlgoTrader.Back.Service.SeedWorks.Interfaces
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(User user, IList<string> userRoles, IEnumerable<string> roleIds, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
