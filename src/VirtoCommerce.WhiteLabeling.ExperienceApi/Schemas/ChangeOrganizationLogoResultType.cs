using GraphQL.Types;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;

public class ChangeOrganizationLogoResultType : ObjectGraphType<ChangeOrganizationLogoResult>
{
    public ChangeOrganizationLogoResultType()
    {
        Field(x => x.IsSuccess, nullable: false);
        Field(x => x.ErrorMessage, nullable: true);
    }
}
