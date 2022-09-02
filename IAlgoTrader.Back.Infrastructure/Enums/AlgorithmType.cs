using System.ComponentModel;

namespace IAlgoTrader.Back.Infrastructure.Enums
{
    public enum AlgorithmType
    {
        [Description("چرخه ای")]
        ITA,
        [Description("میانگین موزون زمان قیمت")]
        TWAP,
        [Description("میانگین موزون حجم قیمت")]
        VWAP,
        [Description("درصد حجمی")]
        POV
    }
}
