using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verikai.Sample.Service.Models;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.Service.Services
{
    internal class MappingService : IMappingService
    {
        private readonly IReferenceData _referenceData;

        public MappingService(IReferenceData referenceData)
        {
            _referenceData = referenceData;
        }

        public IList<PersonOutput> MapPersonRecords(IList<Person> people)
        {
            return
            people.Select(x => new PersonOutput
            {
                FirstName = x.FirstName.Trim(),
                LastName = x.LastName.Trim(),
                Gender = GetGender(x.Gender.Trim()),
                DateOfBirth = GetDateOfBirth(x.DateOfBirth.Trim()),
                State = x.State.Trim(),
                Phone = FormattedPhoneNumber(x.Phone.Trim()),
                ZipCode = x.ZipCode.Trim(),
                Cost = GetCost(x.ZipCode.Substring(0, 3)),
                Age = CalculateAge(GetDateOfBirth(x.DateOfBirth.Trim()))
            }).ToList();
        }

        private int? CalculateAge(string dateOfBirth)
        {
            DateTime bday = new DateTime();
            if (!IsDateTime(dateOfBirth))
                return null;
            bday = DateTime.Parse(dateOfBirth);

            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;    //people perceive their age in years

            if (today.Month < bday.Month ||
               ((today.Month == bday.Month) && (today.Day < bday.Day)))
            {
                age--;  //birthday in current year not yet reached, we are 1 year younger 
                //probably some leap year stuff that would need unit tests to fix here
            }
            return age;
        }

        private decimal? GetCost(string zip3string)
        {
            int? zip3;
            if (IsValidInt(zip3string))
            {
                zip3 = int.Parse(zip3string);
            }
            else
            { 
                return null; 
            }
                
            decimal? cost;
            _referenceData.CostPerZip.TryGetValue(zip3.Value, out cost);
            return cost;            
        }

        private string FormattedPhoneNumber(string phone)
        {
            return ExtractNumber(phone);
        }
        public string ExtractNumber(string original) => new string(original.Where(char.IsDigit).ToArray());

        private string? GetDateOfBirth(string dateOfBirth)
        {
            if (IsDateTime(dateOfBirth))
                return DateTime.Parse(dateOfBirth).ToShortDateString();
            return null;
        }
        private bool IsValidInt(string text)
        {
            int deci;
            return int.TryParse(text, out deci);
        }
        private bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate);
        }

        private string GetGender(string gender)
        {
            if (gender.StartsWith("M", StringComparison.OrdinalIgnoreCase) || gender.StartsWith("F", StringComparison.OrdinalIgnoreCase))
                return (gender.StartsWith("m", StringComparison.OrdinalIgnoreCase)) ? "M" : "F";
            return "U"; //making an assumption to return U for "unknown"
        }
    }
}
