using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Data.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Repositories
{
    public interface IWhiteLabelingRepository : IRepository
    {
        public IQueryable<WhiteLabelingSettingEntity> WhiteLabelingSettings { get; }

        public Task<IList<WhiteLabelingSettingEntity>> GetWhiteLabelingSettingsByIdsAsync(IList<string> ids, string responseGroup);
    }
}
