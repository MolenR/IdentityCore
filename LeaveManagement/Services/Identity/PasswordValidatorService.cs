using LeaveManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace IdentityCore.Web.Services.Identity
{
    public class PasswordValidatorService : IPasswordValidator<Employee>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<Employee> manager, Employee user, string? password)
        {
            /* IMPLEMENT DEEPER SECURITY MEASURES TAKEN ON PASSWORD VALIDATION 
            ----------------------------------------------------------------*/
            var errors = new List<IdentityError>();

            if (password.Contains("Password") || password.Contains("password"))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password Error",
                    Description = "Password Requirements Failed"
                });
            }

            if (password.Contains(user.FirstName) || password.Contains(user.LastName))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password Error",
                    Description = "Password Requirements Failed"
                });
            }

            if (password.Contains(user.Email) || password.Contains(user.UserName))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password Error",
                    Description = "Password Requirements Failed"
                });
            }

            if(errors.Count > 0)
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }

    }
}