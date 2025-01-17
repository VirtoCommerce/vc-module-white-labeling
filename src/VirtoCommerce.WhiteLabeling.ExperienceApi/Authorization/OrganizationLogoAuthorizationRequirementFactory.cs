using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.FileExperienceApi.Core.Authorization;
using VirtoCommerce.FileExperienceApi.Core.Models;
using static VirtoCommerce.WhiteLabeling.Core.ModuleConstants;

namespace VirtoCommerce.QuoteModule.ExperienceApi.Authorization;

public class OrganizationLogoAuthorizationRequirementFactory : IFileAuthorizationRequirementFactory
{
    public string Scope => OrganizationLogoUploadScope;

    public IAuthorizationRequirement Create(File file, string permission)
    {
        return new OrganizationLogoAuthorizationRequirement(permission);
    }
}
