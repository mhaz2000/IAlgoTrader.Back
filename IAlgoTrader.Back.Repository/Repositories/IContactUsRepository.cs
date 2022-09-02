using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface IContactUsRepository : IRepository<ContactUs>
    {
        Task UpdateAsync(ContactUsCommand command);
    }
}
