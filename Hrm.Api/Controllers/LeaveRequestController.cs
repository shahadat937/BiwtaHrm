﻿using Hrm.Application;
using Hrm.Application.DTOs.LeaveRequest;
using Hrm.Application.Features.LeaveRequest.Requests.Commands;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api;

[Route(HrmRoutePrefix.LeaveRequest)]
[ApiController]
[Authorize]
public class LeaveRequestController:Controller
{
    private readonly IMediator _mediator;
    public LeaveRequestController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveRequest")]
    public async Task<ActionResult> GetLeaveRequest() {
        var command = new GetLeaveRequestRequest{};
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-OldLeaveRequest")]
    public async Task<ActionResult> GetOldLeaveRequest()
    {
        var command = new GetOldLeaveRequestRequest { };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveRequestById/{id}")]
    public async Task<ActionResult> GetLeaveRequestById(int id) {
        var command = new GetLeaveRequestByIdRequest { LeaveRequestId = id};
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveRequestByFilter")]
    public async Task<ActionResult> GetLeaveRequestByFilter([FromQuery] LeaveRequestFilterDto filters)
    {
        var command = new GetLeaveRequestByFilterRequest { filterDto = filters };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveAmount")]
    public async Task<ActionResult> GetLeaveAmount([FromQuery] LeaveAmountRequestDto requestDto)
    {
        var command = new GetLeaveAmountRequest {leaveAmountRequestDto = requestDto};
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [Route("get-LeaveStatusOption")]
    public async Task<ActionResult> GetLeaveStatusOption()
    {
        var command = new GetLeaveStatusOptionRequest { };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [Route("get-LeaveAmountForAllLeaveTypeByEmp")]
    public async Task<ActionResult> GetLeaveAmountForAllLeaveType([FromQuery] int EmpId, [FromQuery] DateTime? LeaveStartDate, [FromQuery] DateTime? LeaveEndDate)
    {
        var command = new GetAllLeaveTypeAmountByEmpIdRequest { EmpId = EmpId, LeaveStartDate = LeaveStartDate, LeaveEndDate = LeaveEndDate  };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [Route("get-LeaveFilesByLeaveRequestId/{LeaveRequestId}")]
    public async Task<ActionResult> GetLeaveFiles(int LeaveRequestId)
    {
        var command = new GetLeaveFilesByLeaveRequestIdRequest { LeaveRequestId = LeaveRequestId };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [Route("get-TakenLeaveReport")]
    public async Task<ActionResult> GetTakenLeaveReport([FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate, [FromQuery] List<int> EmpId)
    {
        var command = new GetTakenLeaveReportRequest { EmpId = EmpId, StartDate = StartDate, EndDate = EndDate };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-leaveRequest")]
    public async Task<ActionResult> SaveLeaveRequest([FromForm] CreateLeaveRequestDto leaveRequestDto, List<IFormFile>? AssociatedFiles)
    {
        var command = new CreateLeaveRequestCommand { createLeaveRequestDto = leaveRequestDto, AssociatedFiles = AssociatedFiles };
        var response = await _mediator.Send(command);
        return Ok(response);
    }



    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("delete-LeaveRequest/{LeaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> DeleteLeaveRequest(int LeaveRequestId)
    {
        var command = new DeleteLeaveRequestCommand { LeaveRequestId = LeaveRequestId };

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("delete-LeaveFile/{LeaveFileId}")]
    public async Task<ActionResult<BaseCommandResponse>> DeleteLeaveFile(int LeaveFileId)
    {
        var command = new DeleteLeaveFileByIdCommand { LeaveFileId = LeaveFileId };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("update-LeaveRequest")]
    public async Task<ActionResult<BaseCommandResponse>> UpdateLeaveRequest([FromForm] LeaveRequestDto leaveRequestDto, [FromForm] List<IFormFile>? AssociateFiles, [FromForm] List<int>? AssociateFileDeletion)
    {
        var command = new UpdateLeaveRequestByIdCommand { LeaveRequestDto = leaveRequestDto, AssociateFiles = AssociateFiles, AssociateFileDeletion = AssociateFileDeletion };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("approve-LeaveRequestByReviewer/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> approveLeaveRequestReviewer(int leaveRequestId)
    {
        var command = new ApproveLeaveRequestReviewerCommand { LeaveRequestId =  leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("deny-LeaveRequestByReviewer/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> denyLeaveRequestReviewer(int leaveRequestId)
    {
        var command = new DenyLeaveRequestReviewerCommand { LeaveRequestId= leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("approve-LeaveRequestFinal/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> approveLeaveRequestFinal(int leaveRequestId)
    {
        var command = new ApproveLeaveRequestFinalCommand { LeaveRequestId = leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("deny-LeaveRequestFinal/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> denyLeaveRequestFinal(int leaveRequestId)
    {
        var command = new DenyLeaveRequestFinalCommand { LeaveRequestId = leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

}
