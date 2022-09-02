using Microsoft.AspNetCore.Identity;

namespace IAlgoTrader.Back.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            SecurityStamp = Guid.NewGuid().ToString();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public Guid LogoId { get; set; }

        public void AddRefreshToken(string token, string userId, double min = 10)
        {
            RefreshTokens.Add(new RefreshToken(token, DateTime.Now.AddMinutes(min), userId));
        }

        public class RefreshToken
        {
            public Guid Id { get; set; }
            public string Token { get; private set; }
            public DateTime Expires { get; private set; }
            public string UserId { get; private set; }
            public bool Active => DateTime.Now <= Expires;

            public RefreshToken(string token, DateTime expires, string userId)
            {
                Id = Guid.NewGuid();
                Token = token;
                Expires = expires;
                UserId = userId;
            }
        }
    }
}
