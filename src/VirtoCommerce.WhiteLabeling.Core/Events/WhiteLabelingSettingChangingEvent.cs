using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Core.Events
{
    public class WhiteLabelingSettingChangingEvent : GenericChangedEntryEvent<WhiteLabelingSetting>
    {
        public WhiteLabelingSettingChangingEvent(IEnumerable<GenericChangedEntry<WhiteLabelingSetting>> changedEntries) : base(changedEntries)
        {
        }
    }
}
