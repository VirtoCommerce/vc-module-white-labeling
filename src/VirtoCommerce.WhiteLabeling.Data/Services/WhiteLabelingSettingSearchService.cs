using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Data.GenericCrud;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.Data.Models;
using VirtoCommerce.WhiteLabeling.Data.Repositories;

namespace VirtoCommerce.WhiteLabeling.Data.Services
{
    public class WhiteLabelingSettingSearchService : SearchService<WhiteLabelingSettingSearchCriteria, WhiteLabelingSettingSearchResult, WhiteLabelingSetting, WhiteLabelingSettingEntity>, IWhiteLabelingSettingSearchService
    {
        public WhiteLabelingSettingSearchService(Func<IWhiteLabelingRepository> repositoryFactory, IPlatformMemoryCache platformMemoryCache, IWhiteLabelingSettingService crudService, IOptions<CrudOptions> crudOptions)
            : base(repositoryFactory, platformMemoryCache, crudService, crudOptions)
        {
        }

        protected override IQueryable<WhiteLabelingSettingEntity> BuildQuery(IRepository repository, WhiteLabelingSettingSearchCriteria criteria)
        {
            var query = ((IWhiteLabelingRepository)repository).WhiteLabelingSettings;

            if (criteria.IsEnabled.HasValue)
            {
                query = query.Where(x => x.IsEnabled == criteria.IsEnabled.Value);
            }

            if (!string.IsNullOrEmpty(criteria.OrganizationId))
            {
                query = query.Where(x => x.OrganizationId == criteria.OrganizationId);
            }

            return query;
        }

        protected override IList<SortInfo> BuildSortExpression(WhiteLabelingSettingSearchCriteria criteria)
        {
            var sortInfos = criteria.SortInfos;

            if (sortInfos.IsNullOrEmpty())
            {
                sortInfos =
                [
                    new SortInfo { SortColumn = nameof(WhiteLabelingSetting.CreatedDate), SortDirection = SortDirection.Descending },
                    new SortInfo { SortColumn = nameof(WhiteLabelingSetting.Id) },
                ];
            }

            return sortInfos;
        }
    }
}
