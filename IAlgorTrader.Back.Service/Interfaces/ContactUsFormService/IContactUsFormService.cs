using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.Message.DTOs.ContactUsDTOs;

namespace IAlgorTrader.Back.Service.Interfaces.ContactUsFormService
{
    public interface IContactUsFormService
    {
        Task Create(AddCotactUsFormCommand command);

        Task Delete(Guid id);

        Task<ICollection<ContactUsFormDto>> GetForms(string search = "");
    }
}
