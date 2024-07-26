using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.DeletePondType
{
    public class DeletePondTypeValidation:AbstractValidator<DeletePondType>
    {
        public DeletePondTypeValidation() 
        {
            
        }
    }
}
