using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.WhiteLabeling.Core.Models
{
    public class WhiteLabelingSettingSearchCriteria : SearchCriteriaBase
    {
        public bool? IsEnabled { get; set; }
        public string OrganizationId { get; set; }
        public string StoreId { get; set; }
    }
}
