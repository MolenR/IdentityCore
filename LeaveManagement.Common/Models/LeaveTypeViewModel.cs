using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models;

public class LeaveTypeViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Numbers of Days")]
    [Required]
    [Range(1, 25)]
    public int DefaultDays { get; set; }
}
