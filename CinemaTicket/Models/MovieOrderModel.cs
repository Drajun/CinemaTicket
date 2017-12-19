namespace CinemaTicket.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MovieOrderModel : DbContext
    {
        public MovieOrderModel()
            : base("name=MovieOrderModel")
        {
        }

        public virtual DbSet<OrderModels> OrderModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModels>()
                .Property(e => e.totalPrice)
                .HasPrecision(3, 0);
        }
    }
}
