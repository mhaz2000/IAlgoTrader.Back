using IAlgorTrader.Back.Service.SeedWorks.Interfaces;
using System.Security.Cryptography;

namespace IAlgorTrader.Back.Service.SeedWorks.Helpers
{
    public class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
