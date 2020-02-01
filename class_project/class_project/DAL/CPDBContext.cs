using class_project.Models;
namespace class_project.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CPDBContext : DbContext
    {
        public CPDBContext()
            : base("name=CPDBContext")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Meet> Meets { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.Teams)
                .WithMany(e => e.Athletes)
                .Map(m => m.ToTable("TeamMember").MapLeftKey("AthleteID").MapRightKey("TeamID"));

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Records)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);
        }
    }
}
