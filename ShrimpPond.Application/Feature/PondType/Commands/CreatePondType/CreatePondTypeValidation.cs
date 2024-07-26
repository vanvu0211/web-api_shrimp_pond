using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.CreatePondType
{
    public class CreatePondTypeValidation: AbstractValidator<CreatePondType>
    {
        public CreatePondTypeValidation() 
        {
            RuleFor(p => p.PondTypeName)
               .NotEmpty().WithMessage("{property} is required")
               .NotNull()
               .MaximumLength(100);
            RuleFor(p => p.PondTypeId)
             .NotEmpty().WithMessage("{property} is required")
             .NotNull()
             .MaximumLength(100);
        }
    }
}
