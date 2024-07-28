using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.Feeding
{
    public class FeedingValidation: AbstractValidator<Feeding>
    {
        public FeedingValidation() 
        {
            RuleFor(p => p.PondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);          
        }
    }
}
