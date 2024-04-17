using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using VirtoCommerce.ExperienceApiModule.Core.BaseQueries;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Queries
{
    public class GetWhiteLabelingSettingsQuery : Query<WhiteLabelingSettings>
    {
        public string OrganizationId { get; set; }

        public string UserId { get; set; }

        public string CultureName { get; set; }

        public override IEnumerable<QueryArgument> GetArguments()
        {
            yield return Argument<StringGraphType>(nameof(OrganizationId));
            yield return Argument<StringGraphType>(nameof(UserId));
            yield return Argument<StringGraphType>(nameof(CultureName));
        }

        public override void Map(IResolveFieldContext context)
        {
            OrganizationId = context.GetArgument<string>(nameof(OrganizationId));
            UserId = context.GetArgument<string>(nameof(UserId));
            CultureName = context.GetArgument<string>(nameof(CultureName));
        }
    }
}
