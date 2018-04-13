using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProEventApp.Models
{
    

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<EventComment> EventComments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<AppEvent> Events { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<EventProfessional> EventProfessionals { get; set; }
        public DbSet<InvitationStatus> InvitationStatuses { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Address> Addresses { get; set; }


        

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}