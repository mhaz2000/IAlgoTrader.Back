namespace IAlgoTrader.Back.Message.DTOs.TransactionDTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid SymbolId { get; set; }
        public string Date { get; set; }
        public string SymbolName { get; set; }
        public int NumberTrade { get; set; }
        public double ClosePrice { get; set; }
        public double LastPrice { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public double PriceFirst { get; set; }
    }
}
