using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.Xapi.Core.Infrastructure;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetCachedWhiteLabelingSettingsQueryHandler : IQueryHandler<GetCachedWhiteLabelingSettingsQuery, ExpWhiteLabelingSetting>
    {
        public async Task<ExpWhiteLabelingSetting> Handle(GetCachedWhiteLabelingSettingsQuery request, CancellationToken cancellationToken)
        {
            var result = new ExpWhiteLabelingSetting
            {
                LabelingSetting = new WhiteLabelingSetting
                {
                    LogoUrl = "https://vcst-qa.govirto.com/cms-content/assets/customization/logo_B2B-store_1744274373508.png",
                    ThemePresetName = "Default",
                    FaviconUrl = "https://vcst-qa.govirto.com/cms-content/assets/customization/favicons/favicon_B2B-store_1740495196822_16x16.png",
                },
                Favicons =
                [
                    new ExpFavicon()
                    {
                        Rel = "icon",
                        Type = "image/png",
                        Sizes = "16x16",
                        Href = "https://vcst-qa.govirto.com/cms-content/assets/customization/favicons/favicon_B2B-store_1740495196822_16x16.png",
                    },
                    new ExpFavicon()
                    {
                        Rel = "icon",
                        Type = "image/png",
                        Sizes = "32x32",
                        Href = "https://vcst-qa.govirto.com/cms-content/assets/customization/favicons/favicon_B2B-store_1740495196822_32x32.png",
                    },
                    new ExpFavicon()
                    {
                        Rel = "icon",
                        Type = "image/png",
                        Sizes = "96x96",
                        Href = "https://vcst-qa.govirto.com/cms-content/assets/customization/favicons/favicon_B2B-store_1740495196822_96x96.png",
                    },
                    new ExpFavicon()
                    {
                        Rel = "icon",
                        Type = "image/png",
                        Sizes = "128x128",
                        Href = "https://vcst-qa.govirto.com/cms-content/assets/customization/favicons/favicon_B2B-store_1740495196822_128x128.png",
                    },
                    new ExpFavicon()
                    {
                        Rel = "icon",
                        Type = "image/png",
                        Sizes = "196x196",
                        Href = "https://vcst-qa.govirto.com/cms-content/assets/customization/favicons/favicon_B2B-store_1740495196822_196x196.png",
                    },
                ],
            };

            return await Task.FromResult(result);
        }
    }
}
