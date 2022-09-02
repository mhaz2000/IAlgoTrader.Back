using System.ComponentModel.DataAnnotations;


namespace IAlgoTrader.Back.Domain.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public Symbol Symbol { get; set; }
        public Guid SymbolId { get; set; }
        public DateTime Date { get; set; }
        public int NumberTrade { get; set; }
        public double ClosePrice { get; set; }
        public double LastPrice { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public double PriceFirst { get; set; }
        public DateTime CreatedAt { get; set; }  
    }
}
