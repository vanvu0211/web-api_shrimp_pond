using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.HarvestPond
{
    public class HarvestPondValidation : AbstractValidator<HarvestPond>
    {
        public HarvestPondValidation()
        {
            RuleFor(p => p.PondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.Size)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.HarvestDate)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();

        }
    }
}
