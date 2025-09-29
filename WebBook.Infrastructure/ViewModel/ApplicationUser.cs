using Microsoft.AspNet.Identity.EntityFramework;

namespace WebBook.Infrastructure.ViewModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public bool ActiveUser { get; set; }

    }
}
