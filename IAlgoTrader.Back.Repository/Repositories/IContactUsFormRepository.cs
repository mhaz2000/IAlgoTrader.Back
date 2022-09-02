using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface IContactUsFormRepository : IRepository<ContactUsForm>
    {
        Task CreateAsync(AddCotactUsFormCommand command);
        Task Delete(Guid id);
    }
}
