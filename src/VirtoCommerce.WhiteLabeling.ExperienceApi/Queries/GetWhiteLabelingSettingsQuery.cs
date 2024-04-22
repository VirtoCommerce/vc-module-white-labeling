using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using VirtoCommerce.ExperienceApiModule.Core.BaseQueries;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQuery : Query<ExpWhiteLabelingSetting>
    {
        public string OrganizationId { get; set; }

        public string UserId { get; set; }

        public string StoreId { get; set; }

        public string CultureName { get; set; }

        public override IEnumerable<QueryArgument> GetArguments()
        {
            yield return Argument<StringGraphType>(nameof(OrganizationId));
            yield return Argument<StringGraphType>(nameof(UserId));
            yield return Argument<StringGraphType>(nameof(StoreId));
            yield return Argument<StringGraphType>(nameof(CultureName));
        }

        public override void Map(IResolveFieldContext context)
        {
            OrganizationId = context.GetArgument<string>(nameof(OrganizationId));
            UserId = context.GetArgument<string>(nameof(UserId));
            StoreId = context.GetArgument<string>(nameof(StoreId));
            CultureName = context.GetArgument<string>(nameof(CultureName));
        }
    }
}
