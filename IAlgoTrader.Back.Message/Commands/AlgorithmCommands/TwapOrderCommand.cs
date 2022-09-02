using IAlgoTrader.Back.Infrastructure.Enums;

namespace IAlgoTrader.Back.Message.Commands.AlgorithmCommands
{
    public class TwapOrderCommand
    {
        public Guid SymbolId { get; set; }
        public int SharesNumber { get; set; }
        public double Price { get; set; }
        public int TimePeriod { get; set; }
        public OrderType OrderType { get; set; }
    }
}
