namespace Core.Domain.ValueObjects;

public record Address(
    string Street,
    string City,
    string ZipCode,
    string Country
)
{
    // EF Core requires a parameterless constructor for owned types
    private Address() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
}