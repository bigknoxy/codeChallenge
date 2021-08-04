using System;
using System.Threading.Tasks;
using Verikai.Sample.Service.Exceptions;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.Service.Services
{
    internal class DownloadFileService : IDownloadFileService
    {
        public async Task DownloadFile(string url)
        {
            if (url == null || url == string.Empty)
                throw new DownloadFileException("must provide a url for file download");
            
            var webClient = new System.Net.WebClient();
            await webClient.DownloadFileTaskAsync(new Uri(url), "verikaiFile.tsv").ConfigureAwait(false);
            //webClient.DownloadFile(new Uri(url), "veriKaiFile.tsv");
        }
    }
}
