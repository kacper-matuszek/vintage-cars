using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Service.Installation
{
    public static partial class NopInstallationDefaults
    {
        /// <summary>
        /// Gets a path to the localization resources file
        /// </summary>
        public static string LocalizationResourcesPath => "~/App_Data/Localization/";

        /// <summary>
        /// Gets a localization resources file extension
        /// </summary>
        public static string LocalizationResourcesFileExtension => "nopres.xml";
    }
}
