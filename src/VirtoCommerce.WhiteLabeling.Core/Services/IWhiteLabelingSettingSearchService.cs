using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Core.Services
{
    public interface IWhiteLabelingSettingSearchService : ISearchService<WhiteLabelingSettingSearchCriteria, WhiteLabelingSettingSearchResult, WhiteLabelingSetting>
    {
    }
}
