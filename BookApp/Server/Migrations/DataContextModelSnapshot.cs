﻿// <auto-generated />
using System;
using BookApp.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookApp.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AnalysisNoteTag", b =>
                {
                    b.Property<int>("AnalysisNotesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("AnalysisNotesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("AnalysisNoteTag");
                });

            modelBuilder.Entity("BookApp.Server.Models.AnalysisVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnalysisNotesVersion")
                        .HasColumnType("int");

                    b.Property<int>("AnalysisSummaryVersion")
                        .HasColumnType("int");

                    b.Property<int>("ChapterNotesVersion")
                        .HasColumnType("int");

                    b.Property<int>("HighlightNotesVersion")
                        .HasColumnType("int");

                    b.Property<int>("HighlightVersion")
                        .HasColumnType("int");

                    b.Property<int>("ParagraphNotesVersion")
                        .HasColumnType("int");

                    b.Property<int>("TagsVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AnalysisVersions");
                });

            modelBuilder.Entity("BookApp.Server.Models.BookAnalysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AnalysisTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("AnalysisVersionId")
                        .HasColumnType("int");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("BookHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AnalysisVersionId")
                        .IsUnique();

                    b.ToTable("BookAnalyses");
                });

            modelBuilder.Entity("BookApp.Server.Models.BookAnalysisUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookAnalysisId")
                        .HasColumnType("int");

                    b.Property<int>("MemberType")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookAnalysisId");

                    b.HasIndex("UsersId");

                    b.ToTable("BookAnalysisUser");
                });

            modelBuilder.Entity("BookApp.Server.Models.Highlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookAnalysisId")
                        .HasColumnType("int");

                    b.Property<int>("Chapter")
                        .HasColumnType("int");

                    b.Property<int>("FirstNodeCharIndex")
                        .HasColumnType("int");

                    b.Property<int>("FirstNodeIndex")
                        .HasColumnType("int");

                    b.Property<int>("LastNodeCharIndex")
                        .HasColumnType("int");

                    b.Property<int>("LastNodeIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookAnalysisId");

                    b.ToTable("Highlights");
                });

            modelBuilder.Entity("BookApp.Server.Models.Identity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "d59d61de-7986-4711-9b1f-ccd6c548c88c",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "c72d95d2-0b03-46a6-82f4-d253895630d6",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("BookApp.Server.Models.Identity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "becac254-7ccd-4950-b970-96a102ef4dc2",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "PlaceholderAdmin"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b5cff2c4-64bd-4bf1-bd1b-156d496f4bd1",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "PlaceholderUser"
                        });
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.AnalysisNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookAnalysisId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookAnalysisId");

                    b.ToTable("AnalysisNotes");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.ChapterNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookAnalysisId")
                        .HasColumnType("int");

                    b.Property<int>("Chapter")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookAnalysisId");

                    b.ToTable("ChapterNotes");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.HighlightNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HighlightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HighlightId");

                    b.ToTable("HighlightNotes");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.ParagraphNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookAnalysisId")
                        .HasColumnType("int");

                    b.Property<int>("Chapter")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TextNodeNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookAnalysisId");

                    b.ToTable("ParagraphNotes");
                });

            modelBuilder.Entity("BookApp.Server.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookAnalysisId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookAnalysisId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ChapterNoteTag", b =>
                {
                    b.Property<int>("ChapterNotesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ChapterNotesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ChapterNoteTag");
                });

            modelBuilder.Entity("HighlightNoteTags", b =>
                {
                    b.Property<int>("HighlightNotesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("HighlightNotesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("HighlightNoteTags");
                });

            modelBuilder.Entity("HighlightTag", b =>
                {
                    b.Property<int>("HighlightsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("HighlightsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("HighlightTag");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ParagraphNoteTag", b =>
                {
                    b.Property<int>("ParagraphNotesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ParagraphNotesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ParagraphNoteTag");
                });

            modelBuilder.Entity("AnalysisNoteTag", b =>
                {
                    b.HasOne("BookApp.Server.Models.Notes.AnalysisNote", null)
                        .WithMany()
                        .HasForeignKey("AnalysisNotesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("BookApp.Server.Models.BookAnalysis", b =>
                {
                    b.HasOne("BookApp.Server.Models.AnalysisVersion", "AnalysisVersion")
                        .WithOne("BookAnalysis")
                        .HasForeignKey("BookApp.Server.Models.BookAnalysis", "AnalysisVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnalysisVersion");
                });

            modelBuilder.Entity("BookApp.Server.Models.BookAnalysisUser", b =>
                {
                    b.HasOne("BookApp.Server.Models.BookAnalysis", "BookAnalysis")
                        .WithMany()
                        .HasForeignKey("BookAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("BookAnalysis");
                });

            modelBuilder.Entity("BookApp.Server.Models.Highlight", b =>
                {
                    b.HasOne("BookApp.Server.Models.BookAnalysis", "BookAnalysis")
                        .WithMany("Highlights")
                        .HasForeignKey("BookAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BookAnalysis");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.AnalysisNote", b =>
                {
                    b.HasOne("BookApp.Server.Models.BookAnalysis", "BookAnalysis")
                        .WithMany("AnalysisNotes")
                        .HasForeignKey("BookAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BookAnalysis");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.ChapterNote", b =>
                {
                    b.HasOne("BookApp.Server.Models.BookAnalysis", "BookAnalysis")
                        .WithMany("ChapterNotes")
                        .HasForeignKey("BookAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BookAnalysis");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.HighlightNote", b =>
                {
                    b.HasOne("BookApp.Server.Models.Highlight", "Highlight")
                        .WithMany("HighlightNotes")
                        .HasForeignKey("HighlightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Highlight");
                });

            modelBuilder.Entity("BookApp.Server.Models.Notes.ParagraphNote", b =>
                {
                    b.HasOne("BookApp.Server.Models.BookAnalysis", "BookAnalysis")
                        .WithMany("ParagraphNotes")
                        .HasForeignKey("BookAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BookAnalysis");
                });

            modelBuilder.Entity("BookApp.Server.Models.Tag", b =>
                {
                    b.HasOne("BookApp.Server.Models.BookAnalysis", "BookAnalysis")
                        .WithMany("Tags")
                        .HasForeignKey("BookAnalysisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BookAnalysis");
                });

            modelBuilder.Entity("ChapterNoteTag", b =>
                {
                    b.HasOne("BookApp.Server.Models.Notes.ChapterNote", null)
                        .WithMany()
                        .HasForeignKey("ChapterNotesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("HighlightNoteTags", b =>
                {
                    b.HasOne("BookApp.Server.Models.Notes.HighlightNote", null)
                        .WithMany()
                        .HasForeignKey("HighlightNotesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("HighlightTag", b =>
                {
                    b.HasOne("BookApp.Server.Models.Highlight", null)
                        .WithMany()
                        .HasForeignKey("HighlightsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("BookApp.Server.Models.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("BookApp.Server.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("BookApp.Server.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("BookApp.Server.Models.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("BookApp.Server.Models.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParagraphNoteTag", b =>
                {
                    b.HasOne("BookApp.Server.Models.Notes.ParagraphNote", null)
                        .WithMany()
                        .HasForeignKey("ParagraphNotesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookApp.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("BookApp.Server.Models.AnalysisVersion", b =>
                {
                    b.Navigation("BookAnalysis")
                        .IsRequired();
                });

            modelBuilder.Entity("BookApp.Server.Models.BookAnalysis", b =>
                {
                    b.Navigation("AnalysisNotes");

                    b.Navigation("ChapterNotes");

                    b.Navigation("Highlights");

                    b.Navigation("ParagraphNotes");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("BookApp.Server.Models.Highlight", b =>
                {
                    b.Navigation("HighlightNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
