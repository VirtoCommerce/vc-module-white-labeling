using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.Xapi.Core.BaseQueries;
using VirtoCommerce.Xapi.Core.Extensions;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQuery : Query<ExpWhiteLabelingSetting>
    {
        public string OrganizationId { get; set; }

        public string UserId { get; set; }

        public string StoreId { get; set; }

        public string Domain { get; set; }

        public string CultureName { get; set; }

        public override IEnumerable<QueryArgument> GetArguments()
        {
            yield return Argument<StringGraphType>(nameof(OrganizationId));
            yield return Argument<StringGraphType>(nameof(UserId));
            yield return Argument<StringGraphType>(nameof(StoreId));
            yield return Argument<StringGraphType>(nameof(Domain));
            yield return Argument<StringGraphType>(nameof(CultureName));
        }

        public override void Map(IResolveFieldContext context)
        {
            OrganizationId = context.GetArgument<string>(nameof(OrganizationId)) ?? context.GetCurrentOrganizationId();
            UserId = context.GetArgument<string>(nameof(UserId));
            StoreId = context.GetArgument<string>(nameof(StoreId));
            Domain = context.GetArgument<string>(nameof(Domain));
            CultureName = context.GetArgument<string>(nameof(CultureName));
        }
    }
}
