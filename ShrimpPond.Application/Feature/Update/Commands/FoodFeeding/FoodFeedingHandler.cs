using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Domain.Food;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Feeding.Commands.Feeding
{
    public class FoodFeedingHandler : IRequestHandler<FoodFeeding, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache; // Thêm IMemoryCache

        public FoodFeedingHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
        public async Task<string> Handle(FoodFeeding request, CancellationToken cancellationToken)
        {
            //xoa cachekey
            _cache.Remove($"PondInfo_{request.pondId}");
            //validate
            var validator = new FoodFeedingValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var pond = _unitOfWork.pondRepository.FindByCondition(x => x.pondId == request.pondId && x.status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.pondId} chưa kích hoạt", validatorResult);
            }

            List<FoodForFeeding> feedingFoods = new List<FoodForFeeding>();

            if (request.foods != null)
            {
                foreach (var food in request.foods)
                {
                    var foodfeeding = new FoodForFeeding()
                    {
                        name = food.name,
                        amount = food.amount
                    };
                    feedingFoods.Add(foodfeeding);
                }
            }

            var feeding = new Domain.Food.FoodFeeding()
            {
                pondId = request.pondId,
                feedingDate = request.feedingDate,
                foods = feedingFoods
            };

            _unitOfWork.foodFeedingRepository.Add(feeding);
            await _unitOfWork.SaveChangeAsync();
            return request.pondId;
        }
    }
}
