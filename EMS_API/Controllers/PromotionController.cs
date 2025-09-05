using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services;
using EMS.DAL.Models;
using EMS_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace EMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    { 
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService, IEmployeeService employeeService)
        {
            _promotionService = promotionService;
        }


        /// <summary>
        /// Get all promotions.
        /// </summary>
        /// <returns>List of promotions</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _promotionService.GetAll();
            return Ok(ApiResponse<IEnumerable<PromotionDto>>.SuccessResponse(result, "Promotions retrieved successfully"));
        }

        /// <summary>
        /// Get all promotions for a specific employee.
        /// </summary>
        /// <param name="empId">Employee ID</param>
        /// <returns>List of employee promotions</returns>
        [HttpGet("GetByEmployeeId/{empId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByEmployeeId(int empId)
        {
            var empPromotions = _promotionService.GetAllEmployeeId(empId);
            if (empPromotions == null || !empPromotions.Any())
                return NotFound(ApiResponse<string>.FailResponse($"Employee with id {empId} does not have any promotions"));

            return Ok(ApiResponse<List<PromotionDto>>.SuccessResponse(empPromotions.ToList() , "Promotions retrieved successfully"));
        }

        /// <summary>
        /// Get a promotion by promotion ID.
        /// </summary>
        /// <param name="id">Promotion ID</param>
        /// <returns>Promotion details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByID(int id)
        {
            var promo = _promotionService.GetByID(id);
            return promo == null
                ? NotFound(ApiResponse<string>.FailResponse($"Promotion {id} not found"))
                : Ok(ApiResponse<PromotionDto>.SuccessResponse(promo, "Promotion retrieved successfully"));
        }

        /// <summary>
        /// Add a new promotion for an employee.
        /// </summary>
        /// <param name="dto">Promotion details</param>
        /// <returns>Created promotion with new ID</returns>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add(PromotionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid model data"));

            var (id, message) = _promotionService.AddPromotionToEmployee(dto);

            if (id == 0)
                return BadRequest(ApiResponse<string>.FailResponse(message));

            return CreatedAtAction(nameof(GetByID), new { id = id },
                ApiResponse<int>.SuccessResponse(id, message));
        }



        /// <summary>
        /// Delete a promotion by ID.
        /// </summary>
        /// <param name="id">Promotion ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var promo = _promotionService.GetByID(id);
            if (promo == null)
                return NotFound(ApiResponse<string>.FailResponse($"Promotion {id} not found"));

            _promotionService.ChangeStatus(id,"");
            return Ok(ApiResponse<string>.SuccessResponse(null, "Promotion deleted successfully"));
        }




    }

}
