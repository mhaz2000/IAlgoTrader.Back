namespace IAlgoTrader.Back.Message.Commands.TransactionCommands
{
    public class CreateTransactionCommand
    {
        public Guid SymbolId { get; set; }
        public DateTime Date { get; set; }
        public int NumberTrade { get; set; }
        public double ClosePrice { get; set; }
        public double LastPrice { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public double PriceFirst { get; set; }
    }
}
