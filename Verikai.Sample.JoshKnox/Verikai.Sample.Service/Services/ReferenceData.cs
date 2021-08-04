using System.Collections.Generic;
using System.Linq;
using Verikai.Sample.Service.Models;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.Service.Services
{
    internal class ReferenceData : IReferenceData
    {
        private IEnumerable<Zip3ToClassRef> _zip3ToClassRefs;
        private IEnumerable<ClassToCostRef> _classToCostRefs;
        private readonly IFileService _fileService;

        public ReferenceData(IFileService fileService)
        {
            _fileService = fileService;
            _zip3ToClassRefs = _fileService.ReadFile<Zip3ToClassRef>("zip3-to-class.csv");
            _classToCostRefs = _fileService.ReadFile<ClassToCostRef>("class-to-cost.csv");
            LoadDictionary(_zip3ToClassRefs, _classToCostRefs);
        }

        private void LoadDictionary(IEnumerable<Zip3ToClassRef> zip3ToClassRefs, IEnumerable<ClassToCostRef> classToCostRefs)
        {
            Dictionary<int, decimal?> dict = new Dictionary<int, decimal?>(zip3ToClassRefs.Count());
            foreach(var zip3Ref in zip3ToClassRefs)
            {
                dict.Add(zip3Ref.Zip3, classToCostRefs?.FirstOrDefault(c => c.AreaClass?.Trim().ToLowerInvariant() == zip3Ref.AreaClass?.Trim().ToLowerInvariant())?.Cost);
            }            
            CostPerZip = dict;
        }

        public Dictionary<int, decimal?> CostPerZip { get; set; } 
    }
}
