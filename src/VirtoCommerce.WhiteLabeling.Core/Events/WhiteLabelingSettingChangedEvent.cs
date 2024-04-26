using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Core.Events
{
    public class WhiteLabelingSettingChangedEvent : GenericChangedEntryEvent<WhiteLabelingSetting>
    {
        public WhiteLabelingSettingChangedEvent(IEnumerable<GenericChangedEntry<WhiteLabelingSetting>> changedEntries) : base(changedEntries)
        {
        }
    }
}
