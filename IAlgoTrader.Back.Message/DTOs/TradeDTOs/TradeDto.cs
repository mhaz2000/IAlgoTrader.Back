namespace IAlgoTrader.Back.Message.DTOs.TradeDTOs
{
    public class TradeDto
    {
        public Guid Id { get; set; }
        public double SellVolume { get; set; }
        public double BuyVolume { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string OrderType { get; set; }
        public string AlgorithmType { get; set; }
        public int OrderNumber { get; set; }
    }
}
