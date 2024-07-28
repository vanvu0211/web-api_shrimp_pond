using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreatePondValidation :AbstractValidator<CreatePond>
    {
        public CreatePondValidation() 
        {
            RuleFor(p => p.PondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.Deep)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.Diameter)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();

        }
    }
}
