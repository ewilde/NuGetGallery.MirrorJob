using System;
using System.Configuration;

namespace NuGetGallery.MirrorJob.Configuration
{
    public class MirrorJobPackageConfigurationElement : ConfigurationElement
    {
        public MirrorJobPackageConfigurationElement()
        {
        }

        public MirrorJobPackageConfigurationElement(string packageName)
        {
            this.PackageName = packageName;
        }

        [ConfigurationProperty("PackageName", DefaultValue = null, IsRequired = true, IsKey = true)]
        public string PackageName
        {
            get { return (string)this["PackageName"]; }
            set { this["PackageName"] = value; }
        }

        [ConfigurationProperty("TargetApiKey", DefaultValue = null, IsRequired = true, IsKey = false)]
        public string TargetApiKey
        {
            get { return (string)this["TargetApiKey"]; }
            set { this["TargetApiKey"] = value; }
        }

        [ConfigurationProperty("SourceFeed", DefaultValue = null, IsRequired = true, IsKey = false)]
        public Uri SourceFeed
        {
            get { return (Uri) this["SourceFeed"]; }
            set { this["SourceFeed"] = value; }
        }

        [ConfigurationProperty("TargetFeed", DefaultValue = null, IsRequired = true, IsKey = false)]
        public Uri TargetFeed
        {
            get { return (Uri)this["TargetFeed"]; }
            set { this["TargetFeed"] = value; }
        }
    }
}