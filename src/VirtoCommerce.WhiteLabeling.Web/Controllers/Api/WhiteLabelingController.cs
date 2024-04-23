using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public WhiteLabelingController(IWhiteLabelingSettingService whiteLabelingSettingService,
            IWhiteLabelingSettingSearchService whiteLabelingSettingSearchService)
        {
            _whiteLabelingSettingService = whiteLabelingSettingService;
            _whiteLabelingSettingSearchService = whiteLabelingSettingSearchService;
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
    }
}
