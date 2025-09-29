namespace WeebBook.Domain.Entities
{
    public class Helper
    {
        public enum Roles
        {
            SuperAdmin,
            Admin,
            Basic
        }

        //Date Default user
        public const string Email = "superadmin@domain.com";
        public const string Password = "SuperAdmin@123*";
        public const string UserName = "superadmin@domain.com";
        public const string Name = "SuperAdmin";

        public const string EmailBasic = "basicuser@domain.com";
        public const string PasswordBasic = "basicuser@123*";
        public const string UserNameBasic = "basicuser@domain.com";
        public const string NameBasic = "BasicUser";
    }
}
