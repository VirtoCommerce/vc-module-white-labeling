using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Core.Domain;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.WhiteLabeling.Data.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Repositories
{
    public class WhiteLabelingRepository : DbContextRepositoryBase<WhiteLabelingDbContext>, IWhiteLabelingRepository
    {
        public WhiteLabelingRepository(WhiteLabelingDbContext dbContext, IUnitOfWork unitOfWork = null) : base(dbContext, unitOfWork)
        {
        }

        public IQueryable<WhiteLabelingSettingEntity> WhiteLabelingSettings => DbContext.Set<WhiteLabelingSettingEntity>();

        public async Task<IList<WhiteLabelingSettingEntity>> GetWhiteLabelingSettingsByIdsAsync(IList<string> ids, string responseGroup)
        {
            return await WhiteLabelingSettings.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
