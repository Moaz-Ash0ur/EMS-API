using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services;
using EMS_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Tags("Employees Management")] grouping  or names for more one controller
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns>List of employees</returns>
        /// <response code="404">No Employee In Database to return</response>
        /// <response code="200">Employees returned successfully</response>
        [HttpGet("All", Name = "GetAllEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAllCached();
            if (employees == null || !employees.Any())
                return NotFound(ApiResponse<List<EmployeeDto>>.FailResponse("No employees found"));

            return Ok(ApiResponse<List<EmployeeDto>>.SuccessResponse(employees.ToList(), "Employees retrieved successfully"));
        }

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns>List of employees with pagination</returns>
        /// <response code="404">No Employee In Database to return</response>
        /// <response code="200">Employees returned successfully</response>
        [HttpGet("AllPaginted/{PageNumber}/{PageSize}", Name = "GetPagintedEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllPaginted(int PageNumber,int PageSize)
        {
            var employeesPagedResult = _employeeService.GetAllPaginted(PageNumber,PageSize);
            if (employeesPagedResult == null || !employeesPagedResult.Items.Any())
                return NotFound(ApiResponse<List<EmployeeDto>>.FailResponse("No employees found"));

            return Ok(ApiResponse<List<EmployeeDto>>.SuccessResponse(employeesPagedResult.Items.ToList(), "Employees retrieved successfully"));
        }


        /// <summary>
        /// Get filtered employees with pagination
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <param name="pageNumber">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <returns>Filtered employees list</returns>
        [HttpGet("filter")]
        public  IActionResult GetEmployeesFiltered([FromQuery] EmployeeFilterDto filter,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var employeesPagedResult = _employeeService.GetFilteredEmployees(filter, pageNumber, pageSize);
            if (employeesPagedResult == null || !employeesPagedResult.Items.Any())
                return NotFound(ApiResponse<List<EmployeeDto>>.FailResponse("No employees found"));

            return Ok(ApiResponse<List<EmployeeDto>>.SuccessResponse(employeesPagedResult.Items.ToList(), "Filtered employees retrieved successfully"));
        }

        /// <summary>
        /// Get Employee By Id
        /// </summary>
        [HttpGet("{id}", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var emp = _employeeService.GetByID(id);
            if (emp == null)
                return NotFound(ApiResponse<EmployeeDto>.FailResponse($"Employee with ID {id} not found."));

            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(emp, "Employee retrieved successfully"));
        }

        /// <summary>
        /// Add New Employee and return created employee
        /// </summary>
        /// <returns>Employee you added</returns>
        /// <response code="201">Employee created successfully</response>
        /// <response code="400">Validation error</response>
        [HttpPost("Add", Name = "AddEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailResponse("Invalid model state"));

            int newId;
            _employeeService.Insert(dto, out newId);

            var createdEmp = _employeeService.GetByID(newId);
            return CreatedAtRoute("GetEmployeeById",new { id = newId },
                ApiResponse<EmployeeDto>.SuccessResponse(createdEmp, "Employee added successfully"));
        }       

        /// <summary>
        /// Update an existing Employee
        /// </summary>
        [HttpPut("{id}", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] EmployeeDto emp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailResponse("Invalid model state"));

            var oldEmp = _employeeService.GetByID(id);
            if (oldEmp == null)
                return NotFound(ApiResponse<object>.FailResponse($"Employee with ID {id} not found."));

            _employeeService.Update(emp);

            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(emp, "Employee updated successfully"));
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetByID(id);
            if (employee == null)
                return NotFound(ApiResponse<object>.FailResponse($"Employee with ID {id} not found."));

            _employeeService.ChangeStatus(id, "");
            return Ok(ApiResponse<object>.SuccessResponse(null, "Employee deleted successfully"));
        }






    }
}
