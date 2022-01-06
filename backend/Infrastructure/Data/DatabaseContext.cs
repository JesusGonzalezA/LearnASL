using System;
using Core.Entities;
using Core.Entities.Tests;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<DatasetItemEntity> Dataset { get; set; }

        public virtual DbSet<LearntWordEntity> LearntWords { get; set; }

        public virtual DbSet<TestEntity> Tests { get; set; }
        public virtual DbSet<QuestionOptionVideoToWordEntity> QuestionsOptionVideoToWord { get; set; }
        public virtual DbSet<QuestionOptionWordToVideoEntity> QuestionsOptionWordToVideo { get; set; }
        public virtual DbSet<QuestionQAEntity> QuestionsQA { get; set; }
        public virtual DbSet<QuestionMimicEntity> QuestionsMimic { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)

            : base(options)
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new DatasetItemConfiguration());

            modelBuilder.ApplyConfiguration(new LearntWordConfiguration());

            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionVideoToWordConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionWordToVideoConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionMimicConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionQaConfiguration());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        private void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (!e.FromQuery && e.Entry.State == EntityState.Added && e.Entry.Entity is BaseEntity entity)
            {
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        private void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified && e.Entry.Entity is BaseEntity entity)
            {
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
