using IAlgoTrader.Back.Infrastructure.Enums;

namespace IAlgoTrader.Back.Message.Commands.TradeCommands
{
    public class CreateTradeCommand
    {
        public double Volume { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public OrderType OrderType { get; set; }
        public Guid OrderId { get; set; }
    }
}
