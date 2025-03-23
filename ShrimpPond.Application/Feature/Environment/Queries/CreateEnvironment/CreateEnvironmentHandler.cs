using MediatR;
using Microsoft.Extensions.Hosting;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Environment.Queries.CreateEnvironment
{
    public class CreateEnvironmentHandler: IRequestHandler<CreateEnvironment,string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEnvironmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateEnvironment request, CancellationToken cancellationToken)
        {
            var data = new EnvironmentStatus()
            {
                pondId = request.PondId,
                name = request.Name,
                value = request.Value,
                timestamp = request.Timestamp,// chia thời gian cho mỗi lần đo
            };

            _unitOfWork.environmentStatusRepository.Add(data);
            await _unitOfWork.SaveChangeAsync();
            return request.PondId;
        }
    }
}
