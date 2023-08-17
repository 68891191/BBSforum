using System;
using Forum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace forum.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    partial class ForumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3");

            modelBuilder.Entity("Forum.Models.Comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<DateTime>("createdAt")
                        .HasColumnType("TEXT");
                    b.Property<int>("postId")
                        .HasColumnType("INTEGER");
                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");
                    b.HasKey("id");
                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Forum.Models.Message", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<int>("receiverId")
                        .HasColumnType("INTEGER");
                    b.Property<int>("senderId")
                        .HasColumnType("INTEGER");
                    b.HasKey("id");
                    b.ToTable("Message");
                });

            modelBuilder.Entity("Forum.Models.Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<DateTime>("createdAt")
                        .HasColumnType("TEXT");
                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");
                    b.HasKey("id");
                    b.ToTable("Post");
                });

            modelBuilder.Entity("Forum.Models.Tag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.HasKey("id");
                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Forum.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");
                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.HasKey("id");
                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}