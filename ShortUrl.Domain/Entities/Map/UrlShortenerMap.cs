
namespace ShortUrl.Domain.Entities.Map
{
    public class UrlShortenerMap : IEntityTypeConfiguration<UrlShortener>
    {
        public void Configure(EntityTypeBuilder<UrlShortener> builder)
        {
            builder.ToTable("tbl_UrlShortener");
            builder.HasKey(k => k.UrlShortenerId);

            #region Properties
            builder.HasIndex(p => p.ShortUrl)
                .IsUnique();

            builder.Property(u => u.ShortUrl)
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(u => u.LongUrl)
                .IsRequired();

            builder.Property(u => u.RequestCount)
                .HasDefaultValue(0)
                .IsRequired();
            #endregion

            #region Relationship
            #endregion
        }
    }
}
