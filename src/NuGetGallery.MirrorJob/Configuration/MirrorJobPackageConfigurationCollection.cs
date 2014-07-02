using System.Configuration;

namespace NuGetGallery.MirrorJob.Configuration
{
    public class MirrorJobPackageConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MirrorJobPackageConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MirrorJobPackageConfigurationElement)element).PackageName;
        }
    }
}