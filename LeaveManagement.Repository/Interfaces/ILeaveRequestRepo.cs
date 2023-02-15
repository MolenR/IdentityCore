using LeaveManagement.Common.Models;
using LeaveManagement.Data;

namespace LeaveManagement.Repository.Interfaces;

public interface ILeaveRequestRepo : IGenericRepo<LeaveRequest>
{
    Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel model);
    
    Task<EmployeeLeaveRequestViewModel> GetLeaveDetails();

    Task<LeaveRequestViewModel?> GetLeaveRequestAsync(int id);

    Task<List<LeaveRequest>> GetAllAsync(string employeeId);

    Task ChangeApprovalStatus(int leaveRequestId, bool approved);

    Task CancelLeaveRequest(int? leaveRequestId);

    Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList();
}
