using FluentValidation;
using FluentValidation.Results;

namespace IAlgoTrader.Back.Message
{
    public static class ValidationResultExtension
    {
        public static void RaiseExceptionIfRequired(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
                throw new ValidationException(string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
