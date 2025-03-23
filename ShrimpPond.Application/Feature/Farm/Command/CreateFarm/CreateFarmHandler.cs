using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;

namespace ShrimpPond.Application.Feature.Farm.Command.CreateFarm
{
    public class CreateFarmHandler : IRequestHandler<CreateFarm, string>
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
            var farm = _unitOfWork.farmRepository.FindByCondition(x => x.farmName == request.FarmName && x.userName == request.UserName).FirstOrDefault();
            if(farm != null)
            {
                throw new BadRequestException("Farm is already exit");
            }
            var farmData = new Domain.Farm.Farm()
            {
                farmName = request.FarmName,
                address = request.Address,
                userName = user.UserName
            };


            _unitOfWork.farmRepository.Add(farmData);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.FarmName;
        }
    }
}
