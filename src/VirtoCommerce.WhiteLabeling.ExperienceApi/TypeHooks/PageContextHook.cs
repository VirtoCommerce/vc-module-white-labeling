using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Queries;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;
using VirtoCommerce.Xapi.Core.Helpers;
using VirtoCommerce.Xapi.Core.Infrastructure;
using VirtoCommerce.XFrontend.Core.Models;
using VirtoCommerce.XFrontend.Core.Schemas;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.TypeHooks;

public class PageContextHook : IGraphTypeHook
{
    public string TypeName { get; set; } = "PageContextResponseType";

    public void BeforeTypeInitialized(IGraphType graphType)
    {
        if (graphType is PageContextResponseType pageContextResponseType)
        {
            var fieldAsync = FieldCreator.CreateFieldAsync<PageContextResponse, WhiteLabelingSettingsType>(
              "whiteLabelingSettings",
              "Gets white labeling settings for the current page context.",
              resolve: async x =>
              {
                  if (x.Source == null)
                  {
                      return null;
                  }

                  var query = GetWhiteLabelingSettingQuery(x.Source);

                  var mediator = x.RequestServices.GetRequiredService<IMediator>();
                  var result = await mediator.Send(query);

                  return result;
              });

            pageContextResponseType.AddField(fieldAsync);
        }
    }

    protected virtual GetWhiteLabelingSettingsQuery GetWhiteLabelingSettingQuery(PageContextResponse response)
    {
        return new GetWhiteLabelingSettingsQuery
        {
            CultureName = response.CultureName,
            OrganizationId = response.OrganizationId,
            UserId = response.User?.Id,
            StoreId = response.StoreResponse.StoreId,
        };
    }
}
