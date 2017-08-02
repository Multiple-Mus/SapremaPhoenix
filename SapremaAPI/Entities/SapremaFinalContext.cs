using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SapremaAPI.Entities
{
    public partial class SapremaFinalContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<SapClass> SapClass { get; set; }
        public virtual DbSet<SapClassComplete> SapClassComplete { get; set; }
        public virtual DbSet<SapClassPoses> SapClassPoses { get; set; }
        public virtual DbSet<SapFlagClasses> SapFlagClasses { get; set; }
        public virtual DbSet<SapFlagMeditations> SapFlagMeditations { get; set; }
        public virtual DbSet<SapGroups> SapGroups { get; set; }
        public virtual DbSet<SapMeditations> SapMeditations { get; set; }
        public virtual DbSet<SapPoses> SapPoses { get; set; }
        public virtual DbSet<SapReviewClass> SapReviewClass { get; set; }
        public virtual DbSet<SapReviewMeditation> SapReviewMeditation { get; set; }
        public virtual DbSet<SapTeachers> SapTeachers { get; set; }
        public virtual DbSet<SapUserGroups> SapUserGroups { get; set; }
        public virtual DbSet<SapUserMeditations> SapUserMeditations { get; set; }
        public virtual DbSet<SapUserPoses> SapUserPoses { get; set; }

        // Unable to generate entity type for table 'dbo.AspNetUserRoles'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-3LVGE28V\SQLEXPRESS;Database=SapremaFinal;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<SapClass>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK_Sap_Class");

                entity.ToTable("Sap_Class");

                entity.Property(e => e.ClassId).ValueGeneratedNever();

                entity.Property(e => e.ClassCreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ClassCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ClassModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.ClassTheme).HasMaxLength(256);

                entity.HasOne(d => d.ClassCreatedByNavigation)
                    .WithMany(p => p.SapClass)
                    .HasForeignKey(d => d.ClassCreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_Class_AspNetUsers");

                entity.HasOne(d => d.ClassGroup)
                    .WithMany(p => p.SapClass)
                    .HasForeignKey(d => d.ClassGroupId)
                    .HasConstraintName("FK_Sap_Class_Sap_Groups");
            });

            modelBuilder.Entity<SapClassComplete>(entity =>
            {
                entity.HasKey(e => e.ClassCompleteId)
                    .HasName("PK_Sap");

                entity.ToTable("Sap_ClassComplete");

                entity.Property(e => e.ClassCompleteId).ValueGeneratedNever();

                entity.Property(e => e.ClassCompletedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.ClassComplete)
                    .WithOne(p => p.SapClassComplete)
                    .HasForeignKey<SapClassComplete>(d => d.ClassCompleteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ClassComplete_Sap_Class");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapClassComplete)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_AspNetUsers");
            });

            modelBuilder.Entity<SapClassPoses>(entity =>
            {
                entity.HasKey(e => e.ClassPoseId)
                    .HasName("PK_Sap_ClassPoses");

                entity.ToTable("Sap_ClassPoses");

                entity.Property(e => e.ClassPoseId).ValueGeneratedNever();

                entity.Property(e => e.PoseId)
                    .IsRequired()
                    .HasColumnType("nchar(3)");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SapClassPoses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ClassPoses_Sap_Class");

                entity.HasOne(d => d.Pose)
                    .WithMany(p => p.SapClassPoses)
                    .HasForeignKey(d => d.PoseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ClassPoses_Sap_Poses");
            });

            modelBuilder.Entity<SapFlagClasses>(entity =>
            {
                entity.HasKey(e => e.FlagId)
                    .HasName("PK_Sap_FlagClasses");

                entity.ToTable("Sap_FlagClasses");

                entity.Property(e => e.FlagId).ValueGeneratedNever();

                entity.Property(e => e.FlagComment).IsRequired();

                entity.Property(e => e.ReasonFlagged)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SapFlagClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_FlagClasses_Sap_Class");

                entity.HasOne(d => d.ClassReview)
                    .WithMany(p => p.SapFlagClasses)
                    .HasForeignKey(d => d.ClassReviewId)
                    .HasConstraintName("FK_Sap_FlagClasses_Sap_ReviewClass");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapFlagClasses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_FlagClasses_AspNetUsers");
            });

            modelBuilder.Entity<SapFlagMeditations>(entity =>
            {
                entity.HasKey(e => e.FlagId)
                    .HasName("PK_Sap_FlagMeditations");

                entity.ToTable("Sap_FlagMeditations");

                entity.Property(e => e.FlagId).ValueGeneratedNever();

                entity.Property(e => e.FlagComment).IsRequired();

                entity.Property(e => e.ReasonFlagged)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Meditation)
                    .WithMany(p => p.SapFlagMeditations)
                    .HasForeignKey(d => d.MeditationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_FlagMeditations_Sap_Meditations");

                entity.HasOne(d => d.MeditationReview)
                    .WithMany(p => p.SapFlagMeditations)
                    .HasForeignKey(d => d.MeditationReviewId)
                    .HasConstraintName("FK_Sap_FlagMeditations_Sap_ReviewMeditation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapFlagMeditations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_FlagMeditations_AspNetUsers");
            });

            modelBuilder.Entity<SapGroups>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_Sap_Groups");

                entity.ToTable("Sap_Groups");

                entity.Property(e => e.GroupId).ValueGeneratedNever();

                entity.Property(e => e.GroupAdmin)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.GroupAdminNavigation)
                    .WithMany(p => p.SapGroups)
                    .HasForeignKey(d => d.GroupAdmin)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_Groups_Sap_Teachers");
            });

            modelBuilder.Entity<SapMeditations>(entity =>
            {
                entity.HasKey(e => e.MeditationId)
                    .HasName("PK_Sap_Meditations");

                entity.ToTable("Sap_Meditations");

                entity.Property(e => e.MeditationId).ValueGeneratedNever();

                entity.Property(e => e.MeditationCreator)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MeditationName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MeditationTheme)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MeditationType)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<SapPoses>(entity =>
            {
                entity.HasKey(e => e.PoseId)
                    .HasName("PK_Sap_Poses");

                entity.ToTable("Sap_Poses");

                entity.Property(e => e.PoseId).HasColumnType("nchar(3)");

                entity.Property(e => e.PoseName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PoseTheme).IsRequired();
            });

            modelBuilder.Entity<SapReviewClass>(entity =>
            {
                entity.HasKey(e => e.ReviewClassId)
                    .HasName("PK_Sap_ReviewClass");

                entity.ToTable("Sap_ReviewClass");

                entity.Property(e => e.ReviewClassId).ValueGeneratedNever();

                entity.Property(e => e.ReviewStars).HasColumnType("decimal");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SapReviewClass)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ReviewClass_Sap_Class");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapReviewClass)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ReviewClass_AspNetUsers");
            });

            modelBuilder.Entity<SapReviewMeditation>(entity =>
            {
                entity.HasKey(e => e.ReviewMeditationId)
                    .HasName("PK_Sap_ReviewMeditation");

                entity.ToTable("Sap_ReviewMeditation");

                entity.Property(e => e.ReviewMeditationId).ValueGeneratedNever();

                entity.Property(e => e.ReviewStars).HasColumnType("decimal");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Meditation)
                    .WithMany(p => p.SapReviewMeditation)
                    .HasForeignKey(d => d.MeditationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ReviewMeditation_Sap_Meditations");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapReviewMeditation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_ReviewMeditation_AspNetUsers");
            });

            modelBuilder.Entity<SapTeachers>(entity =>
            {
                entity.HasKey(e => e.TeachId)
                    .HasName("PK_Sap_Teacher");

                entity.ToTable("Sap_Teachers");

                entity.Property(e => e.TeachId).HasMaxLength(450);

                entity.Property(e => e.Cert)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Site).HasMaxLength(256);

                entity.Property(e => e.Studio)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Teach)
                    .WithOne(p => p.SapTeachers)
                    .HasForeignKey<SapTeachers>(d => d.TeachId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_Teacher_AspNetUsers");
            });

            modelBuilder.Entity<SapUserGroups>(entity =>
            {
                entity.HasKey(e => e.UserGroupId)
                    .HasName("PK_Sap_UserGroups");

                entity.ToTable("Sap_UserGroups");

                entity.Property(e => e.UserGroupId).ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.SapUserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_UserGroups_Sap_Groups");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapUserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_UserGroups_AspNetUsers");
            });

            modelBuilder.Entity<SapUserMeditations>(entity =>
            {
                entity.HasKey(e => e.UserMeditationsId)
                    .HasName("PK_Sap_UserMeditations");

                entity.ToTable("Sap_UserMeditations");

                entity.Property(e => e.UserMeditationsId).ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Meditation)
                    .WithMany(p => p.SapUserMeditations)
                    .HasForeignKey(d => d.MeditationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_UserMeditations_Sap_Meditations");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapUserMeditations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_UserMeditations_AspNetUsers");
            });

            modelBuilder.Entity<SapUserPoses>(entity =>
            {
                entity.HasKey(e => e.UserPoseId)
                    .HasName("PK_Sap_UserPoses");

                entity.ToTable("Sap_UserPoses");

                entity.Property(e => e.UserPoseId).ValueGeneratedNever();

                entity.Property(e => e.PoseId)
                    .IsRequired()
                    .HasColumnType("nchar(3)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SapUserPoses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sap_UserPose_AspNetUsers");
            });
        }
    }
}