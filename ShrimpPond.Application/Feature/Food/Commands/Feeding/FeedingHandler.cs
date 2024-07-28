using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.Feeding
{
    public class FeedingHandler: IRequestHandler<Feeding,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeedingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(Feeding request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new FeedingValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var pond = _unitOfWork.pondRepository.FindAll().Where(x=>x.PondId==request.PondId).Where(x=>x.Status == EPondStatus.InActive);

            if(pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.PondId} chưa kích hoạt", validatorResult);
            }

            List<FeedingFood> feedingFoods = new List<FeedingFood>();

            if(request.Foods != null)
            {
                foreach(var food in request.Foods)
                {
                    var foodfeeding = new Domain.PondData.FeedingFood()
                    {
                        Name = food.Name,
                        Type = food.Type,
                        Amount = food.Amount
                    };
                    feedingFoods.Add(foodfeeding);
                }
            }

            var feeding = new Domain.PondData.Feeding()
            {
                PondId = request.PondId,
                FeedingDate = request.FeedlingDate,
                Foods = feedingFoods
            };
           
            _unitOfWork.feedingRepository.Add(feeding);
           await _unitOfWork.SaveChangeAsync();
            return request.PondId;
        }
    }
}
