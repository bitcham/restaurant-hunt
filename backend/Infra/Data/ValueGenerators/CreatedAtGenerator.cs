using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infra.Data.ValueGenerators;

public class CreatedAtGenerator : ValueGenerator<DateTimeOffset>
{
    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }

    public override bool GeneratesTemporaryValues => false;
}