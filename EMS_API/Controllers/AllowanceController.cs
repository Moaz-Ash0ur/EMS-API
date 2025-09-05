using EMS.BLL;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services;
using EMS.DAL.Data;
using EMS.DAL.Models;
using EMS_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowanceController : ControllerBase
    {

        private readonly IAllowanceService _allowanceService;

        public AllowanceController(IAllowanceService allowanceService)
        {
            _allowanceService = allowanceService;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var allowances = _allowanceService.GetAll();

            if (allowances == null || !allowances.Any())
                return NotFound(ApiResponse<string>.FailResponse("No allowances found."));

            return Ok(ApiResponse<IEnumerable<AllowanceDto>>.SuccessResponse(allowances, "Allowances retrieved successfully."));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int Id)
        {
            var item = _allowanceService.GetByID(Id);

            if (item == null)
                return NotFound(ApiResponse<string>.FailResponse($"Allowance with ID {Id} not found."));

            return Ok(ApiResponse<AllowanceDto>.SuccessResponse(item, "Allowance retrieved successfully."));
        }



        /// <summary>
        /// Add new allowance to an employee with business validation.
        /// </summary>
        /// <param name="dto">Allowance data (EmployeeId, Amount, Date, etc.)</param>
        /// <returns>
        /// Returns an <see cref="ApiResponse{T}"/>:
        /// - If the operation fails (e.g., invalid employee, invalid amount, exceeds salary rule, or max yearly limit reached),
        ///   the service returns <c>id = 0</c> with a failure message inside <see cref="ApiResponse{T}.Errors"/>.
        /// - If the operation succeeds, the service returns the new Allowance <c>id &gt; 0</c>
        ///   along with a success message inside the response body.
        /// </returns>
        /// <response code="201">Allowance added successfully (id > 0).</response>
        /// <response code="400">Failed to add allowance (id = 0).</response>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add(AllowanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid model data."));

            var (id, message) = _allowanceService.AddAllowanceToEmployee(dto);

            if (id == 0) 
                return BadRequest(ApiResponse<string>.FailResponse(message));


            return CreatedAtAction(nameof(GetById),new { Id = id },
                ApiResponse<int>.SuccessResponse(id, message));
        }


        [HttpGet("GetByEmployeeId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByEmployeeId(int id)
        {
            var item = _allowanceService.GetByEmployeeId(id);

            if (item == null)
                return NotFound(ApiResponse<string>.FailResponse($"Allowance with ID {id} not found."));

            return Ok(ApiResponse<List<AllowanceDto>>.SuccessResponse(item, "Allowance retrieved successfully."));
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var item = _allowanceService.GetByID(id);
            if (item == null)
                return NotFound(ApiResponse<string>.FailResponse($"Allowance with ID {id} not found."));

            _allowanceService.ChangeStatus(id,""); 

            return Ok(ApiResponse<string>.SuccessResponse("Allowance deactivated successfully."));
        }




    }

}
