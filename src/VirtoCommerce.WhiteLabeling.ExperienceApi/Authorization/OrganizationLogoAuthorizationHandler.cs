using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.CustomerModule.Core.Extensions;
using VirtoCommerce.FileExperienceApi.Core.Models;
using VirtoCommerce.Platform.Core;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Security.Authorization;
using static VirtoCommerce.FileExperienceApi.Core.ModuleConstants.Security.Permissions;
using static VirtoCommerce.WhiteLabeling.Core.ModuleConstants;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Authorization;

public class OrganizationLogoAuthorizationRequirement : PermissionAuthorizationRequirement
{
    public OrganizationLogoAuthorizationRequirement(string permission) : base(permission)
    {
    }
}

public class OrganizationLogoAuthorizationHandler : PermissionAuthorizationHandlerBase<OrganizationLogoAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OrganizationLogoAuthorizationRequirement requirement)
    {
        var authorized = context.User.IsInRole(PlatformConstants.Security.SystemRoles.Administrator) || requirement.Permission == Read;

        if (!authorized)
        {
            authorized = CheckRequirement(context, requirement);
        }

        if (authorized)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }

    private static bool CheckRequirement(AuthorizationHandlerContext context, OrganizationLogoAuthorizationRequirement requirement)
    {
        if (context.Resource is not File file || file.Scope != OrganizationLogoUploadScope)
        {
            return false;
        }

        var authorized = false;

        if (context.User.GetCurrentOrganizationId() == file.OwnerEntityId)
        {
            authorized = requirement.Permission switch
            {
                Create or Update or Delete => IsOrganizationMaintainer(context.User),
                _ => false,
            };
        }
        else if (string.IsNullOrEmpty(file.OwnerEntityId))
        {
            authorized = requirement.Permission switch
            {
                Delete => IsOrganizationMaintainer(context.User),
                _ => false,
            };
        }

        return authorized;
    }

    private static bool IsOrganizationMaintainer(ClaimsPrincipal principal)
    {
        return principal.HasGlobalPermission("xapi:my_organization:edit");
    }
}
