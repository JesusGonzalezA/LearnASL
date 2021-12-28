using System;
using System.IO;
using Core.Entities;
using Core.Entities.Tests;
using Core.Options;
using Infraestructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Infraestructure.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly VideoServingOptions _videoServingOptions;

        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<TestEntity> Tests { get; set; }
        public virtual DbSet<QuestionOptionVideoToWordEntity> QuestionsOptionVideoToWord { get; set; }
        public virtual DbSet<QuestionOptionWordToVideoEntity> QuestionsOptionWordToVideo { get; set; }
        public virtual DbSet<QuestionQAEntity> QuestionsQA { get; set; }
        public virtual DbSet<QuestionMimicEntity> QuestionsMimic { get; set; }

        public DatabaseContext()
        {}
        
        public DatabaseContext
        (
            DbContextOptions<DatabaseContext> options,
            IOptions<VideoServingOptions> videoServingOptions
        )
            : base(options)
        {
            _videoServingOptions = videoServingOptions.Value;

            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

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

            if (e.NewState == EntityState.Deleted && e.Entry.Entity is UserEntity userEntity)
            {
                DirectoryInfo root = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
                string userVideosDirectoryPath = Path.Combine(root.FullName, _videoServingOptions.Directory, userEntity.Id.ToString());
                Directory.Delete(userVideosDirectoryPath, true);
            }

            if (e.NewState == EntityState.Deleted && e.Entry.Entity is TestEntity testEntity)
            {
                DirectoryInfo root = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
                string userVideosDirectoryPath = Path.Combine(root.FullName, _videoServingOptions.Directory, testEntity.UserId.ToString());

                if (Directory.Exists(userVideosDirectoryPath) )
                {
                    string testVideosDirectoryPath = Path.Combine(userVideosDirectoryPath, testEntity.Id.ToString());
                    Directory.Delete(testVideosDirectoryPath, true);
                }
                
            }
        }

        private void DeleteDirectory(string folderName)
        {

        }
    }
}
