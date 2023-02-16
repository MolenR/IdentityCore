using LeaveManagement.Data;
using LeaveManagement.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveManagement.Web.Views.LeaveRequests;

/*[Authorize(Policy = Policies.IsMinimumAge)]*/
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context) 
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        /*var isMinimumAge = User.Claims.FirstOrDefault(claim => claim.Type == EmployeeClaims.isMinimumAge)?.Value;

        if(isMinimumAge == null || bool.Parse(isMinimumAge) == false)
        {
            return LocalRedirect("/Identity/Account/AccessDenied");
        }*/

        return Page();
    }
}
