using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.ExperienceApiModule.Core.BaseQueries;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Data.Schemas;

namespace VirtoCommerce.WhiteLabeling.Data.Queries
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
