using FluentValidation;

namespace konatus.business.Models.Validations
{
    public class StageValidation : AbstractValidator<StageModel>
    {
        public StageValidation()
        {
            RuleFor(p => p.Description)
               .MaximumLength(250)
               .WithMessage("O campo de descrição não pode ser maior que 250 caracteres.");
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("O campo de descrição é de preenchimento obrigatório.");
            RuleFor(p => p.MaintenanceId)
                .NotEmpty()
                .WithMessage("O campo UserId é de preenchimento obrigatório.");
            RuleFor(p => p.StatusStage)
                .NotEmpty()
                .WithMessage("O campo Status é de preenchimento obrigatório.");
            RuleFor(p => p.Value)
                .MaximumLength(100000)
                .WithMessage("O campo de Value não pode ser maior que 100000 caracteres.");
            RuleFor(p => p.Value)
                .NotEmpty()
                .WithMessage("O campo de Value é de preenchimento obrigatório.");
        }
    }
}