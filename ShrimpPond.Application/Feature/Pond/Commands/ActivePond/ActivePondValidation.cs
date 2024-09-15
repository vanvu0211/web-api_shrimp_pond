using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond
{
    public class ActivePondValidation:AbstractValidator<ActivePond>
    {
        public ActivePondValidation() 
        {
            RuleFor(p => p.SeedId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.SeedName)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.PondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);

            RuleFor(p => p.SizeShrimp)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();

            RuleFor(p => p.Certificates)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
           
            RuleFor(p => p.AmountShrimp)
               .NotEmpty().WithMessage("{property} is required")
               .NotNull();
        }
    }
}
