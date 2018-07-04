namespace SimpleMvc.Data
{
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.DataModels;

    public class ExamDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
               .UseSqlServer(@"Server=.;Database=MeTube;Integrated Security=True;");

            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>() //  To be Unique Username
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<User>()   //  To be Unique Email
             .HasIndex(u => u.Email)
             .IsUnique();



            //builder.Entity<User>()
            //    .HasMany(t => t.Tubes)
            //    .WithOne(u => u.Uploader)
            //    .HasForeignKey(u => u.UploaderId);


            base.OnModelCreating(builder); // whis is why ? May be to take the base configuration if is necessary
        }
    }
}
