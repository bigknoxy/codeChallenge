using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Verikai.Sample.Service.Exceptions;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.Service.Services
{
    internal class FileService : IFileService
    {
        private CsvConfiguration _csvConfiguration;
        private readonly IEncryptionService _encryptionService;

        public FileService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true
            };
        }
        
        public IList<T> ReadFile<T>(string filePath)
        {
            try
            {
                var originalDataFromFile = new List<T>();                
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, _csvConfiguration))
                {
                    originalDataFromFile = csv.GetRecords<T>().ToList();
                }
                return originalDataFromFile;
            }
            catch(Exception e)
            {
                throw new ReadFileException("Error reading file. Make sure you have defined correct type for CSVHelper to use when reading file");
            }
            
        }

        public void WriteFile<T>(string filePath, IEnumerable<T> records, bool isTabDelimited = false)
        {
            if (isTabDelimited)
                SetTabDelimited();
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                csv.WriteRecords(records);
                
            }
        }

        public async Task WriteFileEncryptedAsync<T>(string filePath, IEnumerable<T> records, bool isTabDelimited = false, CancellationToken token = default)
        {
            if (isTabDelimited)
                SetTabDelimited();
            await using var memoryStream = new MemoryStream();
            await using var streamWriter = new StreamWriter(memoryStream);
            await using var csvWriter = new CsvWriter(streamWriter, _csvConfiguration);
            await csvWriter.WriteRecordsAsync(records, token).ConfigureAwait(false);
            var toEncrypt = memoryStream.ToArray();

            var encrypted = _encryptionService.Encrypt(toEncrypt);
            await File.WriteAllBytesAsync(filePath, encrypted, token).ConfigureAwait(false);
        }

        private void SetTabDelimited()
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t"
            };
        }
    }
}
