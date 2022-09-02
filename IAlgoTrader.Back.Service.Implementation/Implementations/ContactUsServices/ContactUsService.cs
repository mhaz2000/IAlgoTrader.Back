using IAlgorTrader.Back.Service.Interfaces.ContactUsService;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.Message.DTOs.ContactUsDTOs;
using IAlgoTrader.Back.Repository;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.ContactUsServices
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactUsDto> GetContactUs()
        {
            var contactUs = await _unitOfWork.ContactUsRepository.FirstOrDefaultAsync(_ => true);
            return new ContactUsDto()
            {
                Addresses = contactUs?.Addresses ?? "",
                Emails = contactUs?.Emails ?? "",
                PhoneNumbers = contactUs?.PhoneNumbers ?? ""
            };
        }

        public async Task UpdateContactUs(ContactUsCommand command)
        {
            await _unitOfWork.ContactUsRepository.UpdateAsync(command);
            await _unitOfWork.CommitAsync();
        }
    }
}
