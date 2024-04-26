using GraphQL.Types;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas
{
    public class FaviconType : ObjectGraphType<ExpFavicon>
    {
        public FaviconType()
        {
            Field(x => x.Rel, nullable: true).Description("Rel of the favicon");
            Field(x => x.Type, nullable: true).Description("Type of the favicon");
            Field(x => x.Sizes, nullable: true).Description("Sizes of the favicon");
            Field(x => x.Href, nullable: true).Description("Href of the favicon");
        }
    }
}
