using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Queries
{
    public class GetWhiteLabelingSettingsQueryHandler : IQueryHandler<GetWhiteLabelingSettingsQuery, WhiteLabelingSettings>
    {
        public GetWhiteLabelingSettingsQueryHandler()
        {
        }

        public Task<WhiteLabelingSettings> Handle(GetWhiteLabelingSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = new List<WhiteLabelingSettings>()
            {
                new WhiteLabelingSettings()
                {
                    UserId = "cfcec63a-faa5-4511-9aef-ee7672af6710",
                    OrganizationId = "f081c52234754c9c8229aa42d6a19220",
                    LogoUrl = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/logo-white-labeling-test.svg",
                    SecondaryLogoUrl = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/logo-inverted-white-labeling-test.svg",
                    FaviconUrl = "/static/icons/favicon-1.svg",
                    FooterLinks = new List<FooterLink>()
                    {
                        new FooterLink()
                        {
                            Title = "Company Details",
                            Url = "/company-details",
                            ChildItems = new List<FooterLink>()
                            {
                                new FooterLink()
                                {
                                    Title = "About Us",
                                    Url = "/about-us"
                                },
                                new FooterLink()
                                {
                                    Title = "Investor Relations",
                                    Url = "/investor-relations"
                                }
                            }
                        },
                        new FooterLink()
                        {
                            Title = "Customer Support",
                            Url = "/customer-support",
                            ChildItems = new List<FooterLink>()
                            {
                                new FooterLink()
                                {
                                    Title = "Catalog Request",
                                    Url = "/catalog-request"
                                },
                                new FooterLink()
                                {
                                    Title = "Contact Us",
                                    Url = "/contact-us"
                                }
                            }
                        }
                    }
                }
            };

            var result = settings.FirstOrDefault(x => x.OrganizationId == request.OrganizationId || x.UserId == request.UserId);
            return Task.FromResult(result);
        }
    }
}
