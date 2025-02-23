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
            var whiteLabelingSetting = await GetWhiteLabelingSettingAsync(request);
            if (whiteLabelingSetting == null)
            {
                return null;
            }

            var result = new ExpWhiteLabelingSetting()
            {
                LabelingSetting = whiteLabelingSetting,
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

        protected virtual async Task<WhiteLabelingSetting> GetWhiteLabelingSettingAsync(GetWhiteLabelingSettingsQuery request)
        {
            WhiteLabelingSetting whiteLabelingSetting = null;

            if (!string.IsNullOrEmpty(request.OrganizationId))
            {
                whiteLabelingSetting = await GetWhiteLabelingSettingAsync(organizationId: request.OrganizationId);
            }

            if (whiteLabelingSetting == null && !string.IsNullOrEmpty(request.StoreId))
            {
                whiteLabelingSetting = await GetWhiteLabelingSettingAsync(storeId: request.StoreId);
            }

            return whiteLabelingSetting;
        }

        private async Task<WhiteLabelingSetting> GetWhiteLabelingSettingAsync(string organizationId = null, string storeId = null)
        {
            var searchCriteria = AbstractTypeFactory<WhiteLabelingSettingSearchCriteria>.TryCreateInstance();

            searchCriteria.OrganizationId = organizationId;
            searchCriteria.StoreId = storeId;
            searchCriteria.IsEnabled = true;
            searchCriteria.Take = 1;

            var searchResult = await _whiteLabelingSettingSearchService.SearchAsync(searchCriteria);

            return searchResult.Results?.FirstOrDefault();
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
    }
}
