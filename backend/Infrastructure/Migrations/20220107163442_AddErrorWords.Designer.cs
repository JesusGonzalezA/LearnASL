﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220107163442_AddErrorWords")]
    partial class AddErrorWords
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.DatasetItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("VideoFilename")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Dataset");
                });

            modelBuilder.Entity("Core.Entities.ErrorWordEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DatasetItemEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DatasetItemEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("ErrorWords");
                });

            modelBuilder.Entity("Core.Entities.LearntWordEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DatasetItemEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DatasetItemEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("LearntWords");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionMimicEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DatasetItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VideoHelp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WordToGuess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DatasetItemId");

                    b.HasIndex("TestId");

                    b.ToTable("QuestionsMimic");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionOptionVideoToWordEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DatasetItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PossibleAnswer0")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PossibleAnswer1")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PossibleAnswer2")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PossibleAnswer3")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoToGuess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DatasetItemId");

                    b.HasIndex("TestId");

                    b.ToTable("QuestionsOptionVideoToWord");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionOptionWordToVideoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DatasetItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PossibleAnswer0")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PossibleAnswer1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PossibleAnswer2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PossibleAnswer3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WordToGuess")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("DatasetItemId");

                    b.HasIndex("TestId");

                    b.ToTable("QuestionsOptionWordToVideo");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionQAEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DatasetItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VideoUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WordToGuess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DatasetItemId");

                    b.HasIndex("TestId");

                    b.ToTable("QuestionsQA");
                });

            modelBuilder.Entity("Core.Entities.Tests.TestEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("TestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Core.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ConfirmedEmail")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TokenEmailConfirmation")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("TokenPasswordRecovery")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Entities.ErrorWordEntity", b =>
                {
                    b.HasOne("Core.Entities.DatasetItemEntity", "DatasetItem")
                        .WithMany()
                        .HasForeignKey("DatasetItemEntityId")
                        .HasConstraintName("FK_ErrorWord_DatasetItem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ErrorWord_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.LearntWordEntity", b =>
                {
                    b.HasOne("Core.Entities.DatasetItemEntity", "DatasetItem")
                        .WithMany()
                        .HasForeignKey("DatasetItemEntityId")
                        .HasConstraintName("FK_LearntWord_DatasetItem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_LearntWord_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionMimicEntity", b =>
                {
                    b.HasOne("Core.Entities.DatasetItemEntity", "DatasetItem")
                        .WithMany()
                        .HasForeignKey("DatasetItemId")
                        .HasConstraintName("FK_Question_DatasetItem_QuestionMimicConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Tests.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .HasConstraintName("FK_Question_Test_QuestionMimicConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetItem");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionOptionVideoToWordEntity", b =>
                {
                    b.HasOne("Core.Entities.DatasetItemEntity", "DatasetItem")
                        .WithMany()
                        .HasForeignKey("DatasetItemId")
                        .HasConstraintName("FK_Question_DatasetItem_QuestionOptionVideoToWordConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Tests.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .HasConstraintName("FK_Question_Test_QuestionOptionVideoToWordConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetItem");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionOptionWordToVideoEntity", b =>
                {
                    b.HasOne("Core.Entities.DatasetItemEntity", "DatasetItem")
                        .WithMany()
                        .HasForeignKey("DatasetItemId")
                        .HasConstraintName("FK_Question_DatasetItem_QuestionOptionWordToVideoConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Tests.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .HasConstraintName("FK_Question_Test_QuestionOptionWordToVideoConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetItem");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Entities.Tests.QuestionQAEntity", b =>
                {
                    b.HasOne("Core.Entities.DatasetItemEntity", "DatasetItem")
                        .WithMany()
                        .HasForeignKey("DatasetItemId")
                        .HasConstraintName("FK_Question_DatasetItem_QuestionQaConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Tests.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .HasConstraintName("FK_Question_Test_QuestionQaConfiguration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetItem");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Entities.Tests.TestEntity", b =>
                {
                    b.HasOne("Core.Entities.UserEntity", "User")
                        .WithMany("Tests")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Test_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.UserEntity", b =>
                {
                    b.Navigation("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}