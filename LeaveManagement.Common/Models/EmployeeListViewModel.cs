using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models;

public class EmployeeListViewModel
{
    public string Id { get; set; } = null!; 

    [Display(Name="First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Date Joined")]
    [DataType(DataType.Date)]
    public DateTime DateJoined { get; set; }
    
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;
}
