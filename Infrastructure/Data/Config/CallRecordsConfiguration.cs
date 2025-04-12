using Domain.CallRecordAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CallRecordsConfiguration : IEntityTypeConfiguration<CallRecord>
{
    public void Configure(EntityTypeBuilder<CallRecord> builder)
    {
        builder.ToTable("CallRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.CallerId).IsUnicode(false);
        builder.Property(x => x.Recipient).IsUnicode(false);
        builder.Property(x => x.CallDate);
        builder.Property(x => x.EndTime);
        builder.Property(x => x.Duration);
        builder.Property(x => x.Cost).HasColumnType($"decimal(18,{DataSchemaConstants.COST_MAX_DECIMAL_PLACES})");
        builder.Property(x => x.Reference).IsUnicode(false);
        builder.Property(x => x.Currency)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<Currency>(x));
    }
}