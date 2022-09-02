using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.SeedWorks.Infrastructure;
using IAlgoTrader.Back.Service.SeedWorks.Models;
using Microsoft.AspNetCore.Identity;

namespace IAlgoTrader.Back.Service.SeedWorks.Interfaces
{
    public interface ITokenGenerator
    {
        JwToken TokenGeneration(User user, JwtIssuerOptions jwtOptions, IList<IdentityRole> userRoles);
    }
}
