using IAlgoTrader.Back.Message.Validators.ContactUsValidators;

namespace IAlgoTrader.Back.Message.Commands.ContactUsCommands
{
    public class AddCotactUsFormCommand : ICommandBase
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public void Validate() => new AddCotactUsFormCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}
