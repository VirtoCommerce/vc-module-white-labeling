using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.WhiteLabeling.Core;

namespace VirtoCommerce.WhiteLabeling.Web.Controllers.Api
{
    [Route("api/white-labeling")]
    public class WhiteLabelingController : Controller
    {
        // GET: api/white-labeling
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}
