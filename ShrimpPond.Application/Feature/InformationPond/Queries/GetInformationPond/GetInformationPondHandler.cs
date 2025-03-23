using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond
{
    public class GetInformationPondHandler: IRequestHandler<GetInformationPond,GetInformationPondDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInformationPondHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetInformationPondDTO> Handle(GetInformationPond request, CancellationToken cancellationToken)
        {
            GetInformationPondDTO data = new GetInformationPondDTO();
            var pond = await _unitOfWork.pondRepository.GetByIdAsync(request.pondId);
            if(pond == null)
            {
                throw new BadRequestException("Not found pond");
            }
            var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.originPondId);

            var pondType = _unitOfWork.pondTypeRepository.FindByCondition(x => x.pondTypeId == pond.pondTypeId).FirstOrDefault();
            if (pondType == null)
            {
                throw new BadRequestException("Not found pondType");
            }
            //Thong tin ao co ban
            data.pondId = pond.pondId;
            data.pondName = pond.pondName;
            data.deep = pond.deep;
            data.diameter = pond.diameter;
            data.pondTypeId = pond.pondTypeId;
            data.pondTypeName = pondType.pondTypeName;
            data.status = (pond.status == Domain.PondData.EPondStatus.Active) ? "Đã kích hoạt" : "Chưa kích hoạt";
            data.originPondName = (originPond == null) ? "Không có ao gốc" : originPond.pondName;
            data.seedId = pond.seedId;
            data.seedName = pond.seedName;
            data.startDate = pond.startDate;

            //Thong tin ve giay chung nhan
            List<Certificate>? certificateData = new();
            var certificates = _unitOfWork.certificateRepository.FindByCondition(x => x.pondId == pond.pondId).ToList();
            if (certificates == null)
            {
                throw new BadRequestException("Not found certificates");
            }
            certificateData.AddRange(certificates);
            data.certificates = certificateData;
            // Thong tin ve cho an
            var foodFeedings = _unitOfWork.foodFeedingRepository.FindByCondition(f => f.pondId == request.pondId);
            List<GetFoodFeedingDTO> getFoodFeedingDTOs = new List<GetFoodFeedingDTO>();
            List<Foods> foods = new List<Foods>();
            foreach (var foodFeeding in foodFeedings)
            {
                var foodforFeedings = _unitOfWork.foodForFeedingRepository.FindAll().Where(f => f.foodFeedingId == foodFeeding.foodFeedingId).ToList();
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
                    PondId = request.pondId,
                    FeedingDate = foodFeeding.feedingDate,
                    Foods = foods
                };
                getFoodFeedingDTOs.Add(getFoodFeedingDTO);
                foods = new();
            }
            data.feedingFoods = getFoodFeedingDTOs;

            // Thong tin ve dieu tri
            var medicineFeedings = _unitOfWork.medicineFeedingRepository.FindByCondition(f => f.pondId == request.pondId);
            List<GetMedicineFeedingDTO> getMedicineFeedingDTOs = new List<GetMedicineFeedingDTO>();
            List<Medicines> medicines = new List<Medicines>();
            foreach (var medicineFeeding in medicineFeedings)
            {
                var medicineforFeedings = _unitOfWork.medicineForFeedingRepository.FindAll().Where(f => f.medicineFeedingId == medicineFeeding.medicineFeedingId).ToList();
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
                    pondId = request.pondId,
                    feedingDate = medicineFeeding.feedingDate,
                    medicines = medicines
                };
                getMedicineFeedingDTOs.Add(getMedicineFeedingDTO);
                medicines = new();
            }
            data.feedingMedicines = getMedicineFeedingDTOs;

            //Lay thong tin size toom
            var sizeShrimps = _unitOfWork.sizeShrimpRepository.FindByCondition(x=>x.pondId == pond.pondId);
            data.sizeShrimps = _mapper.Map<List<GetSizeUpdateDTO>>(sizeShrimps);

            //Lay du lieu tom hao
            var lossShrimps = _unitOfWork.lossShrimpRepository.FindByCondition(x => x.pondId == pond.pondId);
            data.lossShrimps = _mapper.Map<List<GetLossUpdateDTO>>(lossShrimps);
            //Lay du lieu thu hoach
            var harvestData = _unitOfWork.harvestRepository.FindByCondition(x => x.pondId == pond.pondId);
            data.harvests = _mapper.Map<List<HarvestDTO>>(harvestData);

            return data;
            
        }
    } 
}
