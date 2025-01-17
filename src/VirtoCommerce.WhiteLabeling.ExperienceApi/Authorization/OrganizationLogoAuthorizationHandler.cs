using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.CustomerModule.Core.Extensions;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.FileExperienceApi.Core.Models;
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

        var ownerOrganizationId = "";

        switch (context.Resource)
        {
            case File file when file.OwnerEntityType == nameof(Organization):
                ownerOrganizationId = file.OwnerEntityId;
                break;
            case string:
                ownerOrganizationId = context.Resource as string;
                break;
        }

        switch (requirement.Permission)
        {
            case Update:
                authorized = context.User.GetCurrentOrganizationId() == ownerOrganizationId;
                break;
            case Delete:
                authorized = context.User.GetCurrentOrganizationId() == ownerOrganizationId;
                break;
            default:
                // authorize all other permissions
                authorized = true;
                break;
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
}
