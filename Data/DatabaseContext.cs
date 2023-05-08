using Microsoft.EntityFrameworkCore;
using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {

        }
        public DbSet<AIPrompt> AIPrompts { get; set; }
        public DbSet<MealOfDay> MealOfDays { get; set; }
        public DbSet<MyPrompt> MyPrompts { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.MyPrompts)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

            modelBuilder.Entity<User>()
            .HasMany(u => u.AIPrompts)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

            modelBuilder.Entity<MyPrompt>()
            .HasMany(u => u.AIPrompts)
            .WithOne(t => t.MyPrompt)
            .HasForeignKey(t => t.MyPromptId)
            .OnDelete(DeleteBehavior.Restrict) // uygun olan hangisi bir araştır Restrict vs Cascade 
            .IsRequired(false);
        }
    }
}
