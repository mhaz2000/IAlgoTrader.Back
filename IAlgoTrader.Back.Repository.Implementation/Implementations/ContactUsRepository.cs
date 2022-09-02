using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(DataContext context) : base(context)
        {

        }

        public async Task UpdateAsync(ContactUsCommand command)
        {
            var contactUs = await Context.ContactUs.FirstOrDefaultAsync();
            if (contactUs != null)
            {
                contactUs.PhoneNumbers = command.PhoneNumbers;
                contactUs.Addresses = command.Addresses;
                contactUs.Emails = command.Emails;
            }
            else
                await Context.ContactUs.AddAsync(new ContactUs()
                {
                    Emails = command.Emails,
                    Addresses = command.Addresses,
                    PhoneNumbers = command.PhoneNumbers
                });
        }
    }
}
