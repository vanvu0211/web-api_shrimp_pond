using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.CreateFarm
{
    public class CreateFarmHandler: IRequestHandler<CreateFarm,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateFarmHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateFarm request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateFarmValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert
 

            var farm = new Domain.Farm.Farm()
            {
                FarmName = request.FarmName,
                Address = request.Address,
            };


            _unitOfWork.farmRepository.Add(farm);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.FarmName;
        }
    }
}
