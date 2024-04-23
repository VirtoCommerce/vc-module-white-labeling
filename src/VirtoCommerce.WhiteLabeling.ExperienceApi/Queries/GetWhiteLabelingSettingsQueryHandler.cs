using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtoCommerce.ContentModule.Core.Model;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.CustomerModule.Core.Services;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.ExperienceApiModule.XCMS;
using VirtoCommerce.ExperienceApiModule.XCMS.Queries;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQueryHandler : IQueryHandler<GetWhiteLabelingSettingsQuery, ExpWhiteLabelingSetting>
    {
        private readonly IWhiteLabelingSettingSearchService _whiteLabelingSettingSearchService;
        private readonly IMemberService _memberService;
        private readonly IMediator _mediator;

        public GetWhiteLabelingSettingsQueryHandler(IWhiteLabelingSettingSearchService whiteLabelingSettingSearchService, IMemberService memberService, IMediator mediator)
        {
            _whiteLabelingSettingSearchService = whiteLabelingSettingSearchService;
            _memberService = memberService;
            _mediator = mediator;
        }

        public async Task<ExpWhiteLabelingSetting> Handle(GetWhiteLabelingSettingsQuery request, CancellationToken cancellationToken)
        {
            // temp
            var mockResult = GetMockData(request);
            if (mockResult != null)
            {
                return mockResult;
            }

            var searchCriteria = AbstractTypeFactory<WhiteLabelingSettingSearchCriteria>.TryCreateInstance();
            searchCriteria.OrganizationId = request.OrganizationId;
            searchCriteria.Take = 1;

            var searchResult = await _whiteLabelingSettingSearchService.SearchAsync(searchCriteria);
            var whiteLabelingSetting = searchResult.Results?.FirstOrDefault();

            if (whiteLabelingSetting == null)
            {
                return null;
            }

            var result = new ExpWhiteLabelingSetting()
            {
                LabelingSetting = whiteLabelingSetting,
            };

            // search organization
            var organization = await _memberService.GetByIdAsync(whiteLabelingSetting.OrganizationId, responseGroup: MemberResponseGroup.Default.ToString());
            if (organization == null)
            {
                return result;
            }

            // attach link list
            var linkListQuery = new GetMenuQuery()
            {
                StoreId = request.StoreId,
                CultureName = request.CultureName,
                Name = $"footer-{organization.Name}",
            };

            var linkList = await _mediator.Send(linkListQuery, cancellationToken);
            result.FooterLinks = linkList?.MenuList?.Items;

            return result;
        }

        private static ExpWhiteLabelingSetting GetMockData(GetWhiteLabelingSettingsQuery request)
        {
            var mockData = new List<ExpWhiteLabelingSetting>()
            {
                new ExpWhiteLabelingSetting()
                {
                    LabelingSetting = new WhiteLabelingSetting()
                    {
                        UserId = "cfcec63a-faa5-4511-9aef-ee7672af6710",
                        OrganizationId = "f081c52234754c9c8229aa42d6a19220",
                        LogoUrl = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/logo-white-labeling-test.svg",
                        SecondaryLogoUrl = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/logo-inverted-white-labeling-test.svg",
                    },
                    Favicons = new List<ExpFavicon>()
                    {
                        new ExpFavicon()
                        {
                            Rel = "shortcut icon",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/favicon.ico"
                        },
                        new ExpFavicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "16x16",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/favicon-16x16.png"
                        },
                        new ExpFavicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "32x32",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/favicon-32x32.png"
                        },
                        new ExpFavicon()
                        {
                            Type = "manifest",
                            Href = "https://vcst-dev.govirto.com/cms-content/assets/b2b-store-assets/manifest.json"
                        },
                    },
                    FooterLinks = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Link = new MenuLink()
                            {
                                Title = "Company Details",
                                Url = "/company-details"
                            },
                            ChildItems = new List<MenuItem>()
                            {
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "About Us",
                                        Url = "/about-us"
                                    }
                                },
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "Investor Relations",
                                        Url = "/investor-relations"
                                    }
                                }
                            }
                        },
                        new MenuItem()
                        {
                            Link = new MenuLink()
                            {
                                Title = "Customer Support",
                                Url = "/customer-support"
                            },
                            ChildItems = new List<MenuItem>()
                            {
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "Catalog Request",
                                        Url = "/catalog-request"
                                    }
                                },
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "Contact Us",
                                        Url = "/contact-us"
                                    }
                                }
                            }
                        }

                    },
                },
                // QA test entry
                new ExpWhiteLabelingSetting()
                {
                    LabelingSetting = new WhiteLabelingSetting()
                    {
                        UserId = "de8ec5fa-4633-4d81-bb53-8e7ac23a057b",
                        OrganizationId = "05763259-a5d3-4650-ad75-51f416a6368e",
                        LogoUrl = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/logo-white-labeling-test.svg",
                        SecondaryLogoUrl = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/logo-inverted-white-labeling-test.svg",
                    },
                    Favicons = new List<ExpFavicon>()
                    {
                        new ExpFavicon()
                        {
                            Rel = "shortcut icon",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/favicon.ico"
                        },
                        new ExpFavicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "16x16",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/favicon-16x16.png"
                        },
                        new ExpFavicon()
                        {
                            Rel = "icon",
                            Type = "icon/png",
                            Sizes = "32x32",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/favicon-32x32.png"
                        },
                        new ExpFavicon()
                        {
                            Type = "manifest",
                            Href = "https://vcst-qa.govirto.com/cms-content/assets/b2b-store-assets/manifest.json"
                        },
                    },
                    FooterLinks = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Link = new MenuLink()
                            {
                                Title = "Company Details",
                                Url = "/company-details"
                            },
                            ChildItems = new List<MenuItem>()
                            {
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "About Us",
                                        Url = "/about-us"
                                    }
                                },
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "Investor Relations",
                                        Url = "/investor-relations"
                                    }
                                }
                            }
                        },
                        new MenuItem()
                        {
                            Link = new MenuLink()
                            {
                                Title = "Customer Support",
                                Url = "/customer-support"
                            },
                            ChildItems = new List<MenuItem>()
                            {
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "Catalog Request",
                                        Url = "/catalog-request"
                                    }
                                },
                                new MenuItem()
                                {
                                    Link = new MenuLink()
                                    {
                                        Title = "Contact Us",
                                        Url = "/contact-us"
                                    }
                                }
                            }
                        }
                    }
                },
            };

            var result = mockData.FirstOrDefault(x => x.LabelingSetting.OrganizationId == request.OrganizationId || x.LabelingSetting.UserId == request.UserId);
            return result;
        }
    }
}
