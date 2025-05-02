using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.Xapi.Core.BaseQueries;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries;
public class GetCachedWhiteLabelingSettingsQuery : Query<ExpWhiteLabelingSetting>
{
    public string OrganizationId { get; set; }

    public string StoreId { get; set; }

    public override IEnumerable<QueryArgument> GetArguments()
    {
        yield return Argument<StringGraphType>(nameof(OrganizationId));
        yield return Argument<StringGraphType>(nameof(StoreId));
    }

    public override void Map(IResolveFieldContext context)
    {
        OrganizationId = context.GetArgument<string>(nameof(OrganizationId));
        StoreId = context.GetArgument<string>(nameof(StoreId));
    }
}
