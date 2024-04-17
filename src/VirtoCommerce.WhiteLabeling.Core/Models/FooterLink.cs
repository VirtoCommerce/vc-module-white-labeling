using System.Collections.Generic;

namespace VirtoCommerce.WhiteLabeling.Core.Models
{
    public class FooterLink
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public IList<FooterLink> ChildItems { get; set; } = [];
    }
}
