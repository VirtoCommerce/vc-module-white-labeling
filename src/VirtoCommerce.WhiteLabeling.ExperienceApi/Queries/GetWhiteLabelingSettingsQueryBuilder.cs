using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;
using VirtoCommerce.Xapi.Core.BaseQueries;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQueryBuilder : QueryBuilder<GetWhiteLabelingSettingsQuery, ExpWhiteLabelingSetting, WhiteLabelingSettingsType>
    {
        public GetWhiteLabelingSettingsQueryBuilder(IMediator mediator, IAuthorizationService authorizationService)
            : base(mediator, authorizationService)
        {
        }

        protected override string Name => "whiteLabelingSettings";
    }
}
