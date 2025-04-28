using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Features.Modules.Requests.Queries;
using Hrm.Application.Features.ShiftTypes.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Queries
{
    public class GetTreeShiftTypeRequestHandler : IRequestHandler<GetTreeShiftTypeRequest, List<TreeShiftTypeDto>>
    {
        private readonly IHrmRepository<ShiftType> _ShiftTypeRepository;
        private readonly IHrmRepository<ShiftSetting> _ShiftSettingRepository;
        private readonly IMapper _mapper;
        public GetTreeShiftTypeRequestHandler(IHrmRepository<ShiftType> shiftTypeRepository, IHrmRepository<ShiftSetting> shiftSettingRepository, IMapper mapper)
        {
            _ShiftTypeRepository = shiftTypeRepository;
            _ShiftSettingRepository = shiftSettingRepository;
            _mapper = mapper;
        }

        public async Task<List<TreeShiftTypeDto>> Handle(GetTreeShiftTypeRequest request, CancellationToken cancellationToken)
        {
            var shiftTypes = await _ShiftTypeRepository.Where(x => true)
                .Include(x => x.ShiftSetting)
                .OrderBy(x => x.MenuPosition)
                .ToListAsync(cancellationToken);

            var treeShiftType = new List<TreeShiftTypeDto>();

            foreach (var shiftTypeDto in shiftTypes)
            {
                var newTreeShiftType = new TreeShiftTypeDto
                {
                    Id = shiftTypeDto.Id,
                    ShiftName = shiftTypeDto.ShiftName,
                    IsDefault = shiftTypeDto.IsDefault,
                    IsActive = shiftTypeDto.IsActive,
                    Remark = shiftTypeDto.Remark,
                    MenuPosition = shiftTypeDto.MenuPosition
            };

                var shiftSettings = await _ShiftSettingRepository.Where(x => x.ShiftTypeId == shiftTypeDto.Id)
                    .OrderByDescending(x => x.IsActive)
                    .ToListAsync(cancellationToken);

                newTreeShiftType.ShiftSettingDto = _mapper.Map<List<ShiftSettingDto>>(shiftSettings);

                treeShiftType.Add(newTreeShiftType);
            }

            return treeShiftType;
        }
    }
}
