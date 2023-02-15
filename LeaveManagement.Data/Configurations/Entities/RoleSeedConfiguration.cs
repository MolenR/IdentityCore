using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeaveManagement.Common.Constants;

namespace LeaveManagement.Data.Configurations.Entities;

public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "1d3fe105-e5e9-8a1z-4e05-62a2105df6e4",
                Name = Roles.Administrator,
                NormalizedName = Roles.Administrator.ToUpper()
            },
            new IdentityRole
            {
                Id = "e5e9e105-8a1z-4e05-62a2-105df6e41d3f",
                Name = Roles.User,
                NormalizedName = Roles.User.ToUpper()
            }
        );
    }
}