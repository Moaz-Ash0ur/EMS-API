using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.DAL.Models;
using EMS_API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {

        private readonly ILeaveService _leaveService;
        private readonly IEmployeeService _empService;

        public LeaveController(ILeaveService leaveService, IEmployeeService empService)
        {
            _leaveService = leaveService;
            _empService = empService;
        }

        /// <summary>
        /// Get all leaves in the system
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var leaves = _leaveService.GetAll();
            return Ok(ApiResponse<List<LeaveDto>>.SuccessResponse(leaves.ToList(), "Leaves retrieved successfully"));
        }

        /// <summary>
        /// Get all leaves by employee id
        /// </summary>
        [HttpGet("GetLeavesByEmployee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllByEmployeeId(int employeeId)
        {
            var emp = _empService.GetByID(employeeId);
            if (emp == null)
                return NotFound(ApiResponse<string>.FailResponse("Employee not found."));

            var employeeLeaves = _leaveService.GetActiveLeavesByEmployee(employeeId);
            if (employeeLeaves == null || !employeeLeaves.Any())
                return NotFound(ApiResponse<string>.FailResponse("Employee has no active leaves."));

            return Ok(ApiResponse<List<LeaveDto>>.SuccessResponse(employeeLeaves, "Employee leaves retrieved successfully."));
        }

        /// <summary>
        /// Get leave by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var leave = _leaveService.GetByID(id);
            return leave == null
                ? NotFound(ApiResponse<string>.FailResponse($"Leave {id} not found."))
                : Ok(ApiResponse<LeaveDto>.SuccessResponse(leave, "Leave retrieved successfully."));
        }

        /// <summary>
        /// Add new leave request for employee.
        /// </summary>
        /// <remarks>
        /// 📌 API Response:       
        ///     - `Data`: The ID of the newly created leave (int > 0).
        ///     - `Message`: Success message ("Leave request added successfully.").
        ///     - `Success = true`.
        /// - **Failure**: Returns <see cref="ApiResponse{T}"/> with:
        ///     - `Data`: Always 0.
        ///     - `Message`: Error description (e.g. "Employee not found", "Invalid date range").
        ///     - `Success = false`.
        /// </remarks>
        /// <param name="dto">Leave data transfer object containing employee id, reason, start date, and end date.</param>
        /// <returns>
        /// - `201 Created` with new leave ID and success message if creation succeeds.  
        /// - `400 Bad Request` with error message if validation fails.
        /// </returns>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add(LeaveDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid model data."));

            var (id, message) = _leaveService.AddLeaveToEmployee(dto);

            if (id == 0)
                return BadRequest(ApiResponse<string>.FailResponse(message));

            return CreatedAtAction(nameof(GetById),
                new { id = id },
                ApiResponse<int>.SuccessResponse(id, message));
        }

        /// <summary>
        /// Accept a leave request (set IsActive = true)
        /// </summary>
        [HttpPut("Accept/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AcceptLeave(int id)
        {
            var updated = _leaveService.UpdateLeaveStatus(id, true);
            if (!updated)
                return NotFound(ApiResponse<string>.FailResponse($"Leave {id} not found."));

            return Ok(ApiResponse<string>.SuccessResponse("Leave accepted successfully."));
        }

        /// <summary>
        /// Reject a leave request (set IsActive = false)
        /// </summary>
        [HttpPut("Reject/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RejectLeave(int id)
        {
            var updated = _leaveService.UpdateLeaveStatus(id, false);
            if (!updated)
                return NotFound(ApiResponse<string>.FailResponse($"Leave {id} not found."));

            return Ok(ApiResponse<string>.SuccessResponse("Leave rejected successfully."));
        }

    }

}
