using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Data;

public class LeaveRequest : BaseData
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateRequested { get; set; }

    [ForeignKey("LeaveTypeId")]
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    
    public string? RequestComment { get; set; }

    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }

    public string? RequestEmployeeId { get; set; }
}
