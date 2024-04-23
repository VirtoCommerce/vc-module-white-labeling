using GraphQL.Types;
using VirtoCommerce.ExperienceApiModule.XCMS.Schemas;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas
{
    public class WhiteLabelingSettingsType : ObjectGraphType<ExpWhiteLabelingSetting>
    {
        public WhiteLabelingSettingsType()
        {
            Field(x => x.LabelingSetting.IsEnabled, nullable: false).Description("Is enabled");
            Field(x => x.LabelingSetting.UserId, nullable: true).Description("User ID");
            Field(x => x.LabelingSetting.OrganizationId, nullable: true).Description("Organization ID");
            Field(x => x.LabelingSetting.LogoUrl, nullable: true).Description("Logo URL");
            Field(x => x.LabelingSetting.SecondaryLogoUrl, nullable: true).Description("Logo URL for footer");
            Field(x => x.LabelingSetting.FaviconUrl, nullable: true).Description("FavIcon");
            Field(x => x.LabelingSetting.ThemePresetName, nullable: true).Description("Theme preset name");

            Field<ListGraphType<MenuLinkType>>("footerLinks", resolve: context => context.Source.FooterLinks);

            Field<ListGraphType<FaviconType>>("favicons", resolve: context => context.Source.Favicons);
        }
    }
}
