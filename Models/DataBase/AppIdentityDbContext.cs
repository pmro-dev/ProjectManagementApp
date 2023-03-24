using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TODO_List_ASPNET_MVC.Models.ViewModels
{
	/// <summary>
	/// Context class that implements IdentityDbContext.
	/// </summary>
	public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) 
        { 
        }
    }
}
