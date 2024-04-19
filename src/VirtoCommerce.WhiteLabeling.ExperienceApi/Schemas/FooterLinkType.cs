using GraphQL.Types;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas
{
    public class FooterLinkType : ObjectGraphType<FooterLink>
    {
        public FooterLinkType()
        {
            Field(x => x.Title, nullable: true).Description("Title of the footer link");
            Field(x => x.Url, nullable: true).Description("URL of the footer link");
            Field<ListGraphType<FooterLinkType>>("childItems", resolve: context => context.Source.ChildItems);
        }
    }
}
