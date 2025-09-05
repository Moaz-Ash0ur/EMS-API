using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppreciationController : ControllerBase
    {

        private readonly IAppreciationService _appreciationService;

        public AppreciationController(IAppreciationService appreciationService)
        {
            _appreciationService = appreciationService;
        }


        /// <summary>
        /// Retrieves all appreciation 
        /// </summary>
        /// <returns>
        /// ApiResponse containing a list of AppreciationDto objects.
        /// </returns>
        [HttpGet("GetAll")]
        public ActionResult<ApiResponse<List<AppreciationDto>>> GetAll()
        {
            var list = _appreciationService.GetAll();
            if (list == null || !list.Any())
                return NotFound(ApiResponse<List<AppreciationDto>>.FailResponse("No appreciation letters found for this employee"));

            return Ok(ApiResponse<List<AppreciationDto>>.SuccessResponse(list.ToList()));
        }


        /// <summary>
        /// Retrieves a specific appreciation letter by its ID.
        /// </summary>
        [HttpGet("GetById/{id}")]
        public ActionResult<ApiResponse<AppreciationDto>> GetById(int id)
        {
            var letter = _appreciationService.GetByID(id);
            if (letter == null)
                return NotFound(ApiResponse<AppreciationDto>.FailResponse("Appreciation letter not found"));

            return Ok(ApiResponse<AppreciationDto>.SuccessResponse(letter, "Appreciation letter retrieved successfully"));
        }



        /// <summary>
        /// Adds a new appreciation letter for an employee.
        /// Automatically generates a unique LetterNumber for the employee.
        /// Also checks if the employee is eligible for a bonus every 3 letters.
        /// </summary>
        /// <param name="dto">The AppreciationDto containing EmployeeId and other details.</param>
        /// <returns>
        /// ApiResponse containing the generated LetterNumber and a success message.
        /// If the employee is not found, returns an error response.
        /// </returns>
        [HttpPost("add")]
        public ActionResult<ApiResponse<string>> AddAppreciation([FromBody] AppreciationDto dto)
        {
            if (dto == null)
                return BadRequest(ApiResponse<string>.FailResponse("Request body is null"));

            var (newId, message) = _appreciationService.AddAppreciationToEmployee(dto);

            if (newId == 0)
                return NotFound(ApiResponse<string>.FailResponse(message));

            return Ok(ApiResponse<int>.SuccessResponse(newId, message));
        }



        /// <summary>
        /// Retrieves all appreciation letters for a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>
        /// ApiResponse containing a list of AppreciationDto objects.
        /// </returns>
        [HttpGet("GetByEmployee/{employeeId}")]
        public ActionResult<ApiResponse<List<AppreciationDto>>> GetAppreciationsByEmployee(int employeeId)
        {
            var list = _appreciationService.GetAppreciationByEmployee(employeeId);
            if (list == null || !list.Any())
                return NotFound(ApiResponse<List<AppreciationDto>>.FailResponse("No appreciation letters found for this employee"));

            return Ok(ApiResponse<List<AppreciationDto>>.SuccessResponse(list));
        }


        /// <summary>
        /// Deletes an appreciation letter by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<string>> Delete(int id)
        {
            var result = _appreciationService.ChangeStatus(id,"");
            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("Appreciation letter not found or could not be deleted"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Appreciation letter deleted successfully"));
        }
    
    
    
    }
}
