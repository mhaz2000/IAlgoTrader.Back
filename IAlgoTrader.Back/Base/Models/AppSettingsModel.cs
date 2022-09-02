using IAlgoTrader.Back.Service.SeedWorks.Models;

namespace IAlgoTrader.Back.Base.Models
{
    public class AppSettingsModel
    {
        public bool HostRunAsConsole { get; set; }
        public string HostAddress { get; set; }
        public JwtIssuerOptions JwtIssuerOptions { get; set; }
    }
}
