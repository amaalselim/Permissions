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
    }
}
