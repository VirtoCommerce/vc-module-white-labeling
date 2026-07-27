using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;
using VirtoCommerce.Xapi.Core.BaseQueries;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQueryBuilder : QueryBuilder<GetWhiteLabelingSettingsQuery, ExpWhiteLabelingSetting, WhiteLabelingSettingsType>
    {
        public GetWhiteLabelingSettingsQueryBuilder(IAuthorizationService authorizationService)
            : base(authorizationService)
        {
        }

        [Obsolete("Use the constructor without IMediator. The mediator is resolved from context.RequestServices per request.", DiagnosticId = "VC0015", UrlFormat = "https://docs.virtocommerce.org/products/products-virto3-versions")]
        public GetWhiteLabelingSettingsQueryBuilder(IMediator mediator, IAuthorizationService authorizationService)
            : this(authorizationService)
        {
        }

        protected override string Name => "whiteLabelingSettings";
    }
}
