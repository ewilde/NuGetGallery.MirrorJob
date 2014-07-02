using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NuGetGallery.MirrorJob.Utility
{
    public class NugetApi
    {
        public static readonly Uri NugetOrgFeed = new Uri("http://www.nuget.org/api/v2/Packages");

        public async Task<XDocument> GetPackagesFromFeed()
        {
            return await GetPackagesFromFeed(NugetOrgFeed);
        }

        public async Task<XDocument> GetPackagesFromFeed(Uri feedUri)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(feedUri).ConfigureAwait(false))
            using (var content = response.Content)
            {
                string result = await content.ReadAsStringAsync().ConfigureAwait(false);

                return XDocument.Parse(result);
            }
        }

        public async Task DownloadPackage(Uri packageUri, string downloadDirectory)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(packageUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            using (FileStream outputStream = File.Create(Path.Combine(downloadDirectory, response.Content.Headers.ContentDisposition.FileName)))
            {
                using (var httpStream = await response.Content.ReadAsStreamAsync())
                {
                    httpStream.CopyTo(outputStream);
                    outputStream.Flush();
                }
            }
        }
    }
}