using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.DAL.Models;
using EMS_API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        [HttpGet("GetAll", Name = "GetAllDepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var departments = _departmentService.GetAll();
            if (departments == null || !departments.Any())
                return NotFound(ApiResponse<List<DepartmentDto>>.FailResponse("No departments found."));

            return Ok(ApiResponse<List<DepartmentDto>>.SuccessResponse(departments.ToList(), "Departments retrieved successfully"));
        }


        /// <summary>
        /// Get Department By Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByID(int id)
        {
            var dept = _departmentService.GetByID(id);
            if (dept == null)
                return NotFound(ApiResponse<DepartmentDto>.FailResponse($"Department with ID {id} not found."));

            return Ok(ApiResponse<DepartmentDto>.SuccessResponse(dept, "Department retrieved successfully"));
        }

        /// <summary>
        /// Add new Department
        /// </summary>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] DepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailResponse("Invalid model state."));

            int newId;
            _departmentService.Insert(dto, out newId);

            var createdDept = _departmentService.GetByID(newId);
            return CreatedAtAction(nameof(GetByID),
                new { id = newId },
                ApiResponse<DepartmentDto>.SuccessResponse(createdDept, "Department created successfully"));
        }

        /// <summary>
        /// Update Department
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] DepartmentDto dto)
        {
            var dept = _departmentService.GetByID(id);
            if (dept == null)
                return NotFound(ApiResponse<object>.FailResponse($"Department with ID {id} not found."));

            dto.Id = dept.Id;
            _departmentService.Update(dto);

            return Ok(ApiResponse<DepartmentDto>.SuccessResponse(dto, "Department updated successfully"));
        }

        /// <summary>
        /// Delete Department
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var dept = _departmentService.GetByID(id);
            if (dept == null)
                return NotFound(ApiResponse<object>.FailResponse($"Department with ID {id} not found."));

            _departmentService.ChangeStatus(id, ""); // soft delete
            return Ok(ApiResponse<object>.SuccessResponse(null, "Department deleted successfully"));
        }



    }
}
