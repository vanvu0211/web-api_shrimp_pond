using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.Application.Feature.Pond.Commands.CreatePond
{
    public class CreatePondHandler: IRequestHandler<NurseryPond.Commands.CreatePond.CreatePond, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(NurseryPond.Commands.CreatePond.CreatePond request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreatePondValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert

            var condition1 =  _unitOfWork.pondRepository.FindAll().SingleOrDefault(p=>p.PondId == request.PondId);

            if (condition1 != null)
            {
                throw new BadRequestException("PondId already exist");
            }

            var condition2 = _unitOfWork.pondTypeRepository.FindAll().SingleOrDefault(p => p.PondTypeName == request.PondTypeName);

            if (condition2 == null)
            {
                throw new BadRequestException("Not found PondType");
            }

       

            var nurseryPond = new Domain.PondData.Pond()
            {
                PondId = request.PondId,
                Deep = request.Deep,
                Diameter = request.Diameter,
                PondTypeName = request.PondTypeName,
                StartDate = DateTime.Now,
                Status = EPondStatus.InActive,
            };


            _unitOfWork.pondRepository.Add(nurseryPond);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondId;
        }

    }
}
