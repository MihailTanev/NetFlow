namespace NetFlow.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NetFlow.Data.Models;

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Publisher)
                .WithMany(ar => ar.Posts)
                .HasForeignKey(a => a.PublisherId);

            builder.HasMany(c => c.Comments)
                .WithOne(a => a.Post)
                .HasForeignKey(a => a.PostId);
        }
    }
}
