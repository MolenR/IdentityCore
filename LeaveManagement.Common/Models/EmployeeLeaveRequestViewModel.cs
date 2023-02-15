namespace LeaveManagement.Common.Models;

public class EmployeeLeaveRequestViewModel
{
    public EmployeeLeaveRequestViewModel(List<LeaveAllocationViewModel> leaveAlloactions, List<LeaveRequestViewModel> leaveRequests)
    {
        LeaveAlloactions = leaveAlloactions;
        LeaveRequests = leaveRequests;
    }

    public EmployeeLeaveRequestViewModel(List<LeaveAllocationViewModel> allocations, List<LeaveRequestCreateViewModel> requests)
    {
        Allocations = allocations;
        Requests = requests;
    }

    public List<LeaveAllocationViewModel> LeaveAlloactions { get; set; }
    public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    public List<LeaveAllocationViewModel> Allocations { get; }
    public List<LeaveRequestCreateViewModel> Requests { get; }
}

