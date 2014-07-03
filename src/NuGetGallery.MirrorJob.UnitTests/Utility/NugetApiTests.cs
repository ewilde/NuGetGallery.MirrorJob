using System.IO;
using System.Linq;
using NuGetGallery.MirrorJob.Configuration;
using NuGetGallery.MirrorJob.Utility;
using NUnit.Framework;

namespace NuGetGallery.MirrorJob.UnitTests.Utility
{
    [TestFixture]
    public class NugetApiTests
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            if (Directory.Exists("NugetApiTests"))
            {
                Directory.Delete("NugetApiTests", true);
            }

            Directory.CreateDirectory("NugetApiTests");
        }

        [TestFixtureTearDown]
        public void FixutureTearDown()
        {
            if (Directory.Exists("NugetApiTests"))
            {
                Directory.Delete("NugetApiTests", true);
            }
        }

        [Test]
        public async void can_get_package_list()
        {
            var nugetApi = new NugetApi();
            var document = await nugetApi.GetPackagesFromFeed().ConfigureAwait(false);

            Assert.That(document , Is.Not.Null);
        }

        public async void can_down_load_a_package()
        {
            var nugetApi = new NugetApi();
            var nunitConfig =
                MirrorJobPackageConfigurationSection.GetConfiguration()
                    .MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>().First(item=>item.PackageName.Equals("nunit"));

            var feed = await nugetApi.GetPackagesFromFeed(nunitConfig.SourceFeed).ConfigureAwait(false);
           // /var nunitEntry = feed.Element()
            //nugetApi.DownloadPackage(confi)
        }
    }
}