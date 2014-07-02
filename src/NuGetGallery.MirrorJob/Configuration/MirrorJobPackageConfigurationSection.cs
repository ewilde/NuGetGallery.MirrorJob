using System.Configuration;

namespace NuGetGallery.MirrorJob.Configuration
{
    public class MirrorJobPackageConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("MirrorJobPackages", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(MirrorJobPackageConfigurationSection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public MirrorJobPackageConfigurationCollection MirrorJobPackages
        {
            get
            {
                return (MirrorJobPackageConfigurationCollection)base["MirrorJobPackages"];
            }
        }

        public static MirrorJobPackageConfigurationSection GetConfiguration()
        {
            return ConfigurationManager.GetSection("MirrorJobsSection") as MirrorJobPackageConfigurationSection;
        }
    }
}