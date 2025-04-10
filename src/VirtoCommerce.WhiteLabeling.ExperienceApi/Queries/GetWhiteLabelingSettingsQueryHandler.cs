using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.CustomerModule.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.Xapi.Core.Infrastructure;
using VirtoCommerce.XCMS.Core.Queries;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQueryHandler : IQueryHandler<GetWhiteLabelingSettingsQuery, ExpWhiteLabelingSetting>
    {
        private readonly IWhiteLabelingSettingSearchService _whiteLabelingSettingSearchService;
        private readonly IMemberService _memberService;
        private readonly IMediator _mediator;

        private static readonly string[] _faviconsSizes = ["16x16", "32x32", "96x96", "128x128", "196x196"];

        public GetWhiteLabelingSettingsQueryHandler(IWhiteLabelingSettingSearchService whiteLabelingSettingSearchService, IMemberService memberService, IMediator mediator)
        {
            _whiteLabelingSettingSearchService = whiteLabelingSettingSearchService;
            _memberService = memberService;
            _mediator = mediator;
        }

        public async Task<ExpWhiteLabelingSetting> Handle(GetWhiteLabelingSettingsQuery request, CancellationToken cancellationToken)
        {
            var whiteLabelingSettings = await GetWhiteLabelingSettingsAsync(request);
            if (whiteLabelingSettings.OrganizationSetting == null && whiteLabelingSettings.StoreSetting == null)
            {
                return null;
            }

            var combinedWhiteLabelingSetting = GetCombinedWhiteLabelingSetting(whiteLabelingSettings);
            var whiteLabelingSetting = combinedWhiteLabelingSetting.Item1;
            var whiteLabelingFlags = combinedWhiteLabelingSetting.Item2;

            var result = new ExpWhiteLabelingSetting
            {
                LabelingSetting = whiteLabelingSetting,
                LabelingFlags = whiteLabelingFlags,
            };

            // add favicons
            if (!string.IsNullOrEmpty(whiteLabelingSetting.FaviconUrl))
            {
                foreach (var faviconSize in _faviconsSizes)
                {
                    var newFavicon = new ExpFavicon()
                    {
                        Rel = "icon",
                        Type = $"image/{Path.GetExtension(whiteLabelingSetting.FaviconUrl)[1..]}",
                        Sizes = faviconSize,
                        Href = GenerateFaviconName(whiteLabelingSetting.FaviconUrl, faviconSize),
                    };
                    result.Favicons.Add(newFavicon);
                }
            }

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

        [Obsolete("Use GetWhiteLabelingSettingsAsync", DiagnosticId = "VC0010", UrlFormat = "https://docs.virtocommerce.org/platform/user-guide/versions/virto3-products-versions/")]
        protected virtual async Task<WhiteLabelingSetting> GetWhiteLabelingSettingAsync(GetWhiteLabelingSettingsQuery request)
        {
            WhiteLabelingSetting whiteLabelingSetting = null;

            if (!string.IsNullOrEmpty(request.OrganizationId))
            {
                whiteLabelingSetting = (await GetWhiteLabelingSettingAsync(organizationId: request.OrganizationId)).FirstOrDefault();
            }

            if (whiteLabelingSetting == null && !string.IsNullOrEmpty(request.StoreId))
            {
                whiteLabelingSetting = (await GetWhiteLabelingSettingAsync(storeId: request.StoreId)).FirstOrDefault();
            }

            return whiteLabelingSetting;
        }

        protected virtual async Task<WhiteLabelingSettingResult> GetWhiteLabelingSettingsAsync(GetWhiteLabelingSettingsQuery request)
        {
            var result = new WhiteLabelingSettingResult();

            var settings = await GetWhiteLabelingSettingAsync(organizationId: request.OrganizationId, storeId: request.StoreId);

            result.OrganizationSetting = settings.FirstOrDefault(x => x.OrganizationId == request.OrganizationId && x.StoreId == null);
            result.StoreSetting = settings.FirstOrDefault(x => x.StoreId == request.StoreId && x.OrganizationId == null);

            return result;
        }

        private async Task<WhiteLabelingSetting[]> GetWhiteLabelingSettingAsync(string organizationId = null, string storeId = null)
        {
            var searchCriteria = AbstractTypeFactory<WhiteLabelingSettingSearchCriteria>.TryCreateInstance();

            searchCriteria.IsEnabled = true;
            searchCriteria.OrganizationId = organizationId;
            searchCriteria.StoreId = storeId;

            var searchResult = await _whiteLabelingSettingSearchService.SearchAsync(searchCriteria);

            return searchResult.Results.ToArray();
        }

        private static (WhiteLabelingSetting, WhiteLabelingFlags) GetCombinedWhiteLabelingSetting(WhiteLabelingSettingResult result)
        {
            var organizationFlags = LabelingFlags(result.OrganizationSetting);

            if (result.StoreSetting == null)
            {
                return (result.OrganizationSetting, organizationFlags);
            }

            if (result.OrganizationSetting == null)
            {
                return (result.StoreSetting, organizationFlags);
            }

            var combinedSettings = new WhiteLabelingSetting()
            {
                IsEnabled = true,
                OrganizationId = result.OrganizationSetting.OrganizationId,
                StoreId = result.StoreSetting.StoreId,
                LogoUrl = organizationFlags.HasLogo ? result.OrganizationSetting.LogoUrl : result.StoreSetting.LogoUrl,
                SecondaryLogoUrl = organizationFlags.HasSecondaryLogo ? result.OrganizationSetting.SecondaryLogoUrl : result.StoreSetting.SecondaryLogoUrl,
                FaviconUrl = organizationFlags.HasFavicon ? result.OrganizationSetting.FaviconUrl : result.StoreSetting.FaviconUrl,
                FooterLinkListName = !string.IsNullOrEmpty(result.OrganizationSetting.FooterLinkListName) ? result.OrganizationSetting.FooterLinkListName : result.StoreSetting.FooterLinkListName,
                ThemePresetName = !string.IsNullOrEmpty(result.OrganizationSetting.ThemePresetName) ? result.OrganizationSetting.ThemePresetName : result.StoreSetting.ThemePresetName,
            };

            return (combinedSettings, organizationFlags);
        }

        private static WhiteLabelingFlags LabelingFlags(WhiteLabelingSetting labelingSetting)
        {
            return labelingSetting == null
                ? null
                : new WhiteLabelingFlags
                {
                    HasLogo = !string.IsNullOrEmpty(labelingSetting.LogoUrl),
                    HasSecondaryLogo = !string.IsNullOrEmpty(labelingSetting.SecondaryLogoUrl),
                    HasFavicon = !string.IsNullOrEmpty(labelingSetting.FaviconUrl),
                };
        }

        // copy-pasted from VirtoCommerce.ImageToolsModule.Data.ThumbnailGeneration.FileNameHelper, didn't want to add a dependency just for one method
        private static string GenerateFaviconName(string fileName, string aliasName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            var newName = string.Concat(name, "_" + aliasName, extension);

            var uri = new Uri(fileName);
            var uriWithoutLastSegment = uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);

            var result = new Uri(new Uri(uriWithoutLastSegment), newName);

            return result.AbsoluteUri;
        }

        public class WhiteLabelingSettingResult
        {
            public WhiteLabelingSetting OrganizationSetting { get; set; }

            public WhiteLabelingSetting StoreSetting { get; set; }
        }
    }
}
