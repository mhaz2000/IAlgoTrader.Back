using IAlgoTrader.Back.Message.Validators.UserValidators;

namespace IAlgoTrader.Back.Message.Commands.UserCommands
{
    public class LoginCommand : ICommandBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public void Validate() => new LoginCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}
