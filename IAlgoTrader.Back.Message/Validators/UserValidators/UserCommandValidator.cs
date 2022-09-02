using FluentValidation;
using IAlgoTrader.Back.Message.Commands.UserCommands;

namespace IAlgoTrader.Back.Message.Validators.UserValidators
{
    public class UserCommandValidator : AbstractValidator<UserCommand>
    {
        public UserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("ایمیل نمی‌تواند خالی باشد.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("شماره تماس نمی‌تواند خالی باشد.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("نام نمی‌تواند خالی باشد.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("نام خانوادگی نمی‌تواند خالی باشد.");
        }
    }
}
