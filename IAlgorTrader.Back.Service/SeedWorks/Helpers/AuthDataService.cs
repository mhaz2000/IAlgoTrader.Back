namespace IAlgorTrader.Back.Service.SeedWorks.Helpers
{
    public static class DataService
    {
        public static readonly string AdminUserRole = "Admin";
        public static readonly string AdminPassword = "P@ssw0rdadmin";
        public static readonly string CustomerUserRole = "Company";


        public static class JwtClaimIdentifiers
        {
            public const string Rol = "rol", Id = "id";
        }

        public static class JwtClaims
        {
            public const string ApiAccess = "api_access";
        }
    }
}
