﻿using System;
using Core.Entities;
using Core.Entities.Tests;
using Infraestructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infraestructure.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<TestOptionWordToVideoEntity> TestsOptionWordToVideo { get; set; }
        public virtual DbSet<QuestionOptionWordToVideoEntity> QuestionsOptionWordToVideo { get; set; }

        public virtual DbSet<TestOptionVideoToWordEntity> TestsOptionVideoToWord { get; set; }
        public virtual DbSet<QuestionOptionVideoToWordEntity> QuestionsOptionVideoToWord { get; set; }

        public DatabaseContext()
        {}
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new TestOptionWordToVideoConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionWordToVideoConfiguration());

            modelBuilder.ApplyConfiguration(new TestOptionVideoToWordConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionVideoToWordConfiguration());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        private void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (!e.FromQuery && e.Entry.State == EntityState.Added && e.Entry.Entity is Core.Entities.BaseEntity entity)
            {
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        private void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified && e.Entry.Entity is Core.Entities.BaseEntity entity)
            {
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
