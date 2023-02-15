using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models;

public class LeaveAllocationViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Display(Name = "Number of Days")]
    [Required]
    [Range(1, 50, ErrorMessage = "Invalid Number Entered")]
    public int NumberOfDays { get; set; }
    
    [Required]
    [Display(Name = "Allocation Period")]
    public int Period { get; set; }
    public LeaveTypeViewModel? LeaveType { get; set; }
}