using IAlgoTrader.Back.Infrastructure.Enums;

namespace IAlgoTrader.Back.Domain.Entities
{
    public class Trade
    {
        public Trade()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public double SellVolume { get; set; }
        public double BuyVolume { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public Guid OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public int OrderNumber { get; set; }
        public Guid CreatedById { get; set; }

    }
}
