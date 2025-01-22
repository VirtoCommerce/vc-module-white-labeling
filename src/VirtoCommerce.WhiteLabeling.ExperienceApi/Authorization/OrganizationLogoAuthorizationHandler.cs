using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.CustomerModule.Core.Extensions;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.FileExperienceApi.Core.Models;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Security.Authorization;
using static VirtoCommerce.FileExperienceApi.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.QuoteModule.ExperienceApi.Authorization;

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
        var authorized = false;

        var organizationId = "";

        switch (context.Resource)
        {
            case File file when file.OwnerEntityType == nameof(Organization):
                organizationId = file.OwnerEntityId;
                break;
            case string:
                organizationId = context.Resource as string;
                break;
        }

        if (context.User.GetCurrentOrganizationId() == organizationId)
        {
            authorized = requirement.Permission switch
            {
                Create or Update or Delete => IsOrganizationMaintainer(context.User),
                _ => true,// authorize all read permissions
            };
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

    private static bool IsOrganizationMaintainer(ClaimsPrincipal principal)
    {
        return principal.HasGlobalPermission("xapi:my_organization:edit");
    }
}
