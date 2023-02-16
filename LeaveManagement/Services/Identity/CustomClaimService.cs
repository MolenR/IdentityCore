using LeaveManagement.Data;
using LeaveManagement.Web.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LeaveManagement.Web.Services.Identity
{
    public class CustomClaimService : UserClaimsPrincipalFactory<Employee>
    {
        public CustomClaimService(
            UserManager<Employee> userManager, 
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Employee employee)
        {
            var identity = await base.GenerateClaimsAsync(employee);
            var isMinimumAge = employee.DateOfBirth.AddYears(18) >= DateTime.UtcNow;

            identity.AddClaim(new Claim(EmployeeClaims.isMinimumAge, isMinimumAge.ToString()));
            identity.AddClaim(new Claim(EmployeeClaims.FullName, $"{employee.FirstName} {employee.LastName}"));

            foreach (var role in await UserManager.GetRolesAsync(employee))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}