using System.ComponentModel;

namespace IAlgoTrader.Back.Infrastructure.Enums
{
    public enum OrderType
    {
        [Description("اتوماتیک")]
        Automatic = 0,
        [Description("خرید")]
        Buy = 1,
        [Description("فروش")]
        Sell = 2,
    }
}
