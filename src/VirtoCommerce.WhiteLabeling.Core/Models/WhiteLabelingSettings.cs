using System.Collections.Generic;

namespace VirtoCommerce.WhiteLabeling.Core.Models
{
    public class WhiteLabelingSettings
    {
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
        public string LogoUrl { get; set; }
        public string SecondaryLogoUrl { get; set; }
        public string FaviconUrl { get; set; }
        public IList<FooterLink> FooterLinks { get; set; } = [];
        public IList<Favicon> Favicons { get; set; } = [];
    }
}
