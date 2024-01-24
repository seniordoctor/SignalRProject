using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.Concreate
{
    public class SignalRContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\doctor;initial Catalog=SignalRDb;integrated Security=true");
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=SignalRDb;Persist Security Info=False;User ID=sa;Password=M@f911o8u8xQwN50Pb3i8K");
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
