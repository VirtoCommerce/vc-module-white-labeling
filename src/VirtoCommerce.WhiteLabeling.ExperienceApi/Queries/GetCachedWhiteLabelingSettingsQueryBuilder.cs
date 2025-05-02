using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;
using VirtoCommerce.Xapi.Core.BaseQueries;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetCachedWhiteLabelingSettingsQueryBuilder : QueryBuilder<GetCachedWhiteLabelingSettingsQuery, ExpWhiteLabelingSetting, WhiteLabelingSettingsType>
    {
        public GetCachedWhiteLabelingSettingsQueryBuilder(IMediator mediator, IAuthorizationService authorizationService)
            : base(mediator, authorizationService)
        {
        }

        protected override string Name => "cachedWhiteLabelingSettings";
    }
}
