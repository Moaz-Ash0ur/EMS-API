using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("GetDashboardStats")]
        public ApiResponse<DashboardStatsDto> GetDashboardStats()
        {
            var stats =  _dashboardService.GetDashboardStats();

            if (stats == null)
                return ApiResponse<DashboardStatsDto>.FailResponse("No employees found");

            return ApiResponse<DashboardStatsDto>.SuccessResponse(stats, "Statistics Retrieved Successfully");
        }




    }
}
