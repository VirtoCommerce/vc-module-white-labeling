using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;
using VirtoCommerce.WhiteLabeling.Core.Events;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.Data.Models;
using VirtoCommerce.WhiteLabeling.Data.Repositories;

namespace VirtoCommerce.WhiteLabeling.Data.Services
{
    public class WhiteLabelingSettingService : CrudService<WhiteLabelingSetting, WhiteLabelingSettingEntity, WhiteLabelingSettingChangingEvent, WhiteLabelingSettingChangedEvent>, IWhiteLabelingSettingService
    {
        public WhiteLabelingSettingService(
            Func<IWhiteLabelingRepository> repositoryFactory,
            IPlatformMemoryCache platformMemoryCache,
            IEventPublisher eventPublisher)
            : base(repositoryFactory, platformMemoryCache, eventPublisher)
        {
        }

        protected override Task<IList<WhiteLabelingSettingEntity>> LoadEntities(IRepository repository, IList<string> ids, string responseGroup)
        {
            return ((IWhiteLabelingRepository)repository).GetWhiteLabelingSettingsByIdsAsync(ids, responseGroup);
        }
    }
}
