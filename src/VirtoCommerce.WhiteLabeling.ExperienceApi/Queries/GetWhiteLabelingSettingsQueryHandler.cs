using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
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
                    Favicons = new List<Favicon>()
                    {
                        new Favicon()
                        {
                            Rel = "shortcut icon",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/favicon.ico"
                        },
                        new Favicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "16x16",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/favicon-16x16.png"
                        },
                        new Favicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "32x32",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/favicon-32x32.png"
                        }
                    },
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
                },
                // QA test entry
                new WhiteLabelingSettings()
                {
                    UserId = "de8ec5fa-4633-4d81-bb53-8e7ac23a057b",
                    OrganizationId = "05763259-a5d3-4650-ad75-51f416a6368e",
                    LogoUrl = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/logo-white-labeling-test.svg",
                    SecondaryLogoUrl = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/logo-inverted-white-labeling-test.svg",
                    Favicons = new List<Favicon>()
                    {
                        new Favicon()
                        {
                            Rel = "shortcut icon",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/favicon.ico"
                        },
                        new Favicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "16x16",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/favicon-16x16.png"
                        },
                        new Favicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "32x32",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/favicon-32x32.png"
                        }
                    },
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
                },
            };

            var result = settings.FirstOrDefault(x => x.OrganizationId == request.OrganizationId || x.UserId == request.UserId);
            return Task.FromResult(result);
        }
    }
}
