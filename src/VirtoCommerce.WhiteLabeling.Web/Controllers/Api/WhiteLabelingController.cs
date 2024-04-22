using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.ContentModule.Core.Extensions;
using VirtoCommerce.ContentModule.Core.Model;
using VirtoCommerce.ContentModule.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Core;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;

namespace VirtoCommerce.WhiteLabeling.Web.Controllers.Api
{
    [Route("api/white-labeling")]
    public class WhiteLabelingController : Controller
    {
        private readonly IWhiteLabelingSettingService _whiteLabelingSettingService;
        private readonly IWhiteLabelingSettingSearchService _whiteLabelingSettingSearchService;
        private readonly IMenuLinkListSearchService _linkListSearchService;

        public WhiteLabelingController(IWhiteLabelingSettingService whiteLabelingSettingService,
            IWhiteLabelingSettingSearchService whiteLabelingSettingSearchService,
            IMenuLinkListSearchService linkListSearchService)
        {
            _whiteLabelingSettingService = whiteLabelingSettingService;
            _whiteLabelingSettingSearchService = whiteLabelingSettingSearchService;
            _linkListSearchService = linkListSearchService;
        }

        [HttpGet("{id}")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public async Task<ActionResult<WhiteLabelingSetting>> Get([FromRoute] string id)
        {
            var result = await _whiteLabelingSettingService.GetNoCloneAsync(id);
            return Ok(result);
        }

        [HttpGet("organization/{organizationId}")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public async Task<ActionResult<WhiteLabelingSetting>> GetByOrganizationId([FromRoute] string organizationId)
        {
            var searchCriteria = AbstractTypeFactory<WhiteLabelingSettingSearchCriteria>.TryCreateInstance();

            searchCriteria.OrganizationId = organizationId;
            searchCriteria.Take = 1;

            var searchResult = await _whiteLabelingSettingSearchService.SearchAsync(searchCriteria);
            var result = searchResult.Results?.FirstOrDefault();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(ModuleConstants.Security.Permissions.Create)]
        public async Task<ActionResult<WhiteLabelingSetting>> Create([FromBody] WhiteLabelingSetting model)
        {
            model.Id = null;
            await _whiteLabelingSettingService.SaveChangesAsync([model]);
            return Ok(model);
        }

        [HttpPut]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Update)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateContract([FromBody] WhiteLabelingSetting model)
        {
            await _whiteLabelingSettingService.SaveChangesAsync([model]);
            return NoContent();
        }

        [HttpPost]
        [Route("search-link-lists")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public async Task<ActionResult<WhiteLabelingLinkListSearchResult>> SearchContracts([FromBody] MenuLinkListSearchCriteria searchCriteria)
        {
            var menuLinkLists = await _linkListSearchService.SearchAllNoClone(storeId: searchCriteria.StoreId, name: searchCriteria.Name, batchSize: searchCriteria.Take);

            // group menuLinkLists by storeId and Name and create new MenuLinkList for each group
            var menuLinkGroup = menuLinkLists.GroupBy(x => new { x.StoreId, x.Name })
                .Select(x => new MenuLinkList
                {
                    Id = x.Key.Name,
                    StoreId = x.Key.StoreId,
                    Name = $"{x.Key.Name}|{x.Key.StoreId}",
                })
                .OrderBy(x => x.Name)
                .ToList();


            var searchResult = new WhiteLabelingLinkListSearchResult
            {
                Results = menuLinkGroup,
                TotalCount = menuLinkGroup.Count
            };

            return Ok(searchResult);
        }
    }

    public class WhiteLabelingLinkListSearchResult : GenericSearchResult<MenuLinkList>
    {
    }
}
