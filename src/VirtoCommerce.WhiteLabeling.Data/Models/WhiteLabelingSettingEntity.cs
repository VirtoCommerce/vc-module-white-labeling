using System.ComponentModel.DataAnnotations;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;
using VirtoCommerce.WhiteLabeling.Core.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Models
{
    public class WhiteLabelingSettingEntity : AuditableEntity, IDataEntity<WhiteLabelingSettingEntity, WhiteLabelingSetting>
    {
        public bool IsEnabled { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string OrganizationId { get; set; }

        [StringLength(1024)]
        public string LogoUrl { get; set; }

        [StringLength(1024)]
        public string SecondaryLogoUrl { get; set; }

        [StringLength(1024)]
        public string FaviconUrl { get; set; }

        [StringLength(256)]
        public string FooterLinkListName { get; set; }

        [StringLength(256)]
        public string ThemePresetName { get; set; }

        [StringLength(128)]
        public string StoreId { get; set; }

        public WhiteLabelingSetting ToModel(WhiteLabelingSetting model)
        {
            model.Id = Id;
            model.CreatedBy = CreatedBy;
            model.CreatedDate = CreatedDate;
            model.ModifiedBy = ModifiedBy;
            model.ModifiedDate = ModifiedDate;

            model.IsEnabled = IsEnabled;
            model.UserId = UserId;
            model.OrganizationId = OrganizationId;
            model.LogoUrl = LogoUrl;
            model.SecondaryLogoUrl = SecondaryLogoUrl;
            model.FaviconUrl = FaviconUrl;
            model.FooterLinkListName = FooterLinkListName;
            model.ThemePresetName = ThemePresetName;
            model.StoreId = StoreId;

            return model;
        }

        public WhiteLabelingSettingEntity FromModel(WhiteLabelingSetting model, PrimaryKeyResolvingMap pkMap)
        {
            pkMap.AddPair(model, this);

            Id = model.Id;
            CreatedBy = model.CreatedBy;
            CreatedDate = model.CreatedDate;
            ModifiedBy = model.ModifiedBy;
            ModifiedDate = model.ModifiedDate;

            IsEnabled = model.IsEnabled;
            UserId = model.UserId;
            OrganizationId = model.OrganizationId;
            LogoUrl = model.LogoUrl;
            SecondaryLogoUrl = model.SecondaryLogoUrl;
            FaviconUrl = model.FaviconUrl;
            FooterLinkListName = model.FooterLinkListName;
            ThemePresetName = model.ThemePresetName;
            StoreId = model.StoreId;

            return this;
        }

        public void Patch(WhiteLabelingSettingEntity target)
        {
            target.IsEnabled = IsEnabled;
            target.UserId = UserId;
            target.OrganizationId = OrganizationId;
            target.LogoUrl = LogoUrl;
            target.SecondaryLogoUrl = SecondaryLogoUrl;
            target.FaviconUrl = FaviconUrl;
            target.FooterLinkListName = FooterLinkListName;
            target.ThemePresetName = ThemePresetName;
            target.StoreId = StoreId;
        }
    }
}
