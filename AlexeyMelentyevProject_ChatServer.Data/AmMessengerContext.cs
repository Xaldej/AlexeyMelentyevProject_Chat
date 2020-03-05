using AlexeyMelentyevProject_ChatServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data
{
    public class AmMessengerContext : DbContext
    {
        public AmMessengerContext() : base("AmMessenger")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AmMessengerContext>());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ContactRelationship> ContactRelationships { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.ContactRelationships)
        //        .WithMany()
        //        .Map(m =>
        //            {
        //                m.MapLeftKey("UserId");
        //                m.MapRightKey("ContactRelationshipId");
        //                m.ToTable("ContactRelationships");
        //            });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
