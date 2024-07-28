using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.CreateNewFood
{
    public class CreateNewFoodHandler:IRequestHandler<CreateNewFood,string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewFoodHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<string> Handle(CreateNewFood request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateNewFoodValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var condition = _unitOfWork.foodRepository.FindAll().Where(f=>f.Name==request.Name).ToList();
            if (condition.Count() != 0)
            {
                throw new BadRequestException("Food already exist", validatorResult);
            }

            var newfood = new Domain.PondData.Feeding.Food.Food()
            {
                Name = request.Name,
                Type = request.Type,
            };

            _unitOfWork.foodRepository.Add(newfood);
            await _unitOfWork.SaveChangeAsync();
            return request.Name;
        } 
    }
}
