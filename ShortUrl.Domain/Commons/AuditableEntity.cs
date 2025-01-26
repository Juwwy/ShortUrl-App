
namespace ShortUrl.Domain.Commons
{
    public class AuditableEntity
    {
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
