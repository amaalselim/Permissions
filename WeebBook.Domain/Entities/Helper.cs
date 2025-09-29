namespace WeebBook.Domain.Entities
{
    public class Helper
    {

        //Date Default user
        public const string Email = "superadmin@domain.com";
        public const string Password = "SuperAdmin@123*";
        public const string UserName = "superadmin@domain.com";
        public const string Name = "SuperAdmin";

        public const string EmailBasic = "basicuser@domain.com";
        public const string PasswordBasic = "basicuser@123*";
        public const string UserNameBasic = "basicuser@domain.com";
        public const string NameBasic = "BasicUser";
        public enum Roles
        {
            SuperAdmin,
            Admin,
            Basic
        }

        public enum PermissionModuleName
        {
            Home,
            Accounts,
            Roles,
            Registers,
            Categories
        }


    }
}
