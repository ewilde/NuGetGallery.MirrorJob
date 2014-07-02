using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using NuGetGallery.MirrorJob.Configuration;
using NuGetGallery.MirrorJob.Utility;

namespace NuGetGallery.MirrorJob
{
    public class MirrorJob
    {
        public async void Run()
        {
            var feeds = this.GetFeedsForJob().ToDictionary(document => document.Root.Attribute(XNamespace.Xml + "base").Value);

            foreach (var VARIABLE in COLLECTION)
            {
                
            }
        }

        public IEnumerable<XDocument> GetFeedsForJob()
        {
            var config = Configuration.MirrorJobPackageConfigurationSection.GetConfiguration();
            var uniqueFeedListUris = config.MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>().Select(item => new Uri(item.SourceFeed.ToString() + "Packages")).Distinct();

            var nugetApi = new NugetApi();

            var downloadTasks = uniqueFeedListUris.Select(nugetApi.GetPackagesFromFeed).ToArray();
            Task.WaitAll(downloadTasks);

            return downloadTasks.Select(task => task.Result);
        }
    }
}