using FluentValidation;
using IAlgoTrader.Back.Message.Commands.UserCommands;

namespace IAlgoTrader.Back.Message.Validators.UserValidators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Password).NotEmpty().NotNull().WithMessage("رمز عبور نمی‌تواند خالی باشد.");
            RuleFor(c => c.Username).NotEmpty().NotNull().WithMessage("نام کاربری نمی‌تواند خالی باشد.");
        }
    }
}
