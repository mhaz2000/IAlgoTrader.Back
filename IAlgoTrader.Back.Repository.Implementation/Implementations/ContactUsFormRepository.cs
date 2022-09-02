using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.Repository.Repositories;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class ContactUsFormRepository : Repository<ContactUsForm>, IContactUsFormRepository
    {
        public ContactUsFormRepository(DataContext context) : base(context)
        {

        }
        public async Task CreateAsync(AddCotactUsFormCommand command)
        {
            await Context.ContactUsForms.AddAsync(new ContactUsForm()
            {
                Description = command.Description,
                Email = command.Email,
                FullName = command.FullName,
                PhoneNumber = command.PhoneNumber,
                Title = command.Title,
                Date = DateTime.Now
            });
        }

        public async Task Delete(Guid id)
        {
            Context.ContactUsForms.Remove(await Context.ContactUsForms.FindAsync(id));
        }
    }
}
