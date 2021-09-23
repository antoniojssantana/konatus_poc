using FluentValidation;

namespace konatus.business.Models.Validations
{
    public class MaintenanceValidation : AbstractValidator<MaintenanceModel>
    {
        public MaintenanceValidation()
        {
            RuleFor(p => p.Description)
                .MaximumLength(250)
                .WithMessage("O campo de descrição não pode ser maior que 250 caracteres.");
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("O campo de descrição é de preenchimento obrigatório.");
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("O campo UserId é de preenchimento obrigatório.");
        }
    }
}