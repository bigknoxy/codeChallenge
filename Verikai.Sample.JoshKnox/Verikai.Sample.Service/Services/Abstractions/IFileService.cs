using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Verikai.Sample.Service.Services.Abstractions
{
    public interface IFileService
    {
        public IList<T> ReadFile<T>(string filePath);
        public void WriteFile<T>(string filePath, IEnumerable<T> records, bool isTabDelimited = false);
        public Task WriteFileEncryptedAsync<T>(string filePath, IEnumerable<T> records, bool isTabDelimited = false, CancellationToken token = default);
    }
}
