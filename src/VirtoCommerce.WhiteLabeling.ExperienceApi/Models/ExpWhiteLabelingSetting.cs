using System.Collections.Generic;
using VirtoCommerce.ExperienceApiModule.XCMS;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Models
{
    public class ExpWhiteLabelingSetting
    {
        public WhiteLabelingSetting LabelingSetting { get; set; }

        public IList<MenuItem> FooterLinks { get; set; } = [];

        public IList<ExpFavicon> Favicons { get; set; } = [];
    }
}
