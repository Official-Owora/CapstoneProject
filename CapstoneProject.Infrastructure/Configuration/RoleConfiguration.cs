﻿using CapstoneProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapstoneProject.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = UserType.Mentee.ToString(),
                    NormalizedName = "MENTEE",
                },
                new IdentityRole
                {
                    Name = UserType.Mentor.ToString(),
                    NormalizedName = "MENTOR",
                });
        }
    }
}
