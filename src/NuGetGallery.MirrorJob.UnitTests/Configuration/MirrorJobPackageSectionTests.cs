using System.Linq;
using NuGetGallery.MirrorJob.Configuration;
using NUnit.Framework;

namespace NuGetGallery.MirrorJob.UnitTests.Configuration
{
    [TestFixture]
    public class MirrorJobPackageSectionTests
    {
        [Test]
        public void can_read_configuration_section()
        {
            Assert.That(MirrorJobPackageConfigurationSection.GetConfiguration(), Is.Not.Null);
        }

        [Test]
        public void can_read_configuration_element_package_name()
        {
            Assert.That(MirrorJobPackageConfigurationSection.GetConfiguration().MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>().First<MirrorJobPackageConfigurationElement>().PackageName, Is.EqualTo("nunit"));
        }

        [Test]
        public void can_read_configuration_element_source_feed()
        {
            Assert.That(MirrorJobPackageConfigurationSection.GetConfiguration()
                .MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>()
                .First<MirrorJobPackageConfigurationElement>().SourceFeed.ToString(), Is.EqualTo("http://www.nuget.org/api/v2"));

        }

        [Test]
        public void can_read_configuration_element_target_feed()
        {
            Assert.That(MirrorJobPackageConfigurationSection.GetConfiguration()
                .MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>()
                .First<MirrorJobPackageConfigurationElement>().TargetFeed.ToString(), Is.EqualTo("http://localhost/nuget/api/v2"));

        }

        [Test]
        public void can_read_configuration_element_target_api_key()
        {
            Assert.That(MirrorJobPackageConfigurationSection.GetConfiguration()
                .MirrorJobPackages.Cast<MirrorJobPackageConfigurationElement>()
                .First<MirrorJobPackageConfigurationElement>().TargetApiKey.ToString(), Is.EqualTo("99af9aa7-c8a0-4ce0-baf8-13f186d89586"));

        }
    }
}