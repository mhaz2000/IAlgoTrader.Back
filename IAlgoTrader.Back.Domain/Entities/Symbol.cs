using System.ComponentModel.DataAnnotations;

namespace IAlgoTrader.Back.Domain.Entities
{
    public class Symbol
    {
        public Symbol()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
