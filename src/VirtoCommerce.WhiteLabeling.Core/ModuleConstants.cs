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
}
