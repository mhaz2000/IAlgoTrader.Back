using System.ComponentModel.DataAnnotations;

namespace IAlgoTrader.Back.Domain.Entities
{
    public class ContactUs
    {
        public ContactUs()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public string PhoneNumbers { get; set; }
        public string Addresses { get; set; }
        public string Emails { get; set; }
    }
}
