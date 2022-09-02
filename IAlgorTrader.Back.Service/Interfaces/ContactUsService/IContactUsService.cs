using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.Message.DTOs.ContactUsDTOs;

namespace IAlgorTrader.Back.Service.Interfaces.ContactUsService
{
    public interface IContactUsService
    {
        Task<ContactUsDto> GetContactUs();
        Task UpdateContactUs(ContactUsCommand command);
    }
}
