using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Dtos.Dashboard;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<ResponseDto<DashboardDto>> GetDashboardAsync();
    }
}
