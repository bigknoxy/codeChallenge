using CsvHelper.Configuration.Attributes;

namespace Verikai.Sample.Service.Models
{
    public record ClassToCostRef
    {
        [Name("Area Class")] public string AreaClass { get; init; }
        [Name("Cost")] public decimal Cost { get; init; }
    }
}
