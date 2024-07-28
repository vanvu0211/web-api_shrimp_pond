using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate
{
    public class SizeShrimpUpdateValidation : AbstractValidator<SizeShrimpUpdate>
    {
        public SizeShrimpUpdateValidation()
        {
            RuleFor(p => p.PondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.SizeValue)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.UpdateDate)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
        }
    }
}
