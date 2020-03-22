﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OtomatikMuhendis.Kutuphane.Web.Core.Enums;
using OtomatikMuhendis.Kutuphane.Web.Data;
using System;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.BookAuthor", b =>
                {
                    b.Property<int>("BookDetailId");

                    b.Property<int>("AuthorId");

                    b.HasKey("BookDetailId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.BookDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("GoogleBookId");

                    b.Property<string>("ImageLink");

                    b.Property<string>("Isbn10");

                    b.Property<string>("Isbn13");

                    b.Property<int?>("PageCount");

                    b.Property<short?>("PublishedYear");

                    b.Property<string>("Publisher");

                    b.Property<string>("Subtitle")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("BookDetails");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Following", b =>
                {
                    b.Property<string>("FollowerId");

                    b.Property<string>("FolloweeId");

                    b.HasKey("FollowerId", "FolloweeId");

                    b.HasIndex("FolloweeId");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ShelfId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ShelfId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.ItemBookDetail", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int>("BookDetailId");

                    b.HasKey("ItemId", "BookDetailId");

                    b.HasIndex("BookDetailId");

                    b.ToTable("ItemBookDetails");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("DateTime");

                    b.Property<int?>("ItemId");

                    b.Property<int>("ShelfId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ItemId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Star", b =>
                {
                    b.Property<int>("ShelfId");

                    b.Property<string>("UserId");

                    b.HasKey("ShelfId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.UserNotification", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("NotificationId");

                    b.Property<bool>("IsRead");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.BookAuthor", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.BookDetail", "BookDetail")
                        .WithMany("BookAuthorList")
                        .HasForeignKey("BookDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Following", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "Followee")
                        .WithMany("Followees")
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Item", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Shelf", "Shelf")
                        .WithMany("Items")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.ItemBookDetail", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.BookDetail", "BookDetail")
                        .WithMany()
                        .HasForeignKey("BookDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Notification", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Shelf", "Shelf")
                        .WithMany()
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Shelf", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.Star", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Shelf", "Shelf")
                        .WithMany("Stars")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OtomatikMuhendis.Kutuphane.Web.Core.Models.UserNotification", b =>
                {
                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OtomatikMuhendis.Kutuphane.Web.Core.Models.ApplicationUser", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
