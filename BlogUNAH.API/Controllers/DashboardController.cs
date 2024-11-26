using BlogUNAH.API.Constants;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Dtos.Dashboard;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogUNAH.API.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }

        [HttpGet("info")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.AUTHOR}")]
        public async Task<ActionResult<ResponseDto<DashboardDto>>> GetDashboardInfo() 
        {
            var dashboardResponse = await _dashboardService.GetDashboardAsync();

            return StatusCode(dashboardResponse.StatusCode, dashboardResponse);
        }
    }
}
