namespace Ecommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewVoteConfiguration : IEntityTypeConfiguration<ReviewVote>
    {
        public void Configure(EntityTypeBuilder<ReviewVote> reviewVoteBuilder)
        {
            reviewVoteBuilder
                .HasKey(r => new { r.UserId, r.ReviewId });

            reviewVoteBuilder
                .HasOne(r => r.User)
                .WithMany(u => u.ReviewVotes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            reviewVoteBuilder
                .HasOne(r => r.Review)
                .WithMany(r => r.Votes)
                .HasForeignKey(r => r.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
