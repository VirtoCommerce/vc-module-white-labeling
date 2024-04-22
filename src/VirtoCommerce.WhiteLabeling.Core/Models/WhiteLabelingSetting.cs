using System;
using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.WhiteLabeling.Core.Models
{
    public class WhiteLabelingSetting : AuditableEntity, ICloneable
    {
        public bool IsEnabled { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
        public string LogoUrl { get; set; }
        public string SecondaryLogoUrl { get; set; }
        public string FaviconUrl { get; set; }
        public string LinkListName { get; set; }
        public string ThemePresetName { get; set; }

        public IList<Favicon> Favicons { get; set; } = [];

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
