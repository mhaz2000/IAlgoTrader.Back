using IAlgoTrader.Back.Infrastructure.Enums;

namespace IAlgoTrader.Back.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
            RegisteredDate = DateTime.Now;
            IsActive = true;
            IsCompleted = false;
        }
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int? OrderLength { get; set; }
        public double? OrderVolume { get; set; }
        public double? OrderDailyVolume { get; set; }
        public double? OrderAmount { get; set; }
        public double? OrderStopLimit { get; set; }
        public double? OrderStartLimit { get; set; }
        public OrderType OrderType { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
        public double? VolumePercentage { get; set; }
        public Symbol Symbol { get; set; }
        public AlgorithmType AlgorithmType { get; set; }
        public Guid CreatedById { get; set; }
    }
}
