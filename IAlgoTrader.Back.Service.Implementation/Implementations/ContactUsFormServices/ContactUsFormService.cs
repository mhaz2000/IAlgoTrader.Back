using IAlgorTrader.Back.Service.Interfaces.ContactUsFormService;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.Message.DTOs.ContactUsDTOs;
using IAlgoTrader.Back.Repository;
using System.Globalization;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.ContactUsFormServices
{
    public class ContactUsFormService : IContactUsFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsFormService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(AddCotactUsFormCommand command)
        {
            await _unitOfWork.ContactUsFormRepository.CreateAsync(command);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.ContactUsFormRepository.Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ICollection<ContactUsFormDto>> GetForms(string search = "")
        {
            var pc = new PersianCalendar();
            return (await _unitOfWork.ContactUsFormRepository.GetAllAsync())
                .Where(c => c.Email.Contains(search) || c.PhoneNumber.Contains(search) || c.Title.Contains(search))
                .Select(s => new ContactUsFormDto()
                {
                    Description = s.Description,
                    Email = s.Email,
                    FullName = s.FullName,
                    Id = s.Id,
                    PhoneNumber = s.PhoneNumber,
                    Title = s.Title,
                    Date = $"{pc.GetYear(s.Date)}/{pc.GetMonth(s.Date)}/{pc.GetDayOfMonth(s.Date)}-{pc.GetHour(s.Date)}:{pc.GetHour(s.Date)}"
                }).ToList();
        }
    }
}
