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
        public void Run()
        {
            var feeds = this.GetFeedsForJob().Result.ToDictionary(document => document.Root.Attribute(XNamespace.Xml + "base").Value);

            foreach (var item in feeds)
            {
                
            }
        }

        public async Task<IEnumerable<XDocument>> GetFeedsForJob()
        {
            var config = Configuration.MirrorJobPackageConfigurationSection.GetConfiguration();
            var uniqueFeedListUris = config.MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>().Select(item => new Uri(item.SourceFeed.ToString() + "Packages")).Distinct();

            var nugetApi = new NugetApi();

            return await Task.WhenAll(uniqueFeedListUris.Select(nugetApi.GetPackagesFromFeed));
           
        }
    }
}