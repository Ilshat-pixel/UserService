using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain;

namespace UserService.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id).HasName("id");
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.FirstName).HasColumnName("first_name").HasMaxLength(100);
            builder
                .Property(user => user.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(100);
            builder.Property(user => user.LastName).HasColumnName("last_name").HasMaxLength(100);
            builder.Property(user => user.BirthDay).HasColumnName("birth_day");
            builder.Property(user => user.PassportId).HasColumnName("passport_id").HasMaxLength(11);
            builder.Property(user => user.BirthPlace).HasColumnName("birth_place");
            builder.Property(user => user.Phone).HasColumnName("phone").HasMaxLength(11);
            builder.Property(user => user.Email).HasColumnName("email");
            builder
                .Property(user => user.RegistrationAddress)
                .HasColumnName("registration_address");
            builder.Property(user => user.LivingAddress).HasColumnName("living_address");
        }
    }
}
