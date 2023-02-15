using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Common.Constants;


namespace LeaveManagement.Data.Configurations.Entities;

public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "1d3fe105-e5e9-8a1z-4e05-62a2105df6e4",
                UserId = "62b3adbb-e3a7-4f5f-8a05-61d2074df6c2"
            },
            new IdentityUserRole<string>
            {
                RoleId = "e5e9e105-8a1z-4e05-62a2-105df6e41d3f",
                UserId = "f6c2adbb-4f5f-8a05-e3a7-62b3adbb61d2"
            }
        );
    }
}