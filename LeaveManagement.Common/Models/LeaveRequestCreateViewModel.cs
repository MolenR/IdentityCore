using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models;

public class LeaveRequestCreateViewModel : IValidatableObject
{
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }
    
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Apply for Leave")]
    [Required]
    public int LeaveTypeId { get; set; }
    
    public SelectList? LeaveTypes { get; set; }
    
    [Display(Name = "Comments")]
    public string? RequestComment { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(StartDate > EndDate)
        {
            yield return new ValidationResult("End Date must be after Start Date", new[] { nameof(StartDate), nameof(EndDate) });
        }
    }
}
