using IAlgoTrader.Back.Message.Validators.UserValidators;

namespace IAlgoTrader.Back.Message.Commands.UserCommands
{
    public abstract class UserCommand : ICommandBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public Guid LogoId { get; set; }

        public virtual void Validate()
        {
            new UserCommandValidator().Validate(this).RaiseExceptionIfRequired();
        }
    }

    public class CreateUserCommand : UserCommand { }

    public class UpdateUserCommand : UserCommand { }
}
