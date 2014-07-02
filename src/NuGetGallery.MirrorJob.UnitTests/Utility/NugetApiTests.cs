using NuGetGallery.MirrorJob.Utility;
using NUnit.Framework;

namespace NuGetGallery.MirrorJob.UnitTests.Utility
{
    [TestFixture]
    public class NugetApiTests
    {
        [Test]
        public async void can_get_package_list()
        {
            var nugetApi = new NugetApi();
            var document = await nugetApi.GetPackagesFromFeed().ConfigureAwait(false);

            Assert.That(document , Is.Not.Null);
        }
    }
}