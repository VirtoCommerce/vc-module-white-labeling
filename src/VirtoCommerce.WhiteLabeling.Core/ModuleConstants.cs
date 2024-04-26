using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.WhiteLabeling.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Create = "WhiteLabeling:create";
            public const string Read = "WhiteLabeling:read";
            public const string Update = "WhiteLabeling:update";
            public const string Delete = "WhiteLabeling:delete";

            public static string[] AllPermissions { get; } =
            {
                Create,
                Read,
                Update,
                Delete,
            };
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor WhiteLabelingEnabled { get; } = new()
            {
                Name = "WhiteLabeling.WhiteLabelingEnabled",
                GroupName = "WhiteLabeling|General",
                ValueType = SettingValueType.Boolean,
                IsPublic = true,
                DefaultValue = true,
            };

            public static SettingDescriptor ThemePresetNames { get; } = new()
            {
                Name = "WhiteLabeling.ThemePresetNames",
                ValueType = SettingValueType.ShortText,
                GroupName = "WhiteLabeling|General",
                IsDictionary = true,
                DefaultValue = "Default",
                AllowedValues = ["Default"]
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return WhiteLabelingEnabled;
                    yield return ThemePresetNames;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }

        public static IEnumerable<SettingDescriptor> StoreLevelSettings
        {
            get
            {
                yield return General.WhiteLabelingEnabled;
            }
        }
    }
}
