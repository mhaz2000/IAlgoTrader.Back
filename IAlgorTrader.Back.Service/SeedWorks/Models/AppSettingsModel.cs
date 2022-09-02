using IAlgoTrader.Back.Service.SeedWorks.Models;

namespace IAlgoTrader.Back.SeedWorks.Models
{
    public class AppSettingsModel
    {
        public string HostAddress { get; set; }
        public bool HostRunAsConsole { get; set; }
        public string AllowedHosts { get; set; }
        public JwtIssuerOptions JwtIssuerOptions { get; set; }
    }
}
