using static WeebBook.Domain.Entities.Helper;

namespace WeebBook.Domain.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static List<string> PermissionsList()
        {
            var allpermissions = new List<string>();
            foreach (var module in Enum.GetValues(typeof(PermissionModuleName)))
            {
                allpermissions.AddRange(GeneratePermissionsForModule(module.ToString()));
            }
            return allpermissions;
        }
    }
}
