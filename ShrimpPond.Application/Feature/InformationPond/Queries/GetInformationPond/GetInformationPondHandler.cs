using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory; // Thêm namespace cho cache
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond
{
    public class GetInformationPondHandler : IRequestHandler<GetInformationPond, GetInformationPondDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache; // Thêm IMemoryCache

        public GetInformationPondHandler(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetInformationPondDTO> Handle(GetInformationPond request, CancellationToken cancellationToken)
        {
            // Tạo key cho cache dựa trên pondId
            string cacheKey = $"PondInfo_{request.pondId}";

            // Kiểm tra xem dữ liệu đã có trong cache chưa
            if (!_cache.TryGetValue(cacheKey, out GetInformationPondDTO data))
            {
                // Nếu không có trong cache, lấy dữ liệu từ DB
                data = await FetchPondData(request.pondId);

                // Lưu vào cache với thời gian sống (ví dụ: 10 phút)
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5) // Gia hạn nếu được truy cập
                };
                _cache.Set(cacheKey, data, cacheOptions);
            }

            return data;
        }

        private async Task<GetInformationPondDTO> FetchPondData(string pondId)
        {
            GetInformationPondDTO data = new GetInformationPondDTO();
            var pond = await _unitOfWork.pondRepository.GetByIdAsync(pondId);
            if (pond == null)
            {
                throw new BadRequestException("Not found pond");
            }
            var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.originPondId);
            var pondType = _unitOfWork.pondTypeRepository.FindByCondition(x => x.pondTypeId == pond.pondTypeId).FirstOrDefault();
            if (pondType == null)
            {
                throw new BadRequestException("Not found pondType");
            }

            // Thông tin ao cơ bản
            data.pondId = pond.pondId;
            data.pondName = pond.pondName;
            data.deep = pond.deep;
            data.diameter = pond.diameter;
            data.pondTypeId = pond.pondTypeId;
            data.pondTypeName = pondType.pondTypeName;
            data.status = (pond.status == EPondStatus.Active) ? "Đã kích hoạt" : "Chưa kích hoạt";
            data.originPondName = (originPond == null) ? "Không có ao gốc" : originPond.pondName;
            data.seedId = pond.seedId;
            data.seedName = pond.seedName;
            data.startDate = pond.startDate;

            // Thông tin về giấy chứng nhận

            //var certificates = _unitOfWork.certificateRepository
            //                    .FindByCondition(x => x.pondId == pond.pondId)
            //                    .Select(x => new { x.certificateId, x.certificateName, x.pondId })
            //                    .ToList();
            //foreach (var certificate in certificates)
            //{
            //    data.certificates.Add(new GetCertificates()
            //    {
            //        certificateId = certificate.certificateId
            //    });
            //}
            var certificates = _unitOfWork.certificateRepository.FindByCondition(x => x.pondId == pond.pondId).ToList();
            data.certificates = certificates;
                // Thông tin về cho ăn
            var foodFeedings = _unitOfWork.foodFeedingRepository.FindByCondition(f => f.pondId == pondId);
            List<GetFoodFeedingDTO> getFoodFeedingDTOs = new List<GetFoodFeedingDTO>();
            List<Foods> foods = new List<Foods>();
            foreach (var foodFeeding in foodFeedings)
            {
                var foodforFeedings = _unitOfWork.foodForFeedingRepository.FindAll()
                    .Where(f => f.foodFeedingId == foodFeeding.foodFeedingId).ToList();
                foreach (var foodforFeeding in foodforFeedings)
                {
                    var food = new Foods()
                    {
                        name = foodforFeeding.name,
                        amount = foodforFeeding.amount,
                    };
                    foods.Add(food);
                }

                var getFoodFeedingDTO = new GetFoodFeedingDTO()
                {
                    PondId = pondId,
                    FeedingDate = foodFeeding.feedingDate,
                    Foods = foods
                };
                getFoodFeedingDTOs.Add(getFoodFeedingDTO);
                foods = new();
            }
            data.feedingFoods = getFoodFeedingDTOs;

            // Thông tin về điều trị
            var medicineFeedings = _unitOfWork.medicineFeedingRepository.FindByCondition(f => f.pondId == pondId);
            List<GetMedicineFeedingDTO> getMedicineFeedingDTOs = new List<GetMedicineFeedingDTO>();
            List<Medicines> medicines = new List<Medicines>();
            foreach (var medicineFeeding in medicineFeedings)
            {
                var medicineforFeedings = _unitOfWork.medicineForFeedingRepository.FindAll()
                    .Where(f => f.medicineFeedingId == medicineFeeding.medicineFeedingId).ToList();
                foreach (var medicineforFeeding in medicineforFeedings)
                {
                    var medicine = new Medicines()
                    {
                        name = medicineforFeeding.name,
                        amount = medicineforFeeding.amount,
                    };
                    medicines.Add(medicine);
                }

                var getMedicineFeedingDTO = new GetMedicineFeedingDTO()
                {
                    pondId = pondId,
                    feedingDate = medicineFeeding.feedingDate,
                    medicines = medicines
                };
                getMedicineFeedingDTOs.Add(getMedicineFeedingDTO);
                medicines = new();
            }
            data.feedingMedicines = getMedicineFeedingDTOs;

            // Lấy thông tin kích thước tôm
            var sizeShrimps = _unitOfWork.sizeShrimpRepository.FindByCondition(x => x.pondId == pond.pondId);
            data.sizeShrimps = _mapper.Map<List<GetSizeUpdateDTO>>(sizeShrimps);

            // Lấy dữ liệu tôm hao
            var lossShrimps = _unitOfWork.lossShrimpRepository.FindByCondition(x => x.pondId == pond.pondId);
            data.lossShrimps = _mapper.Map<List<GetLossUpdateDTO>>(lossShrimps);

            // Lấy dữ liệu thu hoạch
            var harvestData = _unitOfWork.harvestRepository.FindByCondition(x => x.pondId == pond.pondId);
            data.harvests = _mapper.Map<List<HarvestDTO>>(harvestData);

            return data;
        }
    }
}