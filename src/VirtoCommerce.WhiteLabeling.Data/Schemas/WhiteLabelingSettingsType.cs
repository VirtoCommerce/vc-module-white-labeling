using GraphQL.Types;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Schemas
{
    public class WhiteLabelingSettingsType : ObjectGraphType<WhiteLabelingSettings>
    {
        public WhiteLabelingSettingsType()
        {
            Field(x => x.UserId, nullable: true).Description("User ID");
            Field(x => x.OrganizationId, nullable: true).Description("Organization ID");
            Field(x => x.LogoUrl, nullable: true).Description("Logo URL");
            Field(x => x.SecondaryLogoUrl, nullable: true).Description("Logo URL for footer");
            Field(x => x.FaviconUrl, nullable: true).Description("FavIcon");
            Field<ListGraphType<FooterLinkType>>("footerLinks", resolve: context => context.Source.FooterLinks);
        }
    }
}
