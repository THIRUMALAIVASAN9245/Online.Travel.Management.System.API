namespace Online.Travel.AuthService.API.Entities
{
    using Microsoft.EntityFrameworkCore;

    ///<Summary>
    /// Booking Db Context
    ///</Summary>
    public class AuthServiceDbContext : DbContext
    {
        ///<Summary>
        /// BookingDbContext constructor
        ///</Summary>
        public AuthServiceDbContext() { }

        ///<Summary>
        /// BookingDbContext constructor with DbContextOptions
        ///</Summary>
        public AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> contextOptions) : base(contextOptions)
        {
            //Database.EnsureCreated();
        }

        ///<Summary>
        /// UserDetail Entitiy
        ///</Summary>
        public DbSet<UserDetail> UserInfo { get; set; }

        ///<Summary>
        /// Booking Entitiy
        ///</Summary>
        public DbSet<Booking> BookingInfo { get; set; }
    }
}
