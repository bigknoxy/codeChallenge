using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verikai.Sample.Service.Models;

namespace Verikai.Sample.Service.Services.Abstractions
{
    public interface IMappingService
    {
        IList<PersonOutput> MapPersonRecords(IList<Person> people); 
    }
}
