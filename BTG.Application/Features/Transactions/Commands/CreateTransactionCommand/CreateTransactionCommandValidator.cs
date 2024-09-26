using FluentValidation;

namespace BTG.Application.Features.Transactions.Commands.CreateTransactionCommand
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(Rule => Rule.FundId).NotEmpty();

            RuleFor(Rule => Rule.FundName)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull();

            RuleFor(Rule => Rule.Amount)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que 0.");

            RuleFor(Rule => Rule.Type)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull();

            RuleFor(Rule => Rule.Date)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull();

            RuleFor(Rule => Rule.Client.Email)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} no es una dirección de correo electrónico válida.");

            RuleFor(Rule => Rule.Client.Phone)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull()
                .Matches(@"^\d{10}$").WithMessage("{PropertyName} no es un número de celular válido.");

            RuleFor(Rule => Rule.Client.Estimate)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor que 0.");

            RuleFor(Rule => Rule.Client.TypeNotification)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .NotNull();
        }
    }
}
