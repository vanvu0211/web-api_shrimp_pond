using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public CreateFarmHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager;
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

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }
            var farm = new Domain.Farm.Farm()
            {
                FarmName = request.FarmName,
                Address = request.Address,
                UserName = user.UserName
            };


            _unitOfWork.farmRepository.Add(farm);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.FarmName;
        }
    }
}
