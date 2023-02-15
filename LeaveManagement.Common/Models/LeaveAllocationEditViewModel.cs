namespace LeaveManagement.Common.Models;

public class LeaveAllocationEditViewModel : LeaveAllocationViewModel
{
    public string EmployeeId { get; set; }
    public EmployeeListViewModel? Employee { get; set; } 
    public int LeaveTypeId { get; set; }
}
