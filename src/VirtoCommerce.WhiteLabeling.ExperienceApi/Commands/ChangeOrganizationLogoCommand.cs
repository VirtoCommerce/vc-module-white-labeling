using GraphQL.Types;
using VirtoCommerce.Xapi.Core.Infrastructure;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Commands;

public class ChangeOrganizationLogoCommand : ICommand<bool>
{
    public string OrganizationId { get; set; }

    public string LogoUrl { get; set; }
}

public class InputChangeOrganizationLogoCommandType : InputObjectGraphType<ChangeOrganizationLogoCommand>
{
    public InputChangeOrganizationLogoCommandType()
    {
        Field(x => x.OrganizationId, nullable: false);
        Field(x => x.LogoUrl, nullable: false);
    }
}
