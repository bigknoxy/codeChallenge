using CsvHelper.Configuration.Attributes;

namespace Verikai.Sample.Service.Models
{
    public record Zip3ToClassRef
    {
        [Name("Zip")]public int Zip3 { get; init; }
        [Name("Area Class")] public string AreaClass { get; set; }
    }
}
