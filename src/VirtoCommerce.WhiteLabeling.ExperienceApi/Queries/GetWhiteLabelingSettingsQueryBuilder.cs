using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.ExperienceApiModule.Core.BaseQueries;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Queries
{
    public class GetWhiteLabelingSettingsQueryBuilder : QueryBuilder<GetWhiteLabelingSettingsQuery, WhiteLabelingSettings, WhiteLabelingSettingsType>
    {
        public GetWhiteLabelingSettingsQueryBuilder(IMediator mediator, IAuthorizationService authorizationService)
            : base(mediator, authorizationService)
        {
        }

        protected override string Name => "whiteLabelingSettings";
    }
}
