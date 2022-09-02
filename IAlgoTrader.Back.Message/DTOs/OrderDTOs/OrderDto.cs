using IAlgoTrader.Back.Infrastructure.Enums;

namespace IAlgoTrader.Back.Message.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string OrderType { get; set; }
        public string AlgorithmType { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
        public string SymbolName { get; set; }
        public int OrderNumber { get; set; }
    }
}
