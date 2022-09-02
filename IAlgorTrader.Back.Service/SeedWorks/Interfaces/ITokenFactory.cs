namespace IAlgorTrader.Back.Service.SeedWorks.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
