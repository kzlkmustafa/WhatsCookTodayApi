﻿using Microsoft.EntityFrameworkCore;
using WhatsCookTodayApi.MyModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace WhatsCookTodayApi.Data
{
    public class DatabaseContext : IdentityDbContext<MyUsers>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {

        }
        public DbSet<AIPrompt> AIPrompts { get; set; }
        public DbSet<MealOfDay> MealOfDays { get; set; }
        public DbSet<MyPrompt> MyPrompts { get; set; }
        public DbSet<MyUsers> Users { get; set; }
        public DbSet<MyUserTokens> MyUserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyUsers>()
            .HasMany(u => u.MyPrompts)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.Id).IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

            modelBuilder.Entity<MyUsers>()
            .HasMany(u => u.AIPrompts)
            .WithOne(t => t.User)  
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

            modelBuilder.Entity<MyPrompt>()
            .HasMany(u => u.AIPrompts)
            .WithOne(t => t.MyPrompt)
            .HasForeignKey(t => t.MyPromptId)
            .OnDelete(DeleteBehavior.Restrict) // uygun olan hangisi bir araştır Restrict vs Cascade 
            .IsRequired(false);

            modelBuilder.Entity<MyPrompt>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<MyPrompt>()
            .HasOne(t => t.User)
            .WithMany(u => u.MyPrompts)
            .IsRequired(false);

        }
    }
}
