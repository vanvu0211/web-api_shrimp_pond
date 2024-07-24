using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreateNurseryPondValidation :AbstractValidator<CreateNurseryPond>
    {
        public CreateNurseryPondValidation() 
        {
            RuleFor(p => p.PondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);

            RuleFor(p => p.PondHeight)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.PondHeight)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);

        }
    }
}
