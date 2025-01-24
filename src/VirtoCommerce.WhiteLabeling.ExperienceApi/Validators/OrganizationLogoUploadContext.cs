using VirtoCommerce.FileExperienceApi.Core.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Validators;

public class OrganizationLogoUploadContext
{
    public File Logo { get; set; }

    public string OrganizationId { get; set; }
}
