namespace Online.Travel.Management.System.API.Entities
{
    using Microsoft.EntityFrameworkCore;

    ///<Summary>
    /// Booking Db Context
    ///</Summary>
    public class BookingDbContext : DbContext
    {
        ///<Summary>
        /// BookingDbContext constructor
        ///</Summary>
        public BookingDbContext() { }

        ///<Summary>
        /// BookingDbContext constructor with DbContextOptions
        ///</Summary>
        public BookingDbContext(DbContextOptions<BookingDbContext> contextOptions) : base(contextOptions)
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
