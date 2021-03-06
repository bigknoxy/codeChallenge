using CsvHelper.Configuration.Attributes;

namespace Verikai.Sample.Service.Models
{
    public record PersonOutput
    {    
        [Name("first_name")] public string FirstName { get; init; }
        [Name("last_name")] public string LastName { get; init; }
        [Name("gender")] public string Gender { get; init; }
        [Name("dob")] public string DateOfBirth { get; init; }
        [Name("state")] public string State { get; init; }
        [Name("phone")] public string Phone { get; init; }
        [Name("zip")] public string ZipCode { get; init; }
        [Name("age")] public int? Age { get; init; }
        [Name("cost")] public decimal? Cost { get; init; }
    }    
}
