using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Features.RoleFeatures.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Handlers.Commands
{
    public class CreateRoleFeaturesCommandHandler : IRequestHandler<CreateRoleFeaturesCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<RoleFeature> _RoleFeaturesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleFeaturesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<RoleFeature> RoleFeaturesRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RoleFeaturesRepository = RoleFeaturesRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateRoleFeaturesCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            foreach (var item in request.RoleFeatureDtos)
            {
                if (item.RoleFeatureId == 0)
                {
                    //var RoleFeaturesInfo = _mapper.Map<RoleFeature>(item);
                    RoleFeature RoleFeaturesInfo = new RoleFeature
                    {
                        RoleFeatureId = item.RoleFeatureId,
                        RoleId = item.RoleId,
                        FeatureKey = item.FeatureKey,
                        ViewStatus = item.ViewStatus,
                        Add = item.Add,
                        Update = item.Update,
                        Delete = item.Delete,
                        Report = item.Report,
                        RoleName = item.RoleName,
                        FeatureName = item.FeatureName,
                        FeaturePath = item.FeaturePath
                    };

                    await _unitOfWork.Repository<RoleFeature>().Add(RoleFeaturesInfo);
                }
                else
                {
                    var RoleFeaturesInfo = await _unitOfWork.Repository<RoleFeature>().Get(item.RoleFeatureId);

                    
                    RoleFeaturesInfo.RoleFeatureId = item.RoleFeatureId;
                    RoleFeaturesInfo.RoleId = item.RoleId;
                    RoleFeaturesInfo.FeatureKey = item.FeatureKey;
                    RoleFeaturesInfo.ViewStatus = item.ViewStatus;
                    RoleFeaturesInfo.Add = item.Add;
                    RoleFeaturesInfo.Update = item.Update;
                    RoleFeaturesInfo.Delete = item.Delete;
                    RoleFeaturesInfo.Report = item.Report;
                    RoleFeaturesInfo.RoleName = item.RoleName;
                    RoleFeaturesInfo.FeatureName = item.FeatureName;
                    RoleFeaturesInfo.FeaturePath = item.FeaturePath;

                    await _unitOfWork.Repository<RoleFeature>().Update(RoleFeaturesInfo);
                }
            }

            response.Success = true;
            response.Message = "Update Successful";
            
            await _unitOfWork.Save();


            return response;
        }
    }
}