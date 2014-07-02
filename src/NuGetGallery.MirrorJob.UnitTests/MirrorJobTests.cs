using System.Linq;
using NUnit.Framework;

namespace NuGetGallery.MirrorJob.UnitTests
{
    [TestFixture]
    public class MirrorJobTests
    {
        [Test]
        public void run_job()
        {
            new MirrorJob().Run();
        }

        [Test]
        public void get_feeds_for_job_should_return_one_feed_per_distinct_source_feed()
        {
            // In our app.config we have
            //<MirrorJobPackages>
            //      <add PackageName="nunit" SourceFeed="http://www.nuget.org/api/v2" TargetFeed="http://localhost/nuget/api/v2" TargetApiKey="99af9aa7-c8a0-4ce0-baf8-13f186d89586" />
            //      <add PackageName="rhinomocks" SourceFeed="http://www.nuget.org/api/v2" TargetFeed="http://localhost/nuget/api/v2" TargetApiKey="99af9aa7-c8a0-4ce0-baf8-13f186d89586" />
            //      <add PackageName="Autofac" SourceFeed="https://www.myget.org/F/autofac" TargetFeed="http://localhost/nuget/api/v2" TargetApiKey="99af9aa7-c8a0-4ce0-baf8-13f186d89586" />
            //    </MirrorJobPackages>

            Assert.That(new MirrorJob().GetFeedsForJob().Count(), Is.EqualTo(2));
        }
    }
}