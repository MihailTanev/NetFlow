namespace NetFlow.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NetFlow.Data.Models;

    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(sc => sc.StudentId);

            builder.HasOne(sc => sc.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
